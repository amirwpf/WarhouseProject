#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.UI;
using App.Framework.UI.Model;
using Core.Entites;
using Warehouse.Framework.UI;
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.StockService;

#endregion

namespace WarehouseTest.UI
{
    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "خروج انبار", Order = 5)]
    public partial class AddDeliveryForm : EntityBaseForm, IMenuExtension
    {
        #region Fields
        private readonly IItemService _itemService;
        private readonly IDeliveryService _deliveryService;
        private readonly IStockService _stockService;

        private DeliveryDataset _deliveryDataset;
        private StockTable _stockTable;
        private ItemTable _itemTable;
        private DeliveryRow _newDeliveryRow;
        private int _deliveryId;

        #endregion

        #region Constructor

        public AddDeliveryForm()
        {
            InitializeComponent();
            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            _deliveryDataset = new DeliveryDataset();
            _newDeliveryRow = _deliveryDataset.DeliveryTable.GetNewRow();
            _deliveryId = _newDeliveryRow.Id;
            _deliveryDataset.DeliveryTable.Add(_newDeliveryRow);
            itemDataGrid.Enabled = false;
            addItemBtn.Enabled = false;
            stockCombo.SelectedItem = null;
            _deliveryDataset.DeliveryTable[0].Date = DateTime.Now;
            InitializeItemDataGridView();
            FormSetUp();
            InitializeStockCombo();
            BindData();

            PersianDateTextBox persianDateTextBox = new PersianDateTextBox();
            persianDateTextBox.Location = new System.Drawing.Point(12, 110);
            this.Controls.Add(persianDateTextBox);
        }

        public AddDeliveryForm(int id) : base(id)
        {
            InitializeComponent();
            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            _deliveryDataset = _deliveryService.GetById(id);
            InitializeItemDataGridView();
            FormSetUp();
            InitializeStockCombo();
            _deliveryId = _deliveryDataset.DeliveryTable[0].Id;
            BindData();
        }


        #endregion

        #region Initialization Methods
        private void BindData()
        {
            Binding binding = new Binding("Text", _deliveryDataset.DeliveryTable[0], "Number", true, DataSourceUpdateMode.OnPropertyChanged);

            binding.Format += (sender, e) =>
            {
                if (e.Value != null && e.Value != DBNull.Value && int.TryParse(e.Value.ToString(), out int intValue))
                {
                    e.Value = intValue.ToString();
                }
            };

            binding.Parse += (sender, e) =>
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int intValue))
                {
                    e.Value = intValue;
                }
                else
                {
                    e.Value = 0;
                }
            };

            deliveryNumberTxt.DataBindings.Add(binding);

            Binding bindingDate = new Binding("Value", _deliveryDataset.DeliveryTable[0], "Date", true, DataSourceUpdateMode.OnPropertyChanged);

            bindingDate.Format += (sender, e) =>
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = DateTime.Now;
                }
            };

            deliveryDatePicker.DataBindings.Add(bindingDate);
        }

        private void InitializeStockCombo()
        {
            stockCombo.SelectedIndexChanged -= stockCombo_SelectedIndexChanged;
            _stockTable = _stockService.GetAll().StockTable;

            var stockDictionary = _stockTable.ToDictionary(row => row.Id, row => $"{row.Code} - {row.Name}");
            BindingSource stockBinding = new BindingSource(stockDictionary, null);

            stockCombo.DataSource = stockBinding;
            stockCombo.DisplayMember = "Value";
            stockCombo.ValueMember = "Key";

            int initialStockId = _deliveryDataset.DeliveryTable[0].StockId;
            stockCombo.SelectedValue = initialStockId;

            stockCombo.DataBindings.Add("SelectedValue", _deliveryDataset.DeliveryTable[0], "StockId", true, DataSourceUpdateMode.OnPropertyChanged);
            stockCombo.SelectedIndexChanged += stockCombo_SelectedIndexChanged;
        }


        private void InitializeItemDataGridView()
        {
            _itemTable = _itemService.GetAll().ItemTable;
            itemDataGrid.AllowUserToAddRows = false;
            itemDataGrid.AllowUserToDeleteRows = false;

            var comboBoxColumn = new DataGridViewComboBoxColumn
            {
                Name = "IdColumn",
                HeaderText = "کالا",
                DataPropertyName = "ItemId",
                DisplayMember = "Name",
                ValueMember = "Id",
                DataSource = _itemTable
            };

            itemDataGrid.Columns.Add(comboBoxColumn);

            var textBoxColumn = new DataGridViewTextBoxColumn
            {
                Name = "QuantityColumn",
                HeaderText = "تعداد",
                DataPropertyName = "Quantity",
            };

            itemDataGrid.Columns.Add(textBoxColumn);
        }

        private void FormSetUp()
        {
            itemDataGrid.DataSource = _deliveryDataset.DeliveryItemsTable;

            itemDataGrid.Columns["DeliveryId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;
        }

        #endregion

        #region Event Handlers

        private void AddDeliveryForm_Load(object sender, EventArgs e)
        {
        }

        public override void addBtn_Click(object sender, EventArgs e)
        {
            MainFormManager.AddFormToMainForm(new AddDeliveryForm());
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _deliveryService.Save(_deliveryDataset);
                MessageBox.Show("ذخیره با موفقیت صورت گردید");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = ShowConfirmationMessageBox("سند خروج حذف گردد؟");
            if (result == DialogResult.Yes)
            {
                _deliveryService.DeleteById(_deliveryId);
                this.Close();
            }
        }

        private void addItemBtn_Click(object sender, EventArgs e)
        {
            var newReceiptItemRow = _deliveryDataset.DeliveryItemsTable.GetNewRow();
            newReceiptItemRow.DeliveryId = _deliveryId;
            newReceiptItemRow.Quantity = 0;
            if (_itemTable.Rows.Count > 0)
            {
                newReceiptItemRow.ItemId = (int)_itemTable.Rows[0]["Id"];
            }
            _deliveryDataset.DeliveryItemsTable.Add(newReceiptItemRow);
        }

        private void stockCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemDataGrid.Enabled = true;
            addItemBtn.Enabled = true;
        }

        private void itemDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                MessageBox.Show("مقدار تعداد ناصحیح می باشد");
                e.ThrowException = false;
                _deliveryDataset.DeliveryItemsTable[e.RowIndex].Quantity = 0;
                e.Cancel = true;
            }
        }

        #endregion

        #region Helper Methods

        private DialogResult ShowConfirmationMessageBox(string message)
        {
            return MessageBox.Show(
                message,
                "تایید حذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );
        }

        #endregion

        private void deleteItemBtn_Click(object sender, EventArgs e)
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

    }
}