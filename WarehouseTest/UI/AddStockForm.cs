using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.UI;
using App.Framework.UI.Model;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Framework.UI;
using WarehouseTest.forms;
using WarehouseTest.Services.StockService;
using WarehouseTest.Services.TableIdService;

namespace WarehouseTest.UI
{
    [ExtentionMenu(CategoryName = "Warehouse",  MenuName = "انبار جدید", Order =3)]
    public partial class AddStockForm : EntityBaseForm , IMenuExtension
    {
        private readonly ITableIdService _tableIdService;
        private readonly IStockService _stockService;
        StockDataSet stockDataSet;
        private int _id;


        public AddStockForm()
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            stockDataSet = new StockDataSet();
            _id = 0;
        }

        public override void SetInputId(int inputId)
        {
            stockDataSet = _stockService.GetById(inputId);

            stockCodeTxt.Text = stockDataSet.StockTable[0].Code.ToString();
            stockNameTx.Text = stockDataSet.StockTable[0].Name.ToString();
            _id = inputId;
        }

        public AddStockForm(int id, int code, string name)
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            stockDataSet = new StockDataSet();

            //itemTable = new ItemTable();


            stockCodeTxt.Text = code.ToString();
            stockNameTx.Text = name;
            _id = id;
        }

        private void AddStockForm_Load(object sender, EventArgs e)
        {
            deleteBtn.Enabled = false;
            //addBtn.Enabled = false;
            //refreshBtn.Enabled = false;
            //MaximizeBox = false;
        }

        public override void addBtn_Click(object sender, EventArgs e)
        {
            MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm == null)
            {
                mainForm = new MainForm();
                mainForm.Show();
            }

            AddStockForm addStockForm = new AddStockForm();
            addStockForm.MdiParent = mainForm;

            addStockForm.TabCtrl = mainForm.mainTabControl;

            TabPage tp = new TabPage();
            tp.Parent = mainForm.mainTabControl;
            tp.Text = addStockForm.Text;
            tp.Show();

            addStockForm.TabPag = tp;
            tp.Controls.Add(addStockForm);

            addStockForm.Show();

            mainForm.mainTabControl.SelectedTab = tp;
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _stockService.Save(_id,stockNameTx.Text, stockCodeTxt.Text);
                MessageBox.Show("انبار با موفقیت ذخیره گردید");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemCodeLbl_Click(object sender, EventArgs e)
        {

        }
    }
}