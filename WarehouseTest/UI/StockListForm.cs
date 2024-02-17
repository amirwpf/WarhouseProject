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
using Warehouse.Framework.Common;
using WarehouseTest.forms;
using WarehouseTest.Services.StockService;
using WarehouseTest.UI.models;

namespace WarehouseTest.UI
{
    public partial class StockListForm : BaseForm
    {
        StockDataSet stockDataSet;
        private readonly IStockService _stockService;
        StockTable stockTable;
        public StockListForm()
        {
            InitializeComponent();
            stockDataSet = new StockDataSet();
            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IStockService, StockService>();
            _stockService = proxyFactory.Resolve<IStockService>();
            stockDataSet = _stockService.GetAll();



            InitializeItemDataGirdView();

        }

        private void InitializeItemDataGirdView()
        {
            stockTable = stockDataSet.StockTable;
            stockDataGrid.AllowUserToAddRows = false;
            stockDataGrid.DataSource = stockDataSet.StockTable;
            stockDataGrid.AllowUserToAddRows = false;
            stockDataGrid.AllowUserToDeleteRows = false;
            stockDataGrid.Columns["Id"].Visible = false;
            stockDataGrid.Columns["Code"].Visible = false;
            stockDataGrid.Columns["Name"].Visible = false;

            var codeColumn = new DataGridViewTextBoxColumn
            {
                Name = "codeColumn",
                HeaderText = "کد",
                DataPropertyName = "Code",
            };

            stockDataGrid.Columns.Add(codeColumn);

            var nameColumn = new DataGridViewTextBoxColumn
            {
                Name = "nameColumn",
                HeaderText = "نام",
                DataPropertyName = "Name",
            };

            stockDataGrid.Columns.Add(nameColumn);
        }

        private void StockListForm_Load(object sender, EventArgs e)
        {
            //MaximizeBox = false;
        }

        internal override void refreshBtn_Click(object sender, EventArgs e)
        {
            stockDataSet = _stockService.GetAll();
            stockDataGrid.DataSource = stockDataSet.StockTable;
        }

        internal override void addBtn_Click(object sender, EventArgs e)
        {
            AddStockForm addStockForm = new AddStockForm();
            addStockForm.Show();
        }

        internal override void deleteBtn_Click(object sender, EventArgs e)
        {
            var selectedRows = stockDataGrid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var stockRow = (row.DataBoundItem as DataRowView)?.Row as StockRow;
                        if (stockRow != null)
                        {
                            var id = stockRow.Id;
                            try
                            {
                                _stockService.DeleteById(id);
                                MessageBox.Show("آیتم با موفقیت حذف گردید");
                                RefreshDataGrid();
                            }
                            catch
                            {
                                MessageBox.Show(ErrorMessage.ItemCantBeDeleted(stockRow.Name), "خطا");
                            }
                        }
                    }
                }
            }
        }

        internal override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in stockDataSet.StockTable.Select("", "", DataViewRowState.Deleted))
                {
                    row.Delete();
                }

                _stockService.Save(stockDataSet);
                MessageBox.Show("ذخیره با موفقیت صورت گردید");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private DialogResult ShowConfirmationMessageBox(string message)
        {
            DialogResult result = MessageBox.Show(
                message,
                "تایید حذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            return result;
        }

        public void RefreshDataGrid()
        {
            stockDataSet = _stockService.GetAll();
            stockDataGrid.DataSource = stockDataSet.StockTable;
        }
    }
}
