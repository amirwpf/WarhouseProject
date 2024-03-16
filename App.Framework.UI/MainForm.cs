using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Warehouse.Framework.UI;
using System.IO;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Globalization;
using System.Threading;
using App.Framework.UI.Model;

namespace App.Framework.UI
{
    public partial class MainForm : Form
    {
        private Dictionary<string, List<Type>> categorizedExtensions;
        private Dictionary<string, List<MenuListType>> categorizedListExtensions;
        ExtentionMenuAttribute attribute;

        public MainForm()
        {
            InitializeComponent();
            InitializePersianCulture();
            addPanel.AutoScroll = true;

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
        private void LoadExtensions()
        {
            categorizedExtensions = new Dictionary<string, List<Type>>();
            categorizedListExtensions = new Dictionary<string, List<MenuListType>>();
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

                        var listTypes = assembly.GetTypes().Where(t => typeof(IMenuListInitializer).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

                        foreach (var type in types)
                        {
                            try
                            {
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

                        foreach (var listType in listTypes)
                        {
                            var instanse = Activator.CreateInstance(listType);
                            MethodInfo methodInfo = listType.GetMethod("GetMenuLists");
                            try
                            {
                                var formLists = (List<MenuListType>)(methodInfo.Invoke(instanse, null));
                                attribute = (ExtentionMenuAttribute)listType.GetCustomAttributes(typeof(ExtentionMenuAttribute), true).LastOrDefault();
                                if (attribute != null)
                                {
                                    if (!categorizedListExtensions.ContainsKey(attribute.CategoryName))
                                        categorizedListExtensions[attribute.CategoryName] = new List<MenuListType>();

                                    categorizedListExtensions[attribute.CategoryName].AddRange(formLists);
                                }
                            }
                            catch
                            {

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
                    MainFormManager.AddFormToMainForm(resForm);
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

            if (categorizedListExtensions.ContainsKey(categoryName))
            {
                var result = categorizedListExtensions[categoryName];
                var extensionLists = result.OrderByDescending(x => x.Order);
                foreach (var extensionList in extensionLists)
                {
                    var label = new ListMenuItem(extensionList.FormTitle);
                    label.GetListFormFunc = extensionList.Form;

                    label.Click += ExtensionListButton_Click;
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

            Label listLbl = new Label
            {
                Text = "---> فهرست ",
                ForeColor = System.Drawing.Color.Gray,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                RightToLeft = RightToLeft.Yes
            };
            Panel listPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = Math.Max(listLbl.Height, listLbl.Height),
                Cursor = Cursors.Hand,
                RightToLeft = RightToLeft.No,
            };

            listPanel.Controls.Add(listLbl);
            addPanel.Controls.Add(listPanel);

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

            Label menuLbl = new Label
            {
                Text = "---> عملیات ",
                ForeColor = System.Drawing.Color.Gray,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                RightToLeft = RightToLeft.Yes
            };
            Panel menuPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = Math.Max(menuLbl.Height, menuLbl.Height),
                Cursor = Cursors.Hand,
                RightToLeft = RightToLeft.No,
            };

            menuPanel.Controls.Add(menuLbl);
            addPanel.Controls.Add(menuPanel);


        }

        private void ExtensionListButton_Click(object sender, EventArgs e)
        {
            if (sender is ListMenuItem label && label.GetListFormFunc is Func<BaseListForm> extensionType)
            {
                try
                {
                    var resForm =extensionType();
                    resForm.Text = label.Text;
                    MainFormManager.AddFormToMainForm(resForm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating an instance: {ex.Message}");
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
                button.BackColor = System.Drawing.Color.LightCyan;
            }
        }

        private void warehouseMenuBtn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
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

        private void menuBox_Enter(object sender, EventArgs e)
        {

        }
    }
}