using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Warehouse.Framework;
using System.IO;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace App.Framework.UI
{
    public partial class MainForm : Form
    {
        private List<IMenuExtension> extensions;
        ExtentionMenuAttribute attribute;

        public MainForm()
        {
            InitializeComponent();
            InitializePersianCulture();

            addPanel.AutoScroll = true;

            extensions = new List<IMenuExtension>();

            MessageBoxManager.OK = "بله";
            MessageBoxManager.Yes = "بله";
            MessageBoxManager.No = "خیر";
            MessageBoxManager.Register();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadExtensions();
            AddExtensionButtons("Warehouse");
        }

        private Dictionary<string, List<Type>> categorizedExtensions;

        private void LoadExtensions()
        {
            categorizedExtensions = new Dictionary<string, List<Type>>();
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var dllFiles = Directory.GetFiles(path, "*.dll");

                foreach (var dllFile in dllFiles)
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(dllFile);
                        var types = assembly.GetTypes().Where(t => typeof(IMenuExtension).IsAssignableFrom(t) && typeof(BaseForm).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                        foreach (var type in types)
                        {
                            try
                            {
                                //var attributes = (ExtentionMenuAttribute)type.GetCustomAttributes(typeof(ExtentionMenuAttribute), true).ToList();
                                attribute = (ExtentionMenuAttribute)type.GetCustomAttributes(typeof(ExtentionMenuAttribute), true).LastOrDefault();
                                if (attribute != null)
                                {
                                    if (!categorizedExtensions.ContainsKey(attribute.CategoryName))
                                        categorizedExtensions[attribute.CategoryName] = new List<Type>();

                                    categorizedExtensions[attribute.CategoryName].Add(type);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error processing type {type.FullName}: {ex.Message}");
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading extensions from the current assembly: {ex.Message}");
            }
        }

        private void ExtensionButton_Click(object sender, EventArgs e)
        {
            if (sender is Label label && label.Tag is Type extensionType)
            {
                try
                {
                    var resForm = (BaseForm)Activator.CreateInstance(extensionType);

                    resForm.MdiParent = this;

                    resForm.TabCtrl = mainTabControl;

                    TabPage tp = new TabPage();
                    tp.Parent = mainTabControl;
                    tp.Text = resForm.Text;
                    tp.Show();

                    resForm.TabPag = tp;
                    tp.Controls.Add(resForm);

                    resForm.Show();

                    mainTabControl.SelectedTab = tp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating an instance: {ex.Message}");
                }
                
            }
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void AddExtensionButtons(string categoryName)
        {
            addPanel.Controls.Clear();
            if (categorizedExtensions.ContainsKey(categoryName))
            {
                var categoryExtensions = categorizedExtensions[categoryName]
                    .OrderByDescending(extensionType =>
                    {
                        var attribute = (ExtentionMenuAttribute)extensionType.GetCustomAttributes(typeof(ExtentionMenuAttribute), true).FirstOrDefault();
                        return attribute?.Order ?? int.MaxValue;
            })
                    .ToList();

                foreach (var extensionType in categoryExtensions)
                {
                    attribute = (ExtentionMenuAttribute)extensionType.GetCustomAttributes(typeof(ExtentionMenuAttribute), true).FirstOrDefault();
                    Label label = new Label
                    {
                        Text = attribute.MenuName,
                        Tag = extensionType,
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Cursor = Cursors.Hand,
                        RightToLeft = RightToLeft.Yes
                    };

                    label.Click += ExtensionButton_Click;
                    label.MouseEnter += Label_MouseEnter;
                    label.MouseLeave += Label_MouseLeave;

                    Panel panel = new Panel
                    {
                        Dock = DockStyle.Top,
                        Height = Math.Max(label.Height, label.Height),
                        Cursor = Cursors.Hand,
                        RightToLeft = RightToLeft.No,
                    };

                    panel.Controls.Add(label);
                    addPanel.Controls.Add(panel);
                }
            }
        }


        private void Label_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Label label && label.Parent is Panel panel)
            {
                panel.BorderStyle = BorderStyle.None;
                panel.BackColor = System.Drawing.Color.White;
            }
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Label label && label.Parent is Panel panel)
            {
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.BackColor = System.Drawing.Color.LightCyan;
            }
        }

        private void warehouseMenuBtn_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                //button.BorderStyle = BorderStyle.FixedSingle;
                button.BackColor = System.Drawing.Color.LightCyan;
            }
        }

        private void warehouseMenuBtn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                //button.BorderStyle = BorderStyle.FixedSingle;
                button.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void warehouseMenuBtn_Click(object sender, EventArgs e)
        {
            LoadExtensions();
            AddExtensionButtons("Warehouse");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadExtensions();
            AddExtensionButtons(" ");
        }


        private static void InitializePersianCulture()
        {

            CultureInfo info = new CultureInfo("fa-Ir");
            info.DateTimeFormat.Calendar = new PersianCalendar();
            Thread.CurrentThread.CurrentCulture = info;
        }
    }
}
