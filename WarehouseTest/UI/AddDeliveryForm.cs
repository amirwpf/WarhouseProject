using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.UI;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.StockService;
using WarehouseTest.Services.TableIdService;


namespace WarehouseTest.UI
{
    [ExtentionMenu(CategoryName = "Warehouse" , MenuName = "خروج انبار جدید" , Order =5)]
    public partial class AddDeliveryForm : BaseForm, IMenuExtension
    {
        private readonly IItemService _itemService;
        private readonly ITableIdService _tableIdService;
        private readonly IDeliveryService _deliveryService;
        private readonly IStockService _stockService;

        DeliveryDataset deliveryDataset;
        
        StockTable stockTable;
        ItemTable itemTable;
        DeliveryRow newDeliveryRow;
        int deliveryId;

        private bool validUiInput;


        public AddDeliveryForm()
        {
            InitializeComponent();
            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            InitializeStockCombo();
            InitializeItemDataGirdView();
            FormSetUp();
        }

        public AddDeliveryForm(DeliveryDataset _deliveryDataset, int stockId)
        {
            InitializeComponent();
            deliveryDataset = _deliveryDataset;

            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
            _stockService = serviceFactory.Resolve<IStockService>();

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
            deliveryDatePicker.Value = DateTime.Now;

            deliveryDataset = new DeliveryDataset();
            itemDataGrid.DataSource = deliveryDataset.DeliveryItemsTable;

            itemDataGrid.Columns["DeliveryId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;
            newDeliveryRow = deliveryDataset.DeliveryTable.GetNewRow();
            newDeliveryRow.Id = _tableIdService.GetId(DbTablesEnum.delivery);
            deliveryId = newDeliveryRow.Id;
            deliveryDataset.DeliveryTable.Add(newDeliveryRow);
        }

        private void AddDeliveryForm_Load(object sender, EventArgs e)
        {
            refreshBtn.Enabled = false;
            addBtn.Enabled = false;
            //MaximizeBox = false;
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            validUiInput = true;
            try
            {
                var selectedItem = stockCombo.SelectedItem;
                if (ValidateStockSelection(deliveryDataset, selectedItem))
                {
                    deliveryDataset.DeliveryTable[0].StockId = ((DataRowView)selectedItem).Row.Field<int>("Id");
                }
                else
                {
                    validUiInput = false;
                }
                deliveryDataset.DeliveryTable[0].Date = deliveryDatePicker.Value;

                var deliveryNumberValid = int.TryParse(deliveryNumberTxt.Text, out int deliveryNumber);
                if (deliveryNumberValid && deliveryNumber > 0)
                {
                    deliveryDataset.DeliveryTable[0].Number = deliveryNumber;
                }
                else
                {
                    ReceiptNumberIsNotValid();
                    validUiInput = false;
                }

                if (validUiInput)
                {
                    _deliveryService.Save(deliveryDataset);
                    MessageBox.Show("ذخیره با موفقیت صورت گردید");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void deleteBtn_Click(object sender, EventArgs e)
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
            newReceiptItemRow.Id = _tableIdService.GetId(DbTablesEnum.deliveryItems);
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

        private void itemDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 3)//&& e.Context == DataGridViewDataErrorContexts.Formatting)
            {
                MessageBox.Show("مقدار تعداد ناصحیح می باشد");
                e.ThrowException = false;
                deliveryDataset.DeliveryItemsTable[e.RowIndex].Quantity = 0;
                e.Cancel = true;
            }
        }


        private void ReceiptNumberIsNotValid()
        {
            MessageBox.Show("مقدار شماره سند خروج ناصحیح می باشد");
            deliveryNumberTxt.Text = "0";
        }

        private bool ValidateStockSelection(DeliveryDataset deliveryDataset, object selectedItem)
        {
            if (selectedItem == null)
            {
                MessageBox.Show(ErrorMessage.InValidFieldValue("انبار"));
                return false;
            }

            if (!(selectedItem is DataRowView rowView))
            {
                MessageBox.Show(ErrorMessage.InValidFieldValue("انبار"));
                return false;
            }

            DataRow row = rowView.Row;

            if (row == null)
            {
                MessageBox.Show(ErrorMessage.InValidFieldValue("انبار"));
                return false;
            }

            if (!row.Table.Columns.Contains("Id"))
            {
                MessageBox.Show(ErrorMessage.InValidFieldValue("انبار"));
                return false;
            }

            deliveryDataset.DeliveryTable[0].StockId = row.Field<int>("Id");
            return true;
        }

    }
}