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

namespace WarehouseTest.forms
{
   public partial class MainForm : Form
    {
        private List<IExtension> extensions;
        string PluginsFolderName;
        public MainForm()
        {
            InitializeComponent();
            addPanel.AutoScroll = true;
            extensions = new List<IExtension>();
            PluginsFolderName = "Plugins";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadExtensions();
            AddExtensionButtons();
        }


        private void LoadExtensions()
        {
            string pluginFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginsFolderName);
            if (!Directory.Exists(pluginFolder))
            {
                MessageBox.Show("فولدر پلاگین یافت نشد");
                return;
            }

            string[] dllFiles = Directory.GetFiles(pluginFolder, "*.dll");
            foreach (string dllFile in dllFiles)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllFile);
                    var types = assembly.GetTypes().Where(t => typeof(IExtension).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                    foreach (var type in types)
                    {
                        var extension = (IExtension)Activator.CreateInstance(type);
                        extensions.Add(extension);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطا در دریافت افزونه از {dllFile}: {ex.Message}");
                }
            }

            
            extensions = extensions.OrderByDescending(ext => ext.Order).ToList();
        }

        private void AddExtensionButtons()
        {
            foreach (var extension in extensions)
            {
                Button btn = new Button
                {
                    Text = extension.Name,
                    Tag = extension, 
                    Dock = DockStyle.Top
                };

                btn.Click += ExtensionButton_Click;

                addPanel.Controls.Add(btn);
            }
        }

        private void ExtensionButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is IExtension extension)
            {
                var resForm = extension.Btn_Click();

                resForm.TopMost = true;
                this.IsMdiContainer = true;
                resForm.MdiParent = this;
                resForm.Show();
                mainContaier.Panel2.Controls.Add(resForm);
            }
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

















//private void tabControl_DrawItem(object sender, System.Windows.Forms.PaintEventArgs e)
//{
//    Graphics g = e.Graphics;
//    g.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
//    g.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
//    g.DrawFocusRectangle();
//}

//public void tabControl_MouseDown(object sender, EventArgs e)
//{
//    //Looping through the controls.
//    for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
//    {
//        Rectangle r = tabControl1.GetTabRect(i);
//        //Getting the position of the "x" mark.
//        Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
//        if (closeButton.Contains(e.Location))
//        {
//            if (MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                this.tabControl1.TabPages.RemoveAt(i);
//                break;
//            }
//        }
//    }
//}



//private void addReceipt_Click(object sender, EventArgs e)
//{
//    AddReceiptTabPage addReceiptTab;
//    var existenceTab = TabControlContainsType<AddReceiptTabPage>(mainTabControl);
//    if (existenceTab == null)
//    {
//        addReceiptTab = new AddReceiptTabPage();
//        addReceiptTab.Text = "Add Receipt";
//        mainTabControl.TabPages.Add(addReceiptTab);
//        AddReceiptForm frm = new AddReceiptForm();
//        frm.TopLevel = false;
//        frm.Dock = DockStyle.Fill;
//        frm.MaximizeBox = false;
//        frm.MinimizeBox = false;
//        addReceiptTab.Controls.Add(frm);
//        frm.FormBorderStyle = FormBorderStyle.None;
//        frm.Show();
//        mainTabControl.SelectedTab = addReceiptTab;
//        //addReceiptTab.SetCloseButtonLocation(addReceiptTab.Location.X, addReceiptTab.Location.Y - 25);
//    }
//    else
//    {
//        mainTabControl.TabPages.Remove(existenceTab);
//    }
//}

//private void addItemBtn_Click(object sender, EventArgs e)
//{
//    AddItemTabPage addItemTab;
//    var existenceTab = TabControlContainsType<AddItemTabPage>(mainTabControl);
//    if (existenceTab == null)
//    {
//        addItemTab = new AddItemTabPage();
//        addItemTab.Text = "Add Item";
//        mainTabControl.TabPages.Add(addItemTab);
//        AddItemForm frm = new AddItemForm();
//        frm.TopLevel = false;
//        frm.Dock = DockStyle.Fill;
//        frm.MaximizeBox = false;
//        frm.MinimizeBox = false;
//        addItemTab.Controls.Add(frm);
//        frm.FormBorderStyle = FormBorderStyle.None;
//        frm.Show();
//        mainTabControl.SelectedTab = addItemTab;
//        //addItemTab.SetCloseButtonLocation(addItemTab.Location.X, addItemTab.Location.Y-25);
//    }
//    else
//    {
//        mainTabControl.TabPages.Remove(existenceTab);
//    }
//}

//private T TabControlContainsType<T>(TabControl tabControl) where T : TabPage
//{
//    foreach (var tab in tabControl.TabPages)
//    {
//        if (tab is T)
//        {
//            return (T)tab;
//        }
//    }

//    return null;
//}

//private void addDeliveryBtn_Click(object sender, EventArgs e)
//{
//    AddDeliveryTabPage addDeliveryTab;
//    var existenceTab = TabControlContainsType<AddDeliveryTabPage>(mainTabControl);
//    if (existenceTab == null)
//    {
//        addDeliveryTab = new AddDeliveryTabPage();
//        addDeliveryTab.Text = "Add Delivery";
//        //addDeliveryTab.SendToBack();
//        ////Button b = new Button();
//        //addDeliveryTab.closeButton.Location= new Point(addDeliveryTab.Location.X, addDeliveryTab.Location.Y-10);
//        //addDeliveryTab.closeButton.BringToFront();
//        //addDeliveryTab.Controls.Add(addDeliveryTab.closeButton);
//        mainTabControl.TabPages.Add(addDeliveryTab);
//        AddDeliveryForm frm = new AddDeliveryForm();
//        frm.TopLevel = false;
//        frm.Dock = DockStyle.Fill;
//        frm.MaximizeBox = false;
//        frm.MinimizeBox = false;
//        addDeliveryTab.Controls.Add(frm);
//        frm.FormBorderStyle = FormBorderStyle.None;
//        frm.Show();
//        mainTabControl.SelectedTab = addDeliveryTab;
//        // addDeliveryTab.SetCloseButtonLocation(addDeliveryTab.Location.X, addDeliveryTab.Location.Y - 25);
//    }
//    else
//    {
//        mainTabControl.TabPages.Remove(existenceTab);
//    }
//}

//private void addStockBtn_Click(object sender, EventArgs e)
//{
//    AddStockTabPage addStockTab;
//    var existenceTab = TabControlContainsType<AddStockTabPage>(mainTabControl);
//    if (existenceTab == null)
//    {
//        addStockTab = new AddStockTabPage();
//        addStockTab.Text = "Add Stock";
//        mainTabControl.TabPages.Add(addStockTab);
//        AddStockForm frm = new AddStockForm();
//        frm.TopLevel = false;
//        frm.Dock = DockStyle.Fill;
//        frm.MaximizeBox = false;
//        frm.MinimizeBox = false;
//        addStockTab.Controls.Add(frm);
//        frm.FormBorderStyle = FormBorderStyle.None;
//        frm.Show();
//        mainTabControl.SelectedTab = addStockTab;
//        //addItemTab.SetCloseButtonLocation(addItemTab.Location.X, addItemTab.Location.Y-25);
//    }
//    else
//    {
//        mainTabControl.TabPages.Remove(existenceTab);
//    }
//}

//private void listOfReceiptBtn_Click(object sender, EventArgs e)
//{
//    ReceiptListTabPage receiptListTab;
//    var existenceTab = TabControlContainsType<ReceiptListTabPage>(mainTabControl);
//    if (existenceTab == null)
//    {
//        receiptListTab = new ReceiptListTabPage();
//        receiptListTab.Text = "Receipt List";
//        mainTabControl.TabPages.Add(receiptListTab);
//        ReceiptList frm = new ReceiptList();
//        frm.TopLevel = false;
//        frm.Dock = DockStyle.Fill;
//        frm.MaximizeBox = false;
//        frm.MinimizeBox = false;
//        receiptListTab.Controls.Add(frm);
//        frm.FormBorderStyle = FormBorderStyle.None;
//        frm.Show();
//        mainTabControl.SelectedTab = receiptListTab;
//        //addItemTab.SetCloseButtonLocation(addItemTab.Location.X, addItemTab.Location.Y-25);
//    }
//    else
//    {
//        mainTabControl.TabPages.Remove(existenceTab);
//    }
//}