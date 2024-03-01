//using App.Domin.Core.Contracts.ServiceInterface;
//using App.Framework;
//using App.Framework.UI;
//using App.Framework.UI.Model;
//using Core.Entites;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace WarehouseTest.UI
//{
//    [ExtentionMenu(CategoryName = "Warehouse" , MenuName = "گزارش کالای انبار", Order =31)]
//    public partial class StockItemQuantityReportForm : ListBaseForm , IMenuExtension
//    {
//        private readonly IStockService _stockService;
//        //StockItemQuantityReportDataSet stockItemQuantityReportDataSet;
//        DataTable stockItemQuantityReportDataTable;

//        public StockItemQuantityReportForm()
//        {
//            InitializeComponent();

//            var serviceFactory = new ServiceFactory();
//            _stockService = serviceFactory.Resolve<IStockService>();

//            stockItemQuantityReportDataTable = _stockService.GetStockItemQuantityReport();

//            InitializeItemDataGirdView();
//        }

//        private void InitializeItemDataGirdView()
//        {
//            //stockItemQuantityReportDataTable = stockItemQuantityReportDataSet.StockItemQuantityReportDataTable;
//            itemDataGrid.DataSource = stockItemQuantityReportDataTable;
//            itemDataGrid.AllowUserToAddRows = false;
//            itemDataGrid.AllowUserToDeleteRows = false;
//            itemDataGrid.Columns["Id"].Visible = false;
//            itemDataGrid.Columns["Name"].Visible = false;
//            itemDataGrid.Columns["Quantity"].Visible = false;

//            var nameColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "nameColumn",
//                HeaderText = "نام انبار",
//                DataPropertyName = "Name",
//            };

//            itemDataGrid.Columns.Add(nameColumn);

//            var quantityColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "quantityColumn",
//                HeaderText = "موجودی کالا",
//                DataPropertyName = "Quantity",
//            };

//            itemDataGrid.Columns.Add(quantityColumn);
//        }

//        private void StockItemQuantityReportForm_Load(object sender, EventArgs e)
//        {
//            //saveBtn.Enabled = false;
//            deleteBtn.Enabled = false;
//            //refreshBtn.Enabled = false;
//            addBtn.Enabled = false;
//        }

//        public override void refreshBtn_Click(object sender, EventArgs e)
//        {
//            stockItemQuantityReportDataTable = _stockService.GetStockItemQuantityReport();
//            itemDataGrid.DataSource = stockItemQuantityReportDataTable;
//        }
//    }
//}
