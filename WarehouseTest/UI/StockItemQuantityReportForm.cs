using App.Bus.Services.ReportService;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Domin.Core.Entities.TypedDataTables;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WarehouseTest.UI.models;

namespace WarehouseTest.UI
{
    public partial class StockItemQuantityReportForm : BaseForm
    {
        private readonly IReportService _reportService;
        StockItemQuantityReportDataSet stockItemQuantityReportDataSet;
        StockItemQuantityReportDataTable stockItemQuantityReportDataTable;
        public StockItemQuantityReportForm()
        {
            InitializeComponent();

            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IReportService, ReportService>();
            _reportService = proxyFactory.Resolve<IReportService>();

            stockItemQuantityReportDataSet = _reportService.GetStockItemQuantityReport();

            InitializeItemDataGirdView();
        }

        private void InitializeItemDataGirdView()
        {
            stockItemQuantityReportDataTable = stockItemQuantityReportDataSet.StockItemQuantityReportDataTable;
            itemDataGrid.DataSource = stockItemQuantityReportDataTable;
            itemDataGrid.AllowUserToAddRows = false;
            itemDataGrid.AllowUserToDeleteRows = false;
            itemDataGrid.Columns["Id"].Visible = false;
            itemDataGrid.Columns["Name"].Visible = false;
            itemDataGrid.Columns["Quantity"].Visible = false;

            //var codeColumn = new DataGridViewTextBoxColumn
            //{
            //    Name = "codeColumn",
            //    HeaderText = "کد انبار",
            //    DataPropertyName = "Code",
            //};

            //itemDataGrid.Columns.Add(codeColumn);

            var nameColumn = new DataGridViewTextBoxColumn
            {
                Name = "nameColumn",
                HeaderText = "نام انبار",
                DataPropertyName = "Name",
            };

            itemDataGrid.Columns.Add(nameColumn);

            var quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "quantityColumn",
                HeaderText = "موجودی کالا",
                DataPropertyName = "Quantity",
            };

            itemDataGrid.Columns.Add(quantityColumn);
        }

        private void StockItemQuantityReportForm_Load(object sender, EventArgs e)
        {
            saveBtn.Enabled = false;
            deleteBtn.Enabled = false;
            refreshBtn.Enabled = false;
            addBtn.Enabled = false;
        }
    }
}
