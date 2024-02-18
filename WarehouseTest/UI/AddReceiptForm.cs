using App.Domin.Core.Contracts.ServiceInterface;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Framework.Common;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.ReceiptService;
using WarehouseTest.Services.StockService;
using WarehouseTest.Services.TableIdService;
using WarehouseTest.UI.models;

namespace WarehouseTest
{
    public partial class AddReceiptForm : BaseForm
    {
        private readonly ITableIdService _tableIdService;
        private readonly IReceiptService _receiptService;
        private readonly IItemService _itemService;
        private readonly IStockService _stockService;
        ReceiptDataset receiptDataset;
        StockTable stockTable;
        ItemTable itemTable;
        ReceiptRow newReceiptRow;
        int receiptId;

        public AddReceiptForm()
        {
            InitializeComponent();

            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IItemService, ItemService>();
            proxyFactory.Register<ITableIdService, TableIdService>();
            proxyFactory.Register<IReceiptService, ReceiptService>();
            proxyFactory.Register<IStockService, StockService>();
            _itemService = proxyFactory.Resolve<IItemService>();
            _tableIdService = proxyFactory.Resolve<ITableIdService>();
            _receiptService = proxyFactory.Resolve<IReceiptService>();
            _stockService = proxyFactory.Resolve<IStockService>();

            InitializeStockCombo();
            InitializeItemDataGirdView();
            FormSetUp();
        }

        public AddReceiptForm(ReceiptDataset _receiptDataset, int stockId)
        {
            InitializeComponent();
            receiptDataset = _receiptDataset;

            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IItemService, ItemService>();
            proxyFactory.Register<ITableIdService, TableIdService>();
            proxyFactory.Register<IReceiptService, ReceiptService>();
            proxyFactory.Register<IStockService, StockService>();
            _itemService = proxyFactory.Resolve<IItemService>();
            _tableIdService = proxyFactory.Resolve<ITableIdService>();
            _receiptService = proxyFactory.Resolve<IReceiptService>();
            _stockService = proxyFactory.Resolve<IStockService>();

            InitializeItemDataGirdView();
            InitializeStockCombo();

            var receiptRow = receiptDataset.ReceiptTable[0];
            receiptId = receiptRow.Id;
            receiptNumberTxt.Text = receiptRow.Number.ToString();
            receiptDatePicker.Value = receiptDataset.ReceiptTable[0].Date;
            itemDataGrid.DataSource = _receiptDataset.ReceiptItemsTable;
            var stockRow = stockTable.FirstOrDefault(x => x.Id == stockId);
            var stockRowIndex = stockTable.Rows.IndexOf(stockRow);
            stockCombo.SelectedIndex = stockRowIndex;



            itemDataGrid.Columns["Id"].Visible = false;
            itemDataGrid.Columns["ReceiptId"].Visible = false;


            //Button deleteBtn = new Button();
            //deleteBtn.Text = "X";
            //deleteBtn.Size = new Size(25, 25);
            //deleteBtn.Location = new System.Drawing.Point(75, 0);
            //deleteBtn.BackColor = System.Drawing.Color.Crimson;
            //deleteBtn.Click += new System.EventHandler(deleteBtn_Click);
            //panel1.Controls.Add(deleteBtn);
        }

        private void InitializeStockCombo()
        {
            stockTable = _stockService.GetAll().StockTable;
            stockCombo.DataSource = stockTable;
            stockCombo.DisplayMember = "Name";
            stockCombo.ValueMember = "Id";
            stockCombo.SelectedItem = null;
        }

        private void InitializeItemDataGirdView()
        {

            itemTable = _itemService.GetAll().ItemTable;
            itemDataGrid.AllowUserToAddRows = false;
            itemDataGrid.AllowUserToDeleteRows = false;

            var comboBoxColumn = new DataGridViewComboBoxColumn
            {
                Name = "IdColumn",
                HeaderText = "کالا",
                DataPropertyName = "ItemId",
                DisplayMember = "Name",
                ValueMember = "Id",
            };

            itemDataGrid.Columns.Add(comboBoxColumn);

            var textBoxColumn = new DataGridViewTextBoxColumn
            {
                Name = "QuantityColumn",
                HeaderText = "تعداد",
                DataPropertyName = "Quantity",
            };

            itemDataGrid.Columns.Add(textBoxColumn);
            comboBoxColumn.DataSource = itemTable;
        }

        private void FormSetUp()
        {
            itemDataGrid.Enabled = false;
            addItemBtn.Enabled = false;
            stockCombo.SelectedItem = null;
            receiptDatePicker.Value = DateTime.Now;

            receiptDataset = new ReceiptDataset();
            itemDataGrid.DataSource = receiptDataset.ReceiptItemsTable;

            itemDataGrid.Columns["ReceiptId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;
            newReceiptRow = receiptDataset.ReceiptTable.GetNewRow();
            newReceiptRow.Id = _tableIdService.GetId(DbTablesEnum.receipt);
            receiptId = newReceiptRow.Id;
            receiptDataset.ReceiptTable.Add(newReceiptRow);
        }

        private void AddReceiptForm_Load(object sender, EventArgs e)
        {
            refreshBtn.Enabled = false;
            addBtn.Enabled = false;
            //MaximizeBox = false;
        }

        internal override void deleteBtn_Click(object sender, EventArgs e)
        {
            var selectedRows = itemDataGrid.SelectedRows;
            if(selectedRows.Count>0)
            {
                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var receiptItem = (row.DataBoundItem as DataRowView)?.Row as ReceiptItemsRow;
                        if (receiptItem != null)
                        {
                            try
                            {
                                receiptItem.Delete();
                                //MessageBox.Show("آیتم با موفقیت حذف گردید");
                            }
                            catch
                            {
                                MessageBox.Show("خطا در حذف آیتم", "خطا");
                            }
                        }
                    }
                }
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

        

        private void addItemBtn_Click(object sender, EventArgs e)
        {
            var newReceiptItemRow = receiptDataset.ReceiptItemsTable.GetNewRow();
            newReceiptItemRow.Id = _tableIdService.GetId(DbTablesEnum.receiptItem);
            newReceiptItemRow.ReceiptId = receiptId;
            newReceiptItemRow.Quantity = 0;
            if (itemTable.Rows.Count > 0)
            {
                newReceiptItemRow.ItemId = (int)itemTable.Rows[0]["Id"];
            }
            receiptDataset.ReceiptItemsTable.Add(newReceiptItemRow);
        }


        internal override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = stockCombo.SelectedItem;
                _receiptService.Save(receiptDataset, selectedItem, receiptNumberTxt.Text, receiptDatePicker.Value);
                MessageBox.Show("ذخیره با موفقیت صورت گردید");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void stockCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemDataGrid.Enabled = true;
            addItemBtn.Enabled = true;
        }

        private void itemDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var boolRes = int.TryParse(receiptDataset.ReceiptItemsTable[e.RowIndex].Quantity.ToString(), out int res);
        }

        private void itemDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 3 )//&& e.Context == DataGridViewDataErrorContexts.Formatting)
            {
                MessageBox.Show("مقدار تعداد ناصحیح می باشد");
                e.ThrowException = false;
                receiptDataset.ReceiptItemsTable[e.RowIndex].Quantity = 0;
                e.Cancel = true;
            }
        }
    }
}
