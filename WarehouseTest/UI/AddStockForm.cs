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

namespace WarehouseTest.UI
{
    [ExtentionMenu(CategoryName = "Warehouse",  MenuName = "انبار جدید", Order =3)]
    public partial class AddStockForm : EntityBaseForm , IMenuExtension
    {
        private readonly ITableIdService _tableIdService;
        private readonly IStockService _stockService;
        StockDataSet _stockDataSet;
        private int _id;


        public AddStockForm()
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            _stockDataSet = new StockDataSet();
            _id = 0;
        }

        public override void SetInputId(int inputId)
        {
            _stockDataSet = _stockService.GetById(inputId);

            stockCodeTxt.Text = _stockDataSet.StockTable[0].Code.ToString();
            stockNameTx.Text = _stockDataSet.StockTable[0].Name.ToString();
            _id = inputId;
        }

        //public AddStockForm(int id, int code, string name)
        //{
        //    InitializeComponent();

        //    var serviceFactory = new ServiceFactory();
        //    _tableIdService = serviceFactory.Resolve<ITableIdService>();
        //    _stockService = serviceFactory.Resolve<IStockService>();
        //    stockDataSet = new StockDataSet();

        //    //itemTable = new ItemTable();


        //    stockCodeTxt.Text = code.ToString();
        //    stockNameTx.Text = name;
        //    _id = id;
        //}

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
                bool dataIsValid = ValidateData(stockNameTx.Text, stockCodeTxt.Text, out int validCode);
                if (dataIsValid)
                {
                    //ItemDataSet itemDataSet;
                    if (_id == 0)
                    {
                        _stockDataSet = new StockDataSet();
                        var newRow = _stockDataSet.StockTable.GetNewRow();
                        _stockDataSet.StockTable.Add(newRow);
                        _stockDataSet.StockTable[0].Code = validCode;
                        _stockDataSet.StockTable[0].Name = stockNameTx.Text;
                    }
                    else
                    {
                        _stockDataSet = _stockService.GetById(_id);
                        _stockDataSet.StockTable[0].Code = validCode;
                        _stockDataSet.StockTable[0].Name = stockNameTx.Text;
                    }
                    _stockService.Save(_stockDataSet);
                    MessageBox.Show("انبار با موفقیت ذخیره گردید");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateData(string name, string code, out int validCode)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(ErrorMessage.ItemCantBeEmpty("نام"));
                validCode = 0;
                return false;
            }
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show(ErrorMessage.ItemCantBeEmpty("کد"));
                validCode = 0;
                return false;
            }

            if (int.TryParse(code, out int _validCode))
            {
                validCode = _validCode;
                return true;
            }
            else
            {
                MessageBox.Show(ErrorMessage.ItemCantBeEmpty("کد"));
            }

            validCode = 0;
            return false;
        }

        private void itemCodeLbl_Click(object sender, EventArgs e)
        {

        }
    }
}