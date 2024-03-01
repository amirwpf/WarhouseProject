using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.UI;
using App.Framework.UI.Model;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.TableIdService;
using WarehouseTest.UI;

namespace WarehouseTest.forms
{
    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "کالا جدید", Order = 1)]
    public partial class AddItemForm : EntityBaseForm , IMenuExtension
    {
        private readonly ITableIdService _tableIdService;
        private readonly IItemService _itemService;
        ItemTable itemTable;

        private ItemDataSet _itemDataset;
        private int _id;

        public AddItemForm()
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
             _tableIdService = serviceFactory.Resolve<ITableIdService>();
            itemTable = new ItemTable();

            _itemDataset = new ItemDataSet();
            _id = 0;


            //ListBaseForm<ItemDataSet, ItemTable, ItemRow, AddItemForm> listBaseForm = new ListBaseForm<ItemDataSet, ItemTable, ItemRow, AddItemForm>(_itemService);
            //listBaseForm.Show();
        }

        public override void SetInputId(int inputId)
        {
            //var serviceFactory = new ServiceFactory();
            //_itemService = serviceFactory.Resolve<IItemService>();
            //_tableIdService = serviceFactory.Resolve<ITableIdService>();

            // itemTable = new ItemTable();
            _itemDataset = _itemService.GetById(inputId);

            itemCodeTxt.Text = _itemDataset.ItemTable[0].Code.ToString();
            itemNameTx.Text = _itemDataset.ItemTable[0].Name.ToString();
            _id = inputId;
        }

        public AddItemForm(int id, int code, string name)
        {
            InitializeComponent();

            var serviceFactory = new ServiceFactory();
            _itemService = serviceFactory.Resolve<IItemService>();
            _tableIdService = serviceFactory.Resolve<ITableIdService>();

            // itemTable = new ItemTable();


            itemCodeTxt.Text = code.ToString();
            itemNameTx.Text = name;
            _id = id;
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
            deleteBtn.Enabled = false;
            addBtn.Enabled = false;
            //refreshBtn.Enabled = false;
            //MaximizeBox = false;
        }

        public override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _itemService.Save(_id,itemNameTx.Text, itemCodeTxt.Text);
                MessageBox.Show("کالا با موفقیت ذخیره گردید");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //public int ValidateCode(string code)
        //{
        //    int codeInt = 0;
        //    if (string.IsNullOrEmpty(code))
        //    {
        //        MessageBox.Show(ErrorMessage.ItemCantBeEmpty("کد"));
        //    }
        //    bool validCode = int.TryParse(code, out codeInt);
        //    if (!validCode || codeInt <= 0)
        //    {
        //        MessageBox.Show(ErrorMessage.InValidFieldValue("کد"));
        //    }
        //    return codeInt;
        //}
    }
}
