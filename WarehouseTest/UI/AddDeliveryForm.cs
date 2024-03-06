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
        private readonly ITableIdService _tableIdService;
        private readonly IDeliveryService _deliveryService;
        private readonly IStockService _stockService;

        private DeliveryDataset _deliveryDataset;
        private StockTable _stockTable;
        private ItemTable _itemTable;
        private DeliveryRow _newDeliveryRow;
        private int _deliveryId;
        private bool _validUiInput;

        #endregion

        #region Constructor

        public AddDeliveryForm()
        {
            InitializeComponent();
            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            _deliveryDataset = new DeliveryDataset();
            _newDeliveryRow = _deliveryDataset.DeliveryTable.GetNewRow();
            _deliveryId = _newDeliveryRow.Id;
            _deliveryDataset.DeliveryTable.Add(_newDeliveryRow);
            InitializeStockCombo();
            InitializeItemDataGridView();
            FormSetUp();
            BindData();
            _deliveryDataset.DeliveryTable[0].Date = DateTime.Now;

            //NumericTextBox persianDateTextBox = new NumericTextBox();
            //persianDateTextBox.Location = new System.Drawing.Point(12, 100);
            //this.Controls.Add(persianDateTextBox);
        }

        public AddDeliveryForm(int id) : base(id)
        {
            InitializeComponent();
            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            _deliveryDataset = _deliveryService.GetById(id);
            InitializeStockCombo();
            InitializeItemDataGridView();
            FormSetUp();
            _deliveryId = _deliveryDataset.DeliveryTable[0].Id;
            BindData();
            //stockCombo.DataBindings.Add("SelectedItem", _deliveryDataset.DeliveryTable[0], "StockId", true, DataSourceUpdateMode.OnPropertyChanged);
            var stockRow = _stockTable.FirstOrDefault(x => x.Id == _deliveryDataset.DeliveryTable[0].StockId);
            var stockRowIndex = _stockTable.Rows.IndexOf(stockRow);
            stockCombo.SelectedIndex = stockRowIndex;
        }

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


            //deliveryDatePicker.DataBindings.Add("Value", _deliveryDataset.DeliveryTable[0], "Date", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public AddDeliveryForm(DeliveryDataset deliveryDataset) : this()
        {
            _deliveryDataset = deliveryDataset;

            //InitializeItemDataGridView();
            //InitializeStockCombo();

            _deliveryId = deliveryDataset.DeliveryTable[0].Id;
            deliveryNumberTxt.Text = _deliveryDataset.DeliveryTable[0].Number.ToString();
            deliveryDatePicker.Value = _deliveryDataset.DeliveryTable[0].Date;
            itemDataGrid.DataSource = _deliveryDataset.DeliveryItemsTable;
            var stockRow = _stockTable.FirstOrDefault(x => x.Id == _deliveryDataset.DeliveryTable[0].StockId);
            var stockRowIndex = _stockTable.Rows.IndexOf(stockRow);
            stockCombo.SelectedIndex = stockRowIndex;

            //itemDataGrid.Columns["DeliveryId"].Visible = false;
            //itemDataGrid.Columns["Id"].Visible = false;
        }
        #endregion

        #region Initialization Methods

        private void InitializeStockCombo()
        {
            //var stockBinding = new BindingSource();
            _stockTable = _stockService.GetAll().StockTable;
            //stockBinding.DataSource = _stockTable;
            var stockDictionary = _stockTable.ToDictionary(row => row.Id, row => $"{row.Code} - {row.Name}");
            stockCombo.DataSource = new BindingSource(stockDictionary, null);
            stockCombo.DisplayMember = "Value";
            stockCombo.ValueMember = "Key";
            //stockCombo.DataSource = stockBinding;
            //stockCombo.DisplayMember = "Name";
            //stockCombo.ValueMember = "Id";
            stockCombo.SelectedItem = null;

            stockCombo.DataBindings.Add("SelectedItem", _deliveryDataset.DeliveryTable[0], "StockId", true, DataSourceUpdateMode.OnPropertyChanged);
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
            itemDataGrid.Enabled = false;
            addItemBtn.Enabled = false;
            stockCombo.SelectedItem = null;
            //deliveryDatePicker.Value = DateTime.Now;
            // 
            // receive all numbers convert it from persian date to georgian date            //_deliveryDataset = new DeliveryDataset();
            itemDataGrid.DataSource = _deliveryDataset.DeliveryItemsTable;

            itemDataGrid.Columns["DeliveryId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;



        }

        #endregion

        #region Event Handlers

        private void AddDeliveryForm_Load(object sender, EventArgs e)
        {
            //refreshBtn.Enabled = false;
            //addBtn.Enabled = false;
            //MaximizeBox = false;
        }

        public override void addBtn_Click(object sender, EventArgs e)
        {

            MainFormManager.AddFormToMainForm(new AddDeliveryForm());
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            _validUiInput = true;

            //_deliveryDataset.DeliveryItemsTable = (DeliveryItemsTable)itemDataGrid.DataSource;
            try
            {
                var selectedItem = stockCombo.SelectedItem;

                if (ValidateStockSelection(_deliveryDataset, selectedItem))
                {
                    _deliveryDataset.DeliveryTable[0].StockId = ((KeyValuePair<int, string>)selectedItem).Key;
                }
                else
                {
                    _validUiInput = false;
                }

                //_deliveryDataset.DeliveryTable[0].Date = deliveryDatePicker.Value;

                //var deliveryNumberValid = int.TryParse(deliveryNumberTxt.Text, out int deliveryNumber);

                //if (deliveryNumberValid && deliveryNumber > 0)
                //{
                //    _deliveryDataset.DeliveryTable[0].Number = deliveryNumber;
                //}
                //else
                //{
                //    ReceiptNumberIsNotValid();
                //    _validUiInput = false;
                //}

                if (_validUiInput)
                {
                    _deliveryService.Save(_deliveryDataset);
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

            //var selectedStock = stockCombo.SelectedItem as DataRowView;

            //if (selectedStock != null)
            //{
            //    int stockId = Convert.ToInt32(selectedStock["Id"]);
            //    _deliveryDataset.DeliveryTable[0].StockId = stockId;
            //}
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

            return true;
        }
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
/*
 * To bind input elements of your form to related data columns, you can use data binding. Here's an example for binding the controls in your `AddDeliveryForm` to the related data columns in the `DeliveryDataset`:

1. **Binding for Delivery Number (deliveryNumberTxt):**

```csharp
deliveryNumberTxt.DataBindings.Add("Text", _deliveryDataset.DeliveryTable[0], "Number", true, DataSourceUpdateMode.OnPropertyChanged);
```

2. **Binding for Delivery Date (deliveryDatePicker):**

```csharp
deliveryDatePicker.DataBindings.Add("Value", _deliveryDataset.DeliveryTable[0], "Date", true, DataSourceUpdateMode.OnPropertyChanged);
```

3. **Binding for Stock Combo (stockCombo):**

```csharp
stockCombo.DataBindings.Add("SelectedItem", _deliveryDataset.DeliveryTable[0], "StockId", true, DataSourceUpdateMode.OnPropertyChanged);
```

4. **Binding for DataGridView (itemDataGrid):**

Assuming the DataGridView is bound to `_deliveryDataset.DeliveryItemsTable`, you don't need to explicitly bind each column. The DataGridView will automatically reflect changes in the DataTable. However, if you want to customize the bindings for specific columns, you can do so by handling the `DataBindingComplete` event of the DataGridView.

```csharp
itemDataGrid.DataBindingComplete += (s, e) =>
{
    // Customize bindings for specific columns if needed
    // For example, binding for QuantityColumn:
    itemDataGrid.Columns["QuantityColumn"].DataPropertyName = "Quantity";
};
```

Note: Make sure the column names in the DataGridView match the column names in your DataTable (`DeliveryItemsTable`).

By using data binding, changes in your form's controls will automatically update the underlying data, and vice versa. Make sure to place these binding statements in an appropriate location, such as in the form's constructor or Load event, depending on when you want the bindings to be established.
 */
