﻿//using App.Domin.Core;
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
//using WarehouseTest.forms;
//using WarehouseTest.Services.StockService;

//namespace WarehouseTest.UI
//{
//    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "انبار" , Order = 4)]
//    public partial class StockListForm : ListBaseForm , IMenuExtension
//    {
//        StockDataSet stockDataSet;
//        private readonly IStockService _stockService;
//        StockTable stockTable;

//        public StockListForm()
//        {
//            InitializeComponent();
//            stockDataSet = new StockDataSet();

//            var serviceFactory = new ServiceFactory();
//            _stockService = serviceFactory.Resolve<IStockService>();

//            stockDataSet = _stockService.GetAll();



//            InitializeItemDataGirdView();

//        }

//        private void InitializeItemDataGirdView()
//        {
//            stockTable = stockDataSet.StockTable;
//            stockDataGrid.AllowUserToAddRows = false;
//            stockDataGrid.DataSource = stockDataSet.StockTable;
//            stockDataGrid.AllowUserToAddRows = false;
//            stockDataGrid.AllowUserToDeleteRows = false;
//            stockDataGrid.Columns["Id"].Visible = false;
//            stockDataGrid.Columns["Code"].Visible = false;
//            stockDataGrid.Columns["Name"].Visible = false;

//            var codeColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "codeColumn",
//                HeaderText = "کد",
//                DataPropertyName = "Code",
//            };

//            stockDataGrid.Columns.Add(codeColumn);

//            var nameColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "nameColumn",
//                HeaderText = "نام",
//                DataPropertyName = "Name",
//            };

//            stockDataGrid.Columns.Add(nameColumn);
//        }

//        private void StockListForm_Load(object sender, EventArgs e)
//        {
//            //saveBtn.Enabled = false;
//            //MaximizeBox = false;
//        }

//        public override void refreshBtn_Click(object sender, EventArgs e)
//        {
//            stockDataSet = _stockService.GetAll();
//            stockDataGrid.DataSource = stockDataSet.StockTable;
//        }

//        public override void addBtn_Click(object sender, EventArgs e)
//        {
//            AddStockForm addStockForm = new AddStockForm();
//            addStockForm.Show();
//        }

//        public override void deleteBtn_Click(object sender, EventArgs e)
//        {
//            var selectedRows = stockDataGrid.SelectedRows;
//            if (selectedRows.Count > 0)
//            {
//                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

//                if (result == DialogResult.Yes)
//                {

//                    foreach (DataGridViewRow row in selectedRows)
//                    {
//                        var stockRow = (row.DataBoundItem as DataRowView)?.Row as StockRow;
//                        if (stockRow != null)
//                        {
//                            var id = stockRow.Id;
//                            try
//                            {
//                                _stockService.DeleteById(id);
//                                MessageBox.Show("آیتم با موفقیت حذف گردید");
//                                RefreshDataGrid();
//                            }
//                            catch
//                            {
//                                MessageBox.Show(ErrorMessage.ItemCantBeDeleted(stockRow.Name), "خطا");
//                            }
//                        }
//                    }
//                }
//            }
//        }

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
//            stockDataSet = _stockService.GetAll();
//            stockDataGrid.DataSource = stockDataSet.StockTable;
//        }

//        private void stockDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.RowIndex < stockDataGrid.Rows.Count)
//            {
//                DataRowView selectedRow = (DataRowView)stockDataGrid.Rows[e.RowIndex].DataBoundItem;


//                if (selectedRow != null && selectedRow.Row.RowState != DataRowState.Deleted)
//                {

//                    var id = (int)selectedRow["Id"];
//                    var code = (int)selectedRow["Code"];
//                    var name = (string)selectedRow["Name"];

//                    AddStockForm addStockForm = new AddStockForm(id, code, name);
//                    addStockForm.WindowState = FormWindowState.Normal;
//                    addStockForm.ShowDialog();
//                }
//            }
//        }
//    }
//}
