using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
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
        private readonly DeliveryServiceDAO deliveryServiceDAO;

        public DeliveryService()
        {
            deliveryServiceDAO = new DeliveryServiceDAO();
        }
        public DeliveryDataset GetById(int id)
        {
            return deliveryServiceDAO.GetMasterDetailByMasterId(id);
        }

        public DeliveryDataset GetByStockId(int stockId)
        {
            return deliveryServiceDAO.GetByStockId(stockId);
        }

        public DeliveryDataset GetByItemId(int itemId)
        {
            return deliveryServiceDAO.GetByItemId(itemId);
        }

        public DeliveryDataset GetAll()
        {
            return deliveryServiceDAO.GetMasterAll();
        }

        public void Save(DeliveryDataset deliveryDataset)
        {
            ValidateData(deliveryDataset);

            deliveryServiceDAO.SaveMasterDetail(deliveryDataset);
        }

        public void DeleteById(int deliveryId)
        {
            deliveryServiceDAO.DeleteMasterDetailByMasterId(deliveryId);
        }

        private bool ValidateReceiptNumber(int id, int deliveryNumber)
        {
            var deliveryTable = deliveryServiceDAO.GetMasterAll().DeliveryTable;
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

    }
}
