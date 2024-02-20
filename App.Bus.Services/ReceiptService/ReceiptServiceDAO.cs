using App.Framework;
using Core.Entites;

namespace WarehouseTest.Services.ReceiptService
{
    public class ReceiptServiceDAO : MasterDetailDAO<ReceiptDataset, ReceiptTable, ReceiptItemsTable, ReceiptRow, ReceiptItemsRow>
    {
        public ReceiptServiceDAO() : base(new GenericRepository<ReceiptTable, ReceiptRow>(), new GenericRepository<ReceiptItemsTable, ReceiptItemsRow>())
        {

        }
    }
}
