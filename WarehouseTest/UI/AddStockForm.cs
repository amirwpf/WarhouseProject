using App.Domin.Core.Contracts.ServiceInterface;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WarehouseTest.Services.StockService;
using WarehouseTest.Services.TableIdService;
using WarehouseTest.UI.models;

namespace WarehouseTest.UI
{
    public partial class AddStockForm : BaseForm
    {
        private readonly ITableIdService _tableIdService;
        private readonly IStockService _stockService;
        StockDataSet stockDataSet;
        public AddStockForm()
        {
            InitializeComponent();
            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<ITableIdService, TableIdService>();
            proxyFactory.Register<IStockService, StockService>();
            _tableIdService = proxyFactory.Resolve<ITableIdService>();
            _stockService = proxyFactory.Resolve<IStockService>();
            stockDataSet = new StockDataSet();
        }

        private void AddStockForm_Load(object sender, EventArgs e)
        {
            deleteBtn.Enabled = false;
            addBtn.Enabled = false;
            refreshBtn.Enabled = false;
            //MaximizeBox = false;
        }

        internal override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _stockService.Save(stockNameTx.Text, stockCodeTxt.Text);
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
