using System.Drawing;
using System.Text;
using WarehouseTest.Services;
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.ReceiptService;
using WarehouseTest.Services.TableIdService;
using WarehouseTest.UI;
using WarehouseTest.UI.models;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Warehouse.Framework.Common;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Bus.Services.ReportService;

namespace WarehouseTest.forms
{
    public partial class MainForm : Form
    {
        private List<IExtension> extensions;
        //string PluginsFolderName;

        public MainForm()
        {
            InitializeComponent();
            addPanel.AutoScroll = true;
            extensions = new List<IExtension>();
            //PluginsFolderName = "Plugins";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadExtensions();
            AddExtensionButtons();
        }


        //private void LoadExtensions()
        //{
        //    string pluginFolder = AppDomain.CurrentDomain.BaseDirectory; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginsFolderName);
        //    if (!Directory.Exists(pluginFolder))
        //    {
        //        MessageBox.Show("فولدر پلاگین یافت نشد");
        //        return;
        //    }

        //    string[] dllFiles = Directory.GetFiles(pluginFolder, "*.dll");
        //    foreach (string dllFile in dllFiles)
        //    {
        //        try
        //        {
        //            Assembly assembly = Assembly.LoadFrom(dllFile);
        //            var types = assembly.GetTypes().Where(t => typeof(IExtension).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        //            foreach (var type in types)
        //            {
        //                var extension = (IExtension)Activator.CreateInstance(type);
        //                extensions.Add(extension);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"خطا در دریافت افزونه از {dllFile}: {ex.Message}");
        //        }
        //    }


        //    extensions = extensions.OrderByDescending(ext => ext.Order).ToList();
        //}

        private void LoadExtensions()
        {
            try
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                var types = currentAssembly.GetTypes().Where(t => typeof(IExtension).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (var type in types)
                {
                    try
                    {
                        var extension = (IExtension)Activator.CreateInstance(type);
                        extensions.Add(extension);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error creating an instance of {type.FullName}: {ex.Message}");
                    }
                }

                extensions = extensions.OrderByDescending(ext => ext.Order).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading extensions from the current assembly: {ex.Message}");
            }
        }


        //private void AddExtensionButtons()
        //{
        //    foreach (var extension in extensions)
        //    {
        //        Button btn = new Button
        //        {
        //            Text = extension.Name,
        //            Tag = extension,
        //            Dock = DockStyle.Top
        //        };

        //        btn.Click += ExtensionButton_Click;

        //        addPanel.Controls.Add(btn);
        //    }
        //}

        private void ExtensionButton_Click(object sender, EventArgs e)
        {
            if (sender is Label label && label.Tag is IExtension extension)
            {
                var resForm = extension.Btn_Click();

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
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddExtensionButtons()
        {
            foreach (var extension in extensions)
            {
                Label label = new Label
                {
                    Text = extension.Name,
                    Tag = extension,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Cursor = Cursors.Hand,
                    RightToLeft = RightToLeft.Yes
                };
                label.Click += ExtensionButton_Click;
                label.MouseEnter += Label_MouseEnter;
                label.MouseLeave += Label_MouseLeave;

                // Create PictureBox for the icon
                //PictureBox iconPictureBox = new PictureBox
                //{
                //    Image = Image.FromFile("C:\\Users\\amirhoseinmo\\Pictures\\icons\\delete.png"),  // Replace YourIconImage with the actual image
                //    SizeMode = PictureBoxSizeMode.AutoSize,
                //    Cursor = Cursors.Hand
                //};


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
    }
}