using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Framework.UI.Model
{
    public class BaseListFormGeneric<TDataSet, TDataTable, TDataRow, TForm, TService> : BaseListForm
        where TDataSet : BaseDataSet<TDataTable, TDataRow>, new()
        where TDataRow : DataRow
        where TForm : EntityBaseForm, new()
        where TDataTable : MasterDataTable<TDataRow>, new()
        where TService : IGenericService<TDataSet>
    {
        private readonly IGenericService<TDataSet> _baseService;
        private TDataSet _dataSet;

        public BaseListFormGeneric()
        {
            ServiceFactory serviceFactory = new ServiceFactory();
            _baseService = serviceFactory.Resolve<TService>();
            _dataSet = _baseService.GetAll();
            InitializeDataGridView();
            dataGrid.DataSource = _dataSet.MasterTable;
        }

        //public override void deleteBtn_Click(object sender, EventArgs e)
        //{
        //    var selectedRows = dataGrid.SelectedRows;
        //    if (selectedRows.Count > 0)
        //    {
        //        DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

        //        if (result == DialogResult.Yes)
        //        {

        //            foreach (DataGridViewRow row in selectedRows)
        //            {
        //                var itemRow = (row.DataBoundItem as DataRowView)?.Row as TDataRow;
        //                if (itemRow != null)
        //                {
        //                    var id = (int)itemRow["Id"];
        //                    try
        //                    {
        //                        _baseService.DeleteById(id);
        //                        MessageBox.Show("آیتم با موفقیت حذف گردید");
        //                        RefreshDataGrid();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        MessageBox.Show(ex.Message, "خطا");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public override void deleteBtn_Click(object sender, EventArgs e)
        {
            var selectedCells = dataGrid.SelectedCells;

            if (selectedCells.Count > 0)
            {
                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

                if (result == DialogResult.Yes)
                {
                    HashSet<int> deleteIds = new HashSet<int>();

                    foreach (DataGridViewCell cell in selectedCells)
                    {
                        int rowIndex = cell.RowIndex;

                        if (!deleteIds.Contains(rowIndex) && rowIndex != -1)
                        {
                            var itemRow = (dataGrid.Rows[rowIndex].DataBoundItem as DataRowView)?.Row as TDataRow;

                            if (itemRow != null)
                            {
                                var id = (int)itemRow["Id"];
                                deleteIds.Add(id);
                            }
                        }
                    }

                    foreach (var id in deleteIds)
                    {
                        try
                        {
                            _baseService.DeleteById(id);

                            //MessageBox.Show("آیتم‌ها با موفقیت حذف گردیدند");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "خطا");
                        }
                    }
                }

                RefreshDataGrid();
            }
        }

        protected override void dataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGrid.Rows.Count)
            {
                DataRowView selectedRow = (DataRowView)dataGrid.Rows[e.RowIndex].DataBoundItem;


                if (selectedRow != null && selectedRow.Row.RowState != DataRowState.Deleted)
                {

                    var _id = (int)selectedRow["Id"];

                    TForm entityForm = new TForm();
                    entityForm.SetInputId(_id);
                    entityForm.WindowState = FormWindowState.Normal;
                    entityForm.ShowDialog();
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

        public void RefreshDataGrid()
        {
            _dataSet = _baseService.GetAll();
            dataGrid.DataSource = _dataSet.MasterTable;
        }


        protected override void refreshBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }


        protected override void addBtn_Click(object sender, EventArgs e)
        {
            TForm form = new TForm();
            form.Show();
        }


        private void InitializeDataGridView()
        {
            dataGrid.AutoGenerateColumns = false;

            var properties = typeof(TDataRow).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name != "Id" && (DisplayNameAttribute)Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute)) != null)
                {
                    var displayNameAttribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute));
                    dataGrid.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        DataPropertyName = property.Name,
                        HeaderText = displayNameAttribute.DisplayName.ToString()
                    });
                }
            }
        }
    }
}
