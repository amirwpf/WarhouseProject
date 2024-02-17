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
        ITableIdService tableIdService;
        IItemService itemService;
        ItemTable itemTable;
        public AddItemForm()
        {
            InitializeComponent();
            tableIdService = new TableIdService();
            itemService = new ItemService();
            itemTable = new ItemTable();
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
                itemService.Save(itemNameTx.Text, itemCodeTxt.Text);
                MessageBox.Show("کالا با موفقیت ذخیره گردید");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
