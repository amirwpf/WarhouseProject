#region using

using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.UI;
using App.Framework.UI.Model;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Framework.UI;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.ReceiptService;
using WarehouseTest.Services.StockService;
#endregion


namespace WarehouseTest
{
    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "ورود انبار", Order = 7)]
    public partial class AddReceiptForm : EntityBaseForm, IMenuExtension
    {

        #region Fields
        private readonly IReceiptService _receiptService;
        private readonly IItemService _itemService;
        private readonly IStockService _stockService;
        private ReceiptDataset _receiptDataset;
        private StockTable _stockTable;
        private ItemTable _itemTable;
        private ReceiptRow _newReceiptRow;
        private int _receiptId;
        private bool _validDate;
        #endregion


        #region Constructor
        public AddReceiptForm()
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _receiptService = serviceFactory.Resolve<IReceiptService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            _receiptDataset = new ReceiptDataset();
            _newReceiptRow = _receiptDataset.ReceiptTable.GetNewRow();
            _receiptId = _newReceiptRow.Id;
            _receiptDataset.ReceiptTable.Add(_newReceiptRow);
            itemDataGrid.Enabled = false;
            addItemBtn.Enabled = false;
            stockCombo.SelectedItem = null;
            _receiptDataset.ReceiptTable[0].Date = DateTime.Now;
            InitializeItemDataGirdView();
            FormSetUp();
            InitializeStockCombo();
            BindData();
        }

        public AddReceiptForm(int id) : base(id)
        {
            InitializeComponent();
            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _receiptService = serviceFactory.Resolve<IReceiptService>();
            _stockService = serviceFactory.Resolve<IStockService>();
            _receiptDataset = _receiptService.GetById(id);
            InitializeItemDataGirdView();
            FormSetUp();
            InitializeStockCombo();
            _receiptId = _receiptDataset.ReceiptTable[0].Id;
            BindData();
        }

        #endregion

        #region Initialization Methods

        private void BindData()
        {
            Binding binding = new Binding("Text", _receiptDataset.ReceiptTable[0], "Number", true, DataSourceUpdateMode.OnPropertyChanged);

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

            receiptNumberTxt.DataBindings.Add(binding);

            Binding bindingDate = new Binding("Date", _receiptDataset.ReceiptTable[0], "Date", true, DataSourceUpdateMode.OnPropertyChanged);

            bindingDate.Format += (sender, e) =>
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = DateTime.Now;
                }
            };

            persianDate.DataBindings.Add(bindingDate);
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

            int initialStockId = _receiptDataset.ReceiptTable[0].StockId;
            stockCombo.SelectedValue = initialStockId;

            stockCombo.DataBindings.Add("SelectedValue", _receiptDataset.ReceiptTable[0], "StockId", true, DataSourceUpdateMode.OnPropertyChanged);
            stockCombo.SelectedIndexChanged += stockCombo_SelectedIndexChanged;
        }

        private void InitializeItemDataGirdView()
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
            };

            itemDataGrid.Columns.Add(comboBoxColumn);

            var textBoxColumn = new DataGridViewTextBoxColumn
            {
                Name = "QuantityColumn",
                HeaderText = "تعداد",
                DataPropertyName = "Quantity",
            };

            itemDataGrid.Columns.Add(textBoxColumn);
            comboBoxColumn.DataSource = _itemTable;
        }

        private void FormSetUp()
        {
            itemDataGrid.DataSource = _receiptDataset.ReceiptItemsTable;

            itemDataGrid.Columns["ReceiptId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;

            _validDate = true;
        }
        #endregion

        private void AddReceiptForm_Load(object sender, EventArgs e)
        {
        }

        public override void addBtn_Click(object sender, EventArgs e)
        {
            MainFormManager.AddFormToMainForm(new AddReceiptForm());
        }

        public override void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = ShowConfirmationMessageBox("سند ورورد حذف گردد؟");
            if (result == DialogResult.Yes)
            {
                try
                {
                    _receiptDataset.ReceiptTable[0].Delete();
                    if (_receiptDataset.ReceiptTable.Rows.Count > 0)
                    {
                        _receiptService.DeleteWithcheckVersion(_receiptDataset, _receiptDataset.ReceiptTable[0]);
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
            var newReceiptItemRow = _receiptDataset.ReceiptItemsTable.GetNewRow();
            newReceiptItemRow.ReceiptId = _receiptId;
            newReceiptItemRow.Quantity = 0;
            if (_itemTable.Rows.Count > 0)
            {
                newReceiptItemRow.ItemId = (int)_itemTable.Rows[0]["Id"];
            }
            _receiptDataset.ReceiptItemsTable.Add(newReceiptItemRow);
        }


        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            ValidateDate(persianDate);
            if (_validDate)
            {
                try
                {
                    _receiptService.Save(_receiptDataset);
                    MessageBox.Show("ذخیره با موفقیت صورت گردید");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void ValidateDate(PersianDateTextBox persianDate)
        {
            if (!persianDate.ValidDate)
            {
                MessageBox.Show(ErrorMessage.InValidFieldValue("تاریخ"));
                _validDate = false;
            }
            else
            {
                _validDate = true;
            }
        }


        private void stockCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemDataGrid.Enabled = true;
            addItemBtn.Enabled = true;
        }

        private void itemDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var boolRes = int.TryParse(_receiptDataset.ReceiptItemsTable[e.RowIndex].Quantity.ToString(), out int res);
        }

        private void itemDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                MessageBox.Show("مقدار تعداد ناصحیح می باشد");
                e.ThrowException = false;
                _receiptDataset.ReceiptItemsTable[e.RowIndex].Quantity = 0;
                e.Cancel = true;
            }
        }

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
                        var receiptItem = (row.DataBoundItem as DataRowView)?.Row as ReceiptItemsRow;
                        if (receiptItem != null)
                        {
                            try
                            {
                                receiptItem.Delete();
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
