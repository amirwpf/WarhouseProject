﻿//using App.Domin.Core.Contracts.ServiceInterface;
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
//using WarehouseTest.Services.DeliveryService;
//using WarehouseTest.Services.ReceiptService;
//using WarehouseTest.Services.StockService;

//namespace WarehouseTest.UI
//{
//    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "خروج انبار", Order = 6)]
//    public partial class DeliveryListForm : ListBaseForm, IMenuExtension
//    {
//        IDeliveryService _deliveryService;
//        IStockService _stockService;
//        DeliveryDataset deliveryDataset;
//        DeliveryTable deliveryTable;


//        public DeliveryListForm()
//        {
//            InitializeComponent();
//            var serviceFactory = new ServiceFactory();
//            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
//            _stockService = serviceFactory.Resolve<IStockService>();

//            deliveryDataset = _deliveryService.GetMasterAll();

//            InitializeItemDataGirdView();


//        }

//        private void InitializeItemDataGirdView()
//        {
//            deliveryTable = deliveryDataset.DeliveryTable;
//            deliveryDataGrid.DataSource = deliveryTable;
//            deliveryDataGrid.ReadOnly = true;
//            deliveryDataGrid.Columns["Id"].Visible = false;
//            deliveryDataGrid.Columns["StockId"].Visible = false;
//            deliveryDataGrid.Columns["Number"].Visible = false;
//            deliveryDataGrid.Columns["Date"].Visible = false;
//            deliveryDataGrid.Columns["StockCode"].Visible = false;
//            deliveryDataGrid.Columns["StockName"].Visible = false;
//            deliveryDataGrid.AllowUserToAddRows = false;
//            deliveryDataGrid.AllowUserToDeleteRows = false;

//            var numberColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "numberColumn",
//                HeaderText = "شماره سند",
//                DataPropertyName = "Number",
//            };

//            deliveryDataGrid.Columns.Add(numberColumn);

//            var dateColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "dateColumn",
//                HeaderText = "تاریخ سند",
//                DataPropertyName = "Date",
//            };

//            deliveryDataGrid.Columns.Add(dateColumn);

//            var stockNameColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "stockNameColumn",
//                HeaderText = "انبار",
//                DataPropertyName = "StockName",
//            };

//            deliveryDataGrid.Columns.Add(stockNameColumn);


//            var stockCodeColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "stockCodeColumn",
//                HeaderText = "کد انبار",
//                DataPropertyName = "StockCode",
//            };

//            deliveryDataGrid.Columns.Add(stockCodeColumn);

//        }

//        private void DeliveryListForm_Load(object sender, EventArgs e)
//        {
//            //saveBtn.Enabled = false;
//            //MaximizeBox = false;
//        }

//        private void deliveryDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            var id = deliveryDataset.DeliveryTable[e.RowIndex].Id;
//            var stockId = deliveryDataset.DeliveryTable[e.RowIndex].StockId;
//            var stock = _stockService.GetById(stockId);
//            var stock_Id = stock.StockTable[0].Id;
//            var res = _deliveryService.GetByMasterId(id);
//            AddDeliveryForm addDeliveryForm = new AddDeliveryForm(res, stock_Id);
//            addDeliveryForm.WindowState = FormWindowState.Normal;
//            //addDeliveryForm.StartPosition = FormStartPosition.CenterParent;
//            addDeliveryForm.Show();
//        }

//        public override void refreshBtn_Click(object sender, EventArgs e)
//        {
//            deliveryDataset = _deliveryService.GetMasterAll();
//            deliveryDataGrid.DataSource = deliveryDataset.DeliveryTable;
//        }

//        public override void addBtn_Click(object sender, EventArgs e)
//        {
//            AddDeliveryForm addDeliveryForm = new AddDeliveryForm();
//            addDeliveryForm.Show();
//        }

//        public override void deleteBtn_Click(object sender, EventArgs e)
//        {
//            var selectedRows = deliveryDataGrid.SelectedRows;
//            if (selectedRows.Count > 0)
//            {
//                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

//                if (result == DialogResult.Yes)
//                {

//                    foreach (DataGridViewRow row in selectedRows)
//                    {
//                        var stockRow = (row.DataBoundItem as DataRowView)?.Row as DeliveryRow;
//                        if (stockRow != null)
//                        {
//                            var id = stockRow.Id;
//                            try
//                            {
//                                _deliveryService.DeleteById(id);
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
//        //        foreach (DataRow row in deliveryDataset.DeliveryTable.Select("", "", DataViewRowState.Deleted))
//        //        {
//        //            row.Delete();
//        //        }

//        //        _deliveryService.Save(deliveryDataset);
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
//            deliveryDataset = _deliveryService.GetMasterAll();
//            deliveryDataGrid.DataSource = deliveryDataset.DeliveryTable;
//        }
//    }
//}
