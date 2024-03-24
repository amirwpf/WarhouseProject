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

namespace WarehouseTest.UI
{
    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "گزارش کالای انبار", Order = 31)]
    public partial class StockItemQuantityReportForm : BaseListForm
    {
        private readonly IStockService _stockService;
        //StockItemQuantityReportDataSet stockItemQuantityReportDataSet;
        DataTable stockItemQuantityReportDataTable;

        public StockItemQuantityReportForm()
        {
            InitializeComponent();
            addBtn.Enabled = false;
            deleteBtn.Enabled = false;
            var serviceFactory = new ServiceFactory();
            _stockService = serviceFactory.Resolve<IStockService>();

            stockItemQuantityReportDataTable = _stockService.GetStockItemQuantityReport();

            InitializeItemDataGirdView();
        }

        private void InitializeItemDataGirdView()
        {
            //stockItemQuantityReportDataTable = stockItemQuantityReportDataSet.StockItemQuantityReportDataTable;
            dataGrid.DataSource = stockItemQuantityReportDataTable;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.Columns["Id"].Visible = false;
            dataGrid.Columns["Name"].Visible = false;
            dataGrid.Columns["Quantity"].Visible = false;

            var nameColumn = new DataGridViewTextBoxColumn
            {
                Name = "nameColumn",
                HeaderText = "نام انبار",
                DataPropertyName = "Name",
            };

            dataGrid.Columns.Add(nameColumn);

            var quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "quantityColumn",
                HeaderText = "موجودی کالا",
                DataPropertyName = "Quantity",
            };

            dataGrid.Columns.Add(quantityColumn);
        }

        private void StockItemQuantityReportForm_Load(object sender, EventArgs e)
        {
            deleteBtn.Enabled = false;
            addBtn.Enabled = false;
        }

        protected override void refreshBtn_Click(object sender, EventArgs e)
        {
            stockItemQuantityReportDataTable = _stockService.GetStockItemQuantityReport();
            dataGrid.DataSource = stockItemQuantityReportDataTable;
        }
    }
}
