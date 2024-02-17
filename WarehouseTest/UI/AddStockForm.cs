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
        ITableIdService tableIdService;
        StockDataSet stockDataSet;
        IStockService stockService;
        public AddStockForm()
        {
            InitializeComponent();
            tableIdService = new TableIdService();
            stockDataSet = new StockDataSet();
            stockService = new StockService();
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
                stockService.Save(stockNameTx.Text, stockCodeTxt.Text);
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
