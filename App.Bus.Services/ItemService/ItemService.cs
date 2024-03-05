using System.Collections.Generic;
using System.Linq;
using WarehouseTest.Services.DeliveryService;
using System;
using System.Text;
using Core.Entites;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Domin.Core;
using WarehouseTest.Services.ReceiptService;
using App.Framework;

namespace WarehouseTest.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly ItemServiceDAO _itemServiceDAO;
        private readonly IReceiptService _receiptService;
        private readonly IDeliveryService _deliveryService;


        public ItemService()
        {
            _itemServiceDAO = new ItemServiceDAO();
            var itemDataSet = new ItemDataSet();
            ServiceFactory serviceFactory = new ServiceFactory();
            _receiptService = serviceFactory.Resolve<IReceiptService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
        }

        public ItemDataSet GetById(int itemId)
        {
            return _itemServiceDAO.GetById(itemId);
        }

        public ItemDataSet GetAll()
        {
            return _itemServiceDAO.GetAll();
        }

        public void Save(ItemDataSet itemDataSet)
        {
            var codeInt = ValidateData(itemDataSet.ItemTable[0].Id, itemDataSet.ItemTable[0].Name, itemDataSet.ItemTable[0].Code.ToString());

            _itemServiceDAO.Save(itemDataSet);
        }

        public void DeleteById(int itemId)
        {
            var itemRecList = _receiptService.GetByItemId(itemId).ReceiptItemsTable;
            var itemDelList = _deliveryService.GetByItemId(itemId).DeliveryItemsTable;
            var errorMsg = new StringBuilder();
            var item = _itemServiceDAO.GetById(itemId).ItemTable[0];

            foreach (var itemRow in itemRecList)
            {
                errorMsg.Append( $"کالا { item.Name} در ورود کد { _receiptService.GetById(itemRow.ReceiptId).ReceiptTable[0].Number} استفاده شده است \n ");
            }

            foreach (var itemRow in itemDelList)
            {
                errorMsg.Append($"کالا { item.Name} در خروج کد { _deliveryService.GetById(itemRow.DeliveryId).DeliveryTable[0].Number} استفاده شده است \n ");
            }

            if(itemRecList.Count()>0 || itemDelList.Count()>0)
            {
                throw new Exception(errorMsg.ToString());
            }
            _itemServiceDAO.Delete(itemId);
        }


        public int ValidateData(int id, string name, string code)
        {
            var errorsMessageString = new StringBuilder();
            ValidateName(name , id, errorsMessageString);
            var codeInt = ValidateCode(id, code, errorsMessageString);

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
            return codeInt;
        }




        public void ValidateName(string name, int id, StringBuilder errorsMessageString)
        {
            if (string.IsNullOrEmpty(name))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("نام"));
            }

            var itemTable = _itemServiceDAO.GetAll().ItemTable;
            foreach (var item in itemTable)
            {
                if (item.Name == name && id != 0)
                {
                    var itemRowId = item.Id;
                    if (itemRowId != id)
                    {
                        errorsMessageString.Append(ErrorMessage.RepititiveValue("نام"));
                        break;
                    }
                }

                if (item.Name == name && id == 0)
                {
                    errorsMessageString.Append(ErrorMessage.RepititiveValue("نام"));
                    break;
                }
            }
        }

        public int ValidateCode(int id, string code , StringBuilder errorsMessageString)
        {
            int codeInt = 0;
            if (string.IsNullOrEmpty(code))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("کد"));
            }
            bool validCode = int.TryParse(code, out codeInt);
            if (!validCode || codeInt <= 0)
            {
                errorsMessageString.Append(ErrorMessage.InValidFieldValue("کد"));
            }
            var itemTable = _itemServiceDAO.GetAll().ItemTable;
            foreach (var item in itemTable)
            {
                if (item.Code == codeInt && id != 0)
                {
                    var itemRowId = item.Id;
                    if (itemRowId != id)
                    {
                        errorsMessageString.Append(ErrorMessage.RepititiveValue("کد"));
                        break;
                    }
                }

                if (item.Code == codeInt && id == 0)
                {
                    errorsMessageString.Append(ErrorMessage.RepititiveValue("کد"));
                    break;
                }
            }
            return codeInt;
        }
    }
}
