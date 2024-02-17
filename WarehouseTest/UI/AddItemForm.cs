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
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.TableIdService;
using WarehouseTest.UI.models;

namespace WarehouseTest.forms
{
    public partial class AddItemForm : BaseForm
    {
        private readonly ITableIdService _tableIdService;
        private readonly IItemService _itemService;
        ItemTable itemTable;

        private ItemDataSet _itemDataset;
        private int _id;
        public AddItemForm()
        {
            InitializeComponent();
            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IItemService, ItemService>();
            proxyFactory.Register<ITableIdService, TableIdService>();
            _itemService = proxyFactory.Resolve<IItemService>();
            _tableIdService = proxyFactory.Resolve<ITableIdService>();
            itemTable = new ItemTable();

            _itemDataset = new ItemDataSet();
            _id = 0;
        }

        public AddItemForm(int id, int code, string name)
        {
            InitializeComponent();


            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IItemService, ItemService>();
            proxyFactory.Register<ITableIdService, TableIdService>();
            _itemService = proxyFactory.Resolve<IItemService>();
            _tableIdService = proxyFactory.Resolve<ITableIdService>();

           // itemTable = new ItemTable();


            itemCodeTxt.Text = code.ToString();
            itemNameTx.Text = name;
            _id = id;
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
            deleteBtn.Enabled = false;
            addBtn.Enabled = false;
            refreshBtn.Enabled = false;
            //MaximizeBox = false;
        }

        internal override void SaveBtn_Click(object sender, EventArgs e)
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
    }
}
