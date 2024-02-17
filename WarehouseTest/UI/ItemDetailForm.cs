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

namespace WarehouseTest.UI
{
    public partial class ItemDetailForm : BaseForm
    {
        ITableIdService tableIdService;
        ItemService itemService;
        ItemTable itemTable;
        public ItemDetailForm(int id, int code, string name)
        {
            InitializeComponent();
            itemCodeTxt.Text = code.ToString();
            itemNameTx.Text = name;
            tableIdService = new TableIdService();
            itemService = new ItemService();
            itemService.updateRow = false;
            itemTable = new ItemTable();
        }

        private void ItemDetailForm_Load(object sender, EventArgs e)
        {
            //MaximizeBox = false;
        }

        internal override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                itemService.Save(itemNameTx.Text, itemCodeTxt.Text);
                MessageBox.Show("Item Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
