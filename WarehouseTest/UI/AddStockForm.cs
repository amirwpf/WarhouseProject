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
        private readonly IStockService _stockService;
        StockDataSet _stockDataSet;


        public AddStockForm()
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _stockService = serviceFactory.Resolve<IStockService>();

            _stockDataSet = new StockDataSet();
            var newRow = _stockDataSet.StockTable.GetNewRow();
            _stockDataSet.StockTable.Add(newRow);
            stockNameTx.DataBindings.Add("Text", _stockDataSet.StockTable[0], "Name");
            Binding binding = new Binding("Text", _stockDataSet.StockTable[0], "Code", true, DataSourceUpdateMode.OnPropertyChanged);

            binding.Format += (sender, e) =>
            {
                if (e.Value != null && e.Value != DBNull.Value && int.TryParse(e.Value.ToString(), out int intValue))
                {
                    e.Value = intValue.ToString();
                }
            };

            binding.Parse += (sender, e) =>
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int intValue))
                {
                    e.Value = intValue;
                }
                else
                {
                    e.Value = 0;
                }
            };

            stockCodeTxt.DataBindings.Add(binding);
            _inputId = 0;
        }

        public AddStockForm(int id):base(id)
        {
            InitializeComponent();
            var serviceFactory = new ServiceFactory();
            _stockService = serviceFactory.Resolve<IStockService>();
            _stockDataSet = _stockService.GetById(id);
            stockNameTx.DataBindings.Add("Text", _stockDataSet.StockTable[0], "Name");
            Binding binding = new Binding("Text", _stockDataSet.StockTable[0], "Code", true, DataSourceUpdateMode.OnPropertyChanged);

            binding.Format += (sender, e) =>
            {
                if (e.Value != null && e.Value != DBNull.Value && int.TryParse(e.Value.ToString(), out int intValue))
                {
                    e.Value = intValue.ToString();
                }
            };

            binding.Parse += (sender, e) =>
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int intValue))
                {
                    e.Value = intValue;
                }
                else
                {
                    e.Value = 0;
                }
            };

            stockCodeTxt.DataBindings.Add(binding);
            _inputId = id;
        }
        private void AddStockForm_Load(object sender, EventArgs e)
        {
            deleteBtn.Enabled = false;
        }

        public override void addBtn_Click(object sender, EventArgs e)
        {
            MainFormManager.AddFormToMainForm(new AddStockForm());
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool dataIsValid = ValidateData(stockNameTx.Text, stockCodeTxt.Text, out int validCode);
                if (dataIsValid)
                {
                    //ItemDataSet itemDataSet;
                    //if (_id == 0)
                    //{
                    //    _stockDataSet = new StockDataSet();
                    //    var newRow = _stockDataSet.StockTable.GetNewRow();
                    //    _stockDataSet.StockTable.Add(newRow);
                    //    _stockDataSet.StockTable[0].Code = validCode;
                    //    _stockDataSet.StockTable[0].Name = stockNameTx.Text;
                    //}
                    //else
                    //{
                    //    _stockDataSet = _stockService.GetById(_id);
                    //    _stockDataSet.StockTable[0].Code = validCode;
                    //    _stockDataSet.StockTable[0].Name = stockNameTx.Text;
                    //}
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