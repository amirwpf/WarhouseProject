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
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.StockService;
using WarehouseTest.Services.TableIdService;
using WarehouseTest.UI.models;

namespace WarehouseTest.UI
{
    public partial class AddDeliveryForm : BaseForm
    {
        ITableIdService tableIdService;
        DeliveryDataset deliveryDataset;
        IDeliveryService deliveryService;
        IItemService itemService;
        StockTable stockTable;
        ItemTable itemTable;
        DeliveryRow newDeliveryRow;
        int deliveryId;
        IStockService stockService;
        public AddDeliveryForm()
        {
            InitializeComponent();
            tableIdService = new TableIdService();
            itemService = new ItemService();
            stockService = new StockService();
            deliveryService = new DeliveryService();
            InitializeStockCombo();
            InitializeItemDataGirdView();
            FormSetUp();
        }

        public AddDeliveryForm(DeliveryDataset _deliveryDataset, int stockId)
        {
            InitializeComponent();
            deliveryDataset = _deliveryDataset;
            itemService = new ItemService();
            deliveryService = new DeliveryService();
            tableIdService = new TableIdService();
            stockService = new StockService();
            InitializeItemDataGirdView();
            InitializeStockCombo();

            var deliveryRow = deliveryDataset.DeliveryTable[0];
            deliveryId = deliveryRow.Id;
            deliveryNumberTxt.Text = deliveryRow.Number.ToString();
            deliveryDatePicker.Value = deliveryDataset.DeliveryTable[0].Date;
            itemDataGrid.DataSource = _deliveryDataset.DeliveryItemsTable;
            var stockRow = stockTable.FirstOrDefault(x => x.Id == stockId);
            var stockRowIndex = stockTable.Rows.IndexOf(stockRow);
            stockCombo.SelectedIndex = stockRowIndex;

            itemDataGrid.Columns["DeliveryId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;

        }

        private void InitializeStockCombo()
        {
            stockTable = stockService.GetAll().StockTable;
            stockCombo.DataSource = stockTable;
            stockCombo.DisplayMember = "Name";
            stockCombo.ValueMember = "Id";
            stockCombo.SelectedItem = null;
        }

        private void InitializeItemDataGirdView()
        {

            itemTable = itemService.GetAll().ItemTable;
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
            deliveryDatePicker.Value = DateTime.Now;

            deliveryDataset = new DeliveryDataset();
            itemDataGrid.DataSource = deliveryDataset.DeliveryItemsTable;

            itemDataGrid.Columns["DeliveryId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;
            newDeliveryRow = deliveryDataset.DeliveryTable.GetNewRow();
            newDeliveryRow.Id = tableIdService.GetId(DbTablesEnum.delivery);
            deliveryId = newDeliveryRow.Id;
            deliveryDataset.DeliveryTable.Add(newDeliveryRow);
        }

        private void AddDeliveryForm_Load(object sender, EventArgs e)
        {
            refreshBtn.Enabled = false;
            addBtn.Enabled = false;
            //MaximizeBox = false;
        }

        internal override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = stockCombo.SelectedItem;
                deliveryService.Save(deliveryDataset, selectedItem, deliveryNumberTxt.Text, deliveryDatePicker.Value);
                MessageBox.Show("ذخیره با موفقیت صورت گردید");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal override void deleteBtn_Click(object sender, EventArgs e)
        {
            var selectedRows = itemDataGrid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var deliveryItem = (row.DataBoundItem as DataRowView)?.Row as DeliveryItemsRow;
                        if (deliveryItem != null)
                        {
                            try
                            {
                                deliveryItem.Delete();
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
            var newReceiptItemRow = deliveryDataset.DeliveryItemsTable.GetNewRow();
            newReceiptItemRow.Id = tableIdService.GetId(DbTablesEnum.deliveryItems);
            newReceiptItemRow.DeliveryId = deliveryId;
            newReceiptItemRow.Quantity = 0;
            if (itemTable.Rows.Count > 0)
            {
                newReceiptItemRow.ItemId = (int)itemTable.Rows[0]["Id"];
            }
            deliveryDataset.DeliveryItemsTable.Add(newReceiptItemRow);
        }

        private void stockCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemDataGrid.Enabled = true;
            addItemBtn.Enabled = true;
        }
    }
}
