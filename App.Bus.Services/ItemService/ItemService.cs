using System.Collections.Generic;
using System.Linq;
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.TableIdService;
using System;
using System.Text;
using Core.Entites;
using Warehouse.Framework.Common;
using App.Domin.Core.Contracts.ServiceInterface;

namespace WarehouseTest.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly ItemServiceDAO itemServiceDAO;
        TableIdService.TableIdService tableIdService;
        StringBuilder errorsMessageString;
        ItemDataSet itemDataSet;
        ItemRow newItemRow;
        public bool updateRow;
        public ItemService()
        {
            itemServiceDAO = new ItemServiceDAO();
            tableIdService = new TableIdService.TableIdService();
            itemDataSet = new ItemDataSet();
            newItemRow = itemDataSet.ItemTable.GetNewRow();
            //updateRow = true;
        }

        public ItemDataSet GetById(int itemId)
        {
            return itemServiceDAO.GetById(itemId);
        }

        public ItemDataSet GetAll()
        {
            return itemServiceDAO.GetAll();
        }

        public void Save(int id, string name, string code)
        {
            var codeInt = ValidateData(name, code);

            newItemRow.Id = id;
            newItemRow.Name = name;
            newItemRow.Code = codeInt;
            if (id == 0)
            {
                newItemRow.Id = tableIdService.GetId(DbTablesEnum.item);
                //updateRow = false;
            }
            itemDataSet.ItemTable.Add(newItemRow);

            itemServiceDAO.Save(itemDataSet);
        }

        public void Save(ItemDataSet itemDataSet)
        {
            ValidateDataSet(itemDataSet);
            itemServiceDAO.Save(itemDataSet);
        }

        public void ValidateDataSet(ItemDataSet itemDataSet)
        {
            foreach (var item in itemDataSet.ItemTable)
            {
                ValidateData(item.Name, item.Code.ToString());
            }
        }

        public void DeleteById(int itemId)
        {
            itemServiceDAO.Delete(itemId);
        }


        public int ValidateData(string name, string code)
        {
            errorsMessageString = new StringBuilder();
            ValidateName(name);
            var codeInt = ValidateCode(code);

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
            return codeInt;
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("نام"));
            }
        }

        public int ValidateCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("کد"));
            }
            bool validCode = int.TryParse(code, out int codeInt);
            if (!validCode || codeInt <= 0)
            {
                errorsMessageString.Append(ErrorMessage.InValidFieldValue("کد"));
            }
            else
            {
                return codeInt;
            }
            return 0;
        }
    }
}
