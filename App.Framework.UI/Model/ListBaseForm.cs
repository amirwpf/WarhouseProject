using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Framework.UI.Model
{
    public partial class ListBaseForm<TDataSet,TDataTable,TDataRow,TForm> : BaseForm 
        where TDataSet:BaseDataSet<TDataTable, TDataRow>,new() 
        where TDataRow : DataRow 
        where TForm:EntityBaseForm,new()
        where TDataTable:MasterDataTable<TDataRow> , new()
    {
        private readonly IGenericService<TDataSet> _baseService;
        private TDataSet _dataSet;
        public ListBaseForm(IGenericService<TDataSet> baseService)
        {
            InitializeComponent();
            InitializeToolTips();
            _baseService = baseService;
            _dataSet = _baseService.GetAll();

        }

        private void InitializeToolTips()
        {

            ToolTip deleteBtnTT = new ToolTip();
            deleteBtnTT.SetToolTip(deleteBtn, "حذف");

            ToolTip refreshBtnTT = new ToolTip();
            refreshBtnTT.SetToolTip(refreshBtn, "بروز رسانی");

            ToolTip addBtnTT = new ToolTip();
            addBtnTT.SetToolTip(addBtn, "جدید");
        }

        public virtual void deleteBtn_Click(object sender, EventArgs e)
        {
            var selectedRows = baseDataGrid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var itemRow = (row.DataBoundItem as DataRowView)?.Row as TDataRow;
                        if (itemRow != null)
                        {
                            var id = (int)itemRow["Id"];
                            try
                            {
                                _baseService.DeleteById(id);
                                MessageBox.Show("آیتم با موفقیت حذف گردید");
                                RefreshDataGrid();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "خطا");
                            }
                        }
                    }
                }
            }
        }

        public virtual void addBtn_Click(object sender, EventArgs e)
        {

        }

        public virtual void refreshBtn_Click(object sender, EventArgs e)
        {

        }

        private void ListBaseForm_Load(object sender, EventArgs e)
        {

        }

        private void itemDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < baseDataGrid.Rows.Count)
            {
                DataRowView selectedRow = (DataRowView)baseDataGrid.Rows[e.RowIndex].DataBoundItem;


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
            baseDataGrid.DataSource = _dataSet.MasterTable;
        }
    }
}
