//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System;
//using System.Windows.Forms;
//using WarehouseTest.Services.ItemService;
//using WarehouseTest.forms;
//using Core.Entites;
//using App.Domin.Core.Contracts.ServiceInterface;
//using App.Framework.UI;
//using App.Framework;
//using App.Domin.Core;
//using App.Framework.UI.Model;

//namespace WarehouseTest.UI
//{
//    [ExtentionMenu(CategoryName = "Warehouse", MenuName = "کالا", Order = 2)]
//    public partial class ItemList : ListBaseForm, IMenuExtension
//    {
//        private readonly IItemService _itemService;
//        ItemDataSet itemDataSet;
//        ItemTable itemTable;


//        public ItemList()
//        {
//            InitializeComponent();

//            var serviceFactory = new ServiceFactory();
//            _itemService = serviceFactory.Resolve<IItemService>();

//            itemDataSet = _itemService.GetAll();

//            InitializeItemDataGirdView();
//        }

//        private void ItemList_Load(object sender, EventArgs e)
//        {
//            //saveBtn.Enabled = false;
//            //MaximizeBox = false;
//        }

//        private void InitializeItemDataGirdView()
//        {
//            itemTable = itemDataSet.ItemTable;
//            itemDataGrid.AllowUserToAddRows = false;
//            itemDataGrid.DataSource = itemDataSet.ItemTable;
//            itemDataGrid.AllowUserToAddRows = false;
//            itemDataGrid.AllowUserToDeleteRows = false;
//            itemDataGrid.Columns["Id"].Visible = false;
//            itemDataGrid.Columns["Code"].Visible = false;
//            itemDataGrid.Columns["Name"].Visible = false;

//            var codeColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "codeColumn",
//                HeaderText = "کد",
//                DataPropertyName = "Code",
//            };

//            itemDataGrid.Columns.Add(codeColumn);

//            var nameColumn = new DataGridViewTextBoxColumn
//            {
//                Name = "nameColumn",
//                HeaderText = "نام",
//                DataPropertyName = "Name",
//            };

//            itemDataGrid.Columns.Add(nameColumn);
//        }

//        public override void refreshBtn_Click(object sender, EventArgs e)
//        {
//            itemDataSet = _itemService.GetAll();
//            itemDataGrid.DataSource = itemDataSet.ItemTable;
//        }

//        public override void addBtn_Click(object sender, EventArgs e)
//        {
//            AddItemForm addItemForm = new AddItemForm();
//            addItemForm.Show();
//        }

//        public override void deleteBtn_Click(object sender, EventArgs e)
//        {
//            var selectedRows = itemDataGrid.SelectedRows;
//            if(selectedRows.Count>0)
//            {
//                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

//                if (result == DialogResult.Yes)
//                {

//                    foreach (DataGridViewRow row in selectedRows)
//                    {
//                        var itemRow = (row.DataBoundItem as DataRowView)?.Row as ItemRow;
//                        if (itemRow != null)
//                        {
//                            var id = itemRow.Id;
//                            try
//                            {
//                                _itemService.DeleteById(id);
//                                MessageBox.Show("آیتم با موفقیت حذف گردید");
//                                RefreshDataGrid();
//                            }
//                            catch(Exception ex)
//                            {
//                                MessageBox.Show(ex.Message, "خطا");
//                            }
//                        }
//                    }
//                }
//            }            
//        }

//        //internal override void SaveBtn_Click(object sender, EventArgs e)
//        //{
//        //    try
//        //    {
//        //        foreach (DataRow row in itemDataSet.ItemTable.Select("", "", DataViewRowState.Deleted))
//        //        {
//        //            row.Delete();
//        //        }

//        //        _itemService.Save(itemDataSet);
//        //        MessageBox.Show("Save Completed !");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show(ex.Message);
//        //    }

//        //}

//        private void itemDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.RowIndex < itemDataGrid.Rows.Count)
//            {
//                DataRowView selectedRow = (DataRowView)itemDataGrid.Rows[e.RowIndex].DataBoundItem;


//                if (selectedRow != null && selectedRow.Row.RowState != DataRowState.Deleted)
//                {

//                    var id = (int)selectedRow["Id"];
//                    var code = (int)selectedRow["Code"];
//                    var name = (string)selectedRow["Name"];

//                    AddItemForm addItemForm = new AddItemForm(id, code, name);
//                    addItemForm.WindowState = FormWindowState.Normal;
//                    addItemForm.ShowDialog();
//                }
//            }
//        }

//        private DialogResult ShowConfirmationMessageBox(string message)
//        {
//            DialogResult result = MessageBox.Show(
//                message,
//                "تایید حذف",
//                MessageBoxButtons.YesNo,
//                MessageBoxIcon.Question,
//                MessageBoxDefaultButton.Button2
//            );

//            return result;
//        }

//        public void RefreshDataGrid()
//        {
//            itemDataSet = _itemService.GetAll();
//            itemDataGrid.DataSource = itemDataSet.ItemTable;
//        }
//    }
//}