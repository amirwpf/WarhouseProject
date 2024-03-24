using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IReceiptService : IEntityService<ReceiptDataset>
    {
        ReceiptDataset GetByStockId(int stockId);
        ReceiptDataset GetByItemId(int itemId);
    }
}
