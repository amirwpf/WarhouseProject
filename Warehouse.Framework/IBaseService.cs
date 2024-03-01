using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Framework
{
    public interface IBaseService
    {
    }
}
















//Certainly! You can create a generic `BaseService` class with common functionality like `GetAll` and `Delete` methods.Then, your specific services, like `ItemService`, can inherit from this `BaseService`. Below is an example:

//```csharp
//using System;
//using System.Linq;
//using System.Text;
//using App.Domin.Core;
//using App.Domin.Core.Contracts.ServiceInterface;
//using Core.Entites;
//using WarehouseTest.Services.DeliveryService;
//using WarehouseTest.Services.TableIdService;
//using WarehouseTest.Services.ReceiptService;

//namespace WarehouseTest.Services
//{
//    public class BaseService<TDataSet,TServiceDAO,> where TDataSet : DataSet, new()
//    {
//        protected StringBuilder ErrorMessageString;
//        protected TableIdService.TableIdService TableIdService;

//        protected BaseService()
//        {
//            ErrorMessageString = new StringBuilder();
//            TableIdService = new TableIdService.TableIdService();
//        }

//        public virtual TDataSet GetById(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual TDataSet GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public virtual void Save(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual void Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        protected abstract int ValidateCode(int id, string code);

//        protected void ValidateName(string name)
//        {
//            if (string.IsNullOrEmpty(name))
//            {
//                ErrorMessageString.Append(ErrorMessage.ItemCantBeEmpty("نام"));
//            }
//        }

//        protected int ValidateData(int id, string name, string code)
//        {
//            ValidateName(name);
//            return ValidateCode(id, code);
//        }
//    }

//    public class ItemService : BaseService<ItemDataSet>, IItemService
//    {
//        private readonly ItemServiceDAO _itemServiceDAO;
//        private readonly ReceiptServiceDAO _receiptServiceDAO;
//        private readonly DeliveryServiceDAO _deliveryServiceDAO;
//        private ItemDataSet _itemDataSet;
//        private ItemRow _newItemRow;

//        public ItemService()
//        {
//            _itemServiceDAO = new ItemServiceDAO();
//            _receiptServiceDAO = new ReceiptServiceDAO();
//            _deliveryServiceDAO = new DeliveryServiceDAO();
//            _itemDataSet = new ItemDataSet();
//        }

//        public override ItemDataSet GetById(int itemId)
//        {
//            return _itemServiceDAO.GetById(itemId);
//        }

//        public override ItemDataSet GetAll()
//        {
//            return _itemServiceDAO.GetAll();
//        }

//        public override void Save(int id)
//        {
//            // Your implementation for Save method
//        }

//        public override void Delete(int itemId)
//        {
//            // Your implementation for Delete method
//        }

//        protected override int ValidateCode(int id, string code)
//        {
//            // Your implementation for ValidateCode method
//            return 0;
//        }
//    }
//}
//```

//In this example, I've created a generic `BaseService<TDataSet>` class with common methods and properties. The `ItemService` then inherits from this `BaseService<ItemDataSet>` and provides specific implementations for the abstract methods. This way, you can reuse common functionality across different services.