using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IReceiptService
    {
        ReceiptDataset GetByMasterId(int ReceiptId);
        ReceiptDataset GetMasterAll();

        void Save(ReceiptDataset receiptDataset, object selectedItem, string receiptNumberText, DateTime receiptDate);

        //void Save(ReceiptDataset receiptDataset);

        void DeleteById(int ReceiptId);
        
    }
}
