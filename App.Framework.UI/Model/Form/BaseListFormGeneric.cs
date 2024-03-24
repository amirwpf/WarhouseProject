using App.Framework.Entities.DataRows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Framework.UI.Model
{
    public class BaseListFormGeneric<TDataSet, TDataRow, TService> : BaseListForm
        where TDataSet : BaseDataSet, new()
        where TDataRow : IdDataRow
        where TService : IEntityService<TDataSet>
    {
        #region public fields
        public event Func<EntityBaseForm> NewForm;
        public event Func<int, EntityBaseForm> EditForm;
        #endregion

        #region private fields
        private readonly IEntityService<TDataSet> _baseService;
        private TDataSet _dataSet;
        #endregion


        public BaseListFormGeneric()
        {
            ServiceFactory serviceFactory = new ServiceFactory();
            _baseService = serviceFactory.Resolve<TService>();
            _dataSet = _baseService.GetAll();
            InitializeDataGridView();
            dataGrid.DataSource = _dataSet.MasterTable;
        }

        public override void deleteBtn_Click(object sender, EventArgs e)
        {
            var selectedCells = dataGrid.SelectedCells;

            if (selectedCells.Count > 0)
            {
                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        //HashSet<int> deletedIndexes = new HashSet<int>();
                        foreach (DataGridViewCell cell in selectedCells)
                        {
                            var rowIndex = cell.RowIndex;
                            var itemRow = (dataGrid.Rows[rowIndex].DataBoundItem as DataRowView)?.Row as IdDataRow;

                            if (itemRow != null && itemRow is IVersionDataRow versionDataRow)
                            {
                                itemRow.Delete();
                                //deletedIndexes.Add(rowIndex);
                                _baseService.DeleteWithcheckVersion(_dataSet,itemRow);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "خطا");
                    }
                    RefreshDataGrid();
                }
            }
        }

        protected override void dataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (EditForm != null)
                {
                    DataRowView selectedRow = (DataRowView)dataGrid.Rows[e.RowIndex].DataBoundItem;

                    if (selectedRow != null && selectedRow.Row.RowState != DataRowState.Deleted)
                    {
                        var _id = (int)selectedRow["Id"];
                        var form = EditForm(_id);
                        MainFormManager.AddFormToMainForm(form);
                    }
                }
            }
            catch (Exception ex) { }
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
            if (NewForm != null)
                MainFormManager.AddFormToMainForm(NewForm());
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
