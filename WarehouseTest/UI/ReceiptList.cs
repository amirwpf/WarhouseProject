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
//using WarehouseTest.Services.ReceiptService;
//using WarehouseTest.Services.StockService;

//namespace WarehouseTest.UI
//{
//    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "ورود انبار", Order = 8)]
//    public partial class ReceiptList : ListBaseForm, IMenuExtension
//    {
//        private readonly IReceiptService _receiptService;
//        ReceiptDataset receiptDataset;
//        private readonly IStockService _stockService;
//        ReceiptTable receiptRows;

//        public ReceiptList()
//        {
//            InitializeComponent();

//            var serviceFactory = new ServiceFactory();
//            _receiptService = serviceFactory.Resolve<IReceiptService>();
//            _stockService = serviceFactory.Resolve<IStockService>();

//            receiptDataset = _receiptService.GetMasterAll();

//            InitializeItemDataGirdView();

//        }

//        private void InitializeItemDataGirdView()
//        {
//            receiptRows = receiptDataset.ReceiptTable;
//            receiptDataGrid.DataSource = receiptDataset.ReceiptTable;
//            receiptDataGrid.ReadOnly = true;
//            receiptDataGrid.Columns["Id"].Visible = false;
//            receiptDataGrid.Columns["StockId"].Visible = false;
//            receiptDataGrid.Columns["Number"].Visible = false;
//            receiptDataGrid.Columns["Date"].Visible = false;
//            receiptDataGrid.Columns["StockCode"].Visible = false;
//            receiptDataGrid.Columns["StockName"].Visible = false;
//            receiptDataGrid.AllowUserToAddRows = false;
//            receiptDataGrid.AllowUserToDeleteRows = false;

//            var numberColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "numberColumn",
//                HeaderText = "شماره سند",
//                DataPropertyName = "Number",
//            };

//            receiptDataGrid.Columns.Add(numberColumn);

//            var dateColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "dateColumn",
//                HeaderText = "تاریخ سند",
//                DataPropertyName = "Date",
//            };

//            receiptDataGrid.Columns.Add(dateColumn);

//            var stockNameColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "stockNameColumn",
//                HeaderText = "انبار",
//                DataPropertyName = "StockName",
//            };

//            receiptDataGrid.Columns.Add(stockNameColumn);


//            var stockCodeColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "stockCodeColumn",
//                HeaderText = "کد انبار",
//                DataPropertyName = "StockCode",
//            };

//            receiptDataGrid.Columns.Add(stockCodeColumn);

//        }

//        private void ReceiptList_Load(object sender, EventArgs e)
//        {
//            //saveBtn.Enabled = false;
//            //MaximizeBox = false;
//        }

//        private void receiptDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            var id =receiptDataset.ReceiptTable[e.RowIndex].Id;
//            var stockId = receiptDataset.ReceiptTable[e.RowIndex].StockId;
//            var stock = _stockService.GetById(stockId);
//            var stock_Id = stock.StockTable[0].Id;
//            var res = _receiptService.GetByMasterId(id);
//            AddReceiptForm addReceiptForm = new AddReceiptForm(res, stock_Id);
//            addReceiptForm.WindowState = FormWindowState.Normal;
//            addReceiptForm.Show();
//        }

//        public override void refreshBtn_Click(object sender, EventArgs e)
//        {
//            receiptDataset = _receiptService.GetMasterAll();
//            receiptDataGrid.DataSource = receiptDataset.ReceiptTable;
//        }

//        public override void addBtn_Click(object sender, EventArgs e)
//        {
//            AddReceiptForm addReceiptForm = new AddReceiptForm();
//            addReceiptForm.Show();
//        }

//        public override void deleteBtn_Click(object sender, EventArgs e)
//        {
//            var selectedRows = receiptDataGrid.SelectedRows;
//            if (selectedRows.Count > 0)
//            {
//                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

//                if (result == DialogResult.Yes)
//                {

//                    foreach (DataGridViewRow row in selectedRows)
//                    {
//                        var stockRow = (row.DataBoundItem as DataRowView)?.Row as ReceiptRow;
//                        if (stockRow != null)
//                        {
//                            var id = stockRow.Id;
//                            try
//                            {
//                                _receiptService.DeleteById(id);
//                                MessageBox.Show("آیتم با موفقیت حذف گردید");
//                                RefreshDataGrid();
//                            }
//                            catch
//                            {
//                                MessageBox.Show("در فرآیند حذف خطایی رخ داد", "خطا");
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        //internal override void SaveBtn_Click(object sender, EventArgs e)
//        //{
//        //    try
//        //    {
//        //        foreach (DataRow row in receiptDataset.ReceiptTable.Select("", "", DataViewRowState.Deleted))
//        //        {
//        //            row.Delete();
//        //        }

//        //        _receiptService.Save(receiptDataset);
//        //        MessageBox.Show("ذخیره با موفقیت صورت گردید");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show(ex.Message);
//        //    }

//        //}

//        private DialogResult ShowConfirmationMessageBox(string message)
//        {
//            DialogResult result = MessageBox.Show(
//                message,
//                "تایید حذف",
//                MessageBoxButtons.YesNo,
//                MessageBoxIcon.Question,
//                MessageBoxDefaultButton.Button2
//            );

//            return result;
//        }

//        public void RefreshDataGrid()
//        {
//            receiptDataset = _receiptService.GetMasterAll();
//            receiptDataGrid.DataSource = receiptDataset.ReceiptTable;
//        }
//    }
//}