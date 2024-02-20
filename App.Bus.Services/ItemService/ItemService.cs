using System.Collections.Generic;
using System.Linq;
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.TableIdService;
using System;
using System.Text;
using Core.Entites;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Domin.Core;

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
            var codeInt = ValidateData(id, name, code);


            if (id == 0)
            {
                newItemRow = itemDataSet.ItemTable.GetNewRow();
                newItemRow.Id = id;
                newItemRow.Name = name;
                newItemRow.Code = codeInt;
                newItemRow.Id = tableIdService.GetId(DbTablesEnum.item);
                itemDataSet.ItemTable.Add(newItemRow);
            }
            else
            {
                itemDataSet = itemServiceDAO.GetById(id);
                itemDataSet.ItemTable[0].Name = name;
                itemDataSet.ItemTable[0].Code = codeInt;
            }

            itemServiceDAO.Save(itemDataSet);
        }

        //public void ValidateDataSet(ItemDataSet itemDataSet)
        //{
        //    foreach (var item in itemDataSet.ItemTable)
        //    {
        //        ValidateData(item.Name, item.Code.ToString());
        //    }
        //}

        public void DeleteById(int itemId)
        {
            itemServiceDAO.Delete(itemId);
        }


        public int ValidateData(int id, string name, string code)
        {
            errorsMessageString = new StringBuilder();
            ValidateName(name);
            var codeInt = ValidateCode(id, code);

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

        public int ValidateCode(int id, string code)
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
            var itemTable = itemServiceDAO.GetAll().ItemTable;
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
