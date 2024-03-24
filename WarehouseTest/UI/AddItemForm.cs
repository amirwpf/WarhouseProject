using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.UI;
using App.Framework.UI.Model;
using Core.Entites;
using System;
using System.Data;
using System.Windows.Forms;
using Warehouse.Framework.UI;
using WarehouseTest.Services.ItemService;
using WarehouseTest.UI;

namespace WarehouseTest.forms
{
    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "کالا جدید", Order = 1)]
    public partial class AddItemForm : EntityBaseForm, IMenuExtension
    {
        private readonly IItemService _itemService;
        private ItemDataSet _itemDataset;

        public AddItemForm()
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();

            _itemDataset = new ItemDataSet();
            var newRow = _itemDataset.ItemTable.GetNewRow();
            _itemDataset.ItemTable.Add(newRow);
            BindData();
            _inputId = 0;
        }



        public AddItemForm(int id) : base(id)
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _itemDataset = _itemService.GetById(id);
            BindData();
            _inputId = id;
        }


        private void BindData()
        {
            itemNameTx.DataBindings.Add("Text", _itemDataset.ItemTable[0], "Name");
            Binding binding = new Binding("Text", _itemDataset.ItemTable[0], "Code", true, DataSourceUpdateMode.OnPropertyChanged);

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

            itemCodeTxt.DataBindings.Add(binding);
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
        }

        public override void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = ShowConfirmationMessageBox("سند خروج حذف گردد؟");
            if (result == DialogResult.Yes)
            {
                try
                {
                    _itemDataset.ItemTable[0].Delete();
                    if (_itemDataset.ItemTable.Rows.Count > 0)
                    {
                        _itemService.DeleteWithcheckVersion(_itemDataset, _itemDataset.ItemTable[0]);
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

        public override void addBtn_Click(object sender, EventArgs e)
        {
            MainFormManager.AddFormToMainForm(new AddItemForm());
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _itemService.Save(_itemDataset);
                MessageBox.Show("کالا با موفقیت ذخیره گردید");
                _inputId = _itemDataset.ItemTable[0].Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}