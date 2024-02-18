using Warehouse.Framework.Common;
using WarehouseTest.UI.models;
using WarehouseTest.UI;

namespace MainMenuExtentions
{
    class StockItemQuantityReportExt : IExtension
    {
        public string Name => "گزارش  موجودی انبار";

        public int Order => 51;

        public BaseForm Btn_Click()
        {
            StockItemQuantityReportForm stockItemQuantityReportForm = new StockItemQuantityReportForm();
            return stockItemQuantityReportForm;
        }
    }
}
