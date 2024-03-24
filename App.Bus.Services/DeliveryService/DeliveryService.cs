using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.Entities.DataRows;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WarehouseTest.Services.StockService;

namespace WarehouseTest.Services.DeliveryService
{
    public class DeliveryService : IDeliveryService
    {
        private readonly DeliveryServiceDAO _deliveryServiceDAO;

        public DeliveryService()
        {
            _deliveryServiceDAO = new DeliveryServiceDAO();
        }
        public DeliveryDataset GetById(int id)
        {
            return _deliveryServiceDAO.GetMasterDetailById(id);
        }

        public DeliveryDataset GetByStockId(int stockId)
        {
            return _deliveryServiceDAO.GetByStockId(stockId);
        }

        public DeliveryDataset GetByItemId(int itemId)
        {
            return _deliveryServiceDAO.GetByItemId(itemId);
        }

        public DeliveryDataset GetAll()
        {
            return _deliveryServiceDAO.GetAll();
        }

        public void Save(DeliveryDataset deliveryDataset)
        {
            if (deliveryDataset.DeliveryTable[0].RowState != DataRowState.Deleted)
                ValidateData(deliveryDataset);

            _deliveryServiceDAO.Save(deliveryDataset);
        }

        public void DeleteById(int deliveryId)
        {
            _deliveryServiceDAO.Delete(deliveryId);
        }

        private bool ValidateReceiptNumber(int id, int deliveryNumber)
        {
            var deliveryTable = _deliveryServiceDAO.GetAll().DeliveryTable;
            foreach (var delivery in deliveryTable)
            {
                if (delivery.Number == deliveryNumber && delivery.Id != id)
                {
                    return false;
                }
            }
            return true;
        }

        private void ValidateData(DeliveryDataset deliveryDataset)
        {
            var errorsMessageString = new StringBuilder();

            if (!ValidateReceiptNumber(deliveryDataset.DeliveryTable[0].Id, deliveryDataset.DeliveryTable[0].Number))
            {
                errorsMessageString.Append(ErrorMessage.RepititiveValue("شماره سند ورود"));
            }

            if (deliveryDataset.DeliveryItemsTable.Rows.Count == 0)
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("لیست کالا ها"));
            }
            else
            {
                foreach (var item in deliveryDataset.DeliveryItemsTable.Where(x => x.RowState != DataRowState.Deleted))
                {
                    if (item.Quantity <= 0 || item.Quantity == null)
                        errorsMessageString.Append(ErrorMessage.ValueMustBePositive("تعداد کالا"));

                    if (item.ItemId == 0)
                        errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("کالا"));
                }
            }

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
        }

        public void DeleteWithcheckVersion(DeliveryDataset dataSet, IdDataRow row)
        {
            _deliveryServiceDAO.DeleteWithcheckVersion(dataSet, row);
        }
    }
}
