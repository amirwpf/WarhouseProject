using App.Domin.Core.Contracts.ServiceInterface;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Framework.Common;
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.ItemService;
using WarehouseTest.Services.StockService;
using WarehouseTest.Services.TableIdService;
using WarehouseTest.UI.models;
using static WarehouseTest.Program;

namespace WarehouseTest.UI
{
    public partial class AddDeliveryForm : BaseForm
    {
        private readonly IItemService _itemService;
        private readonly ITableIdService _tableIdService;
        private readonly IDeliveryService _deliveryService;
        private readonly IStockService _stockService;

        DeliveryDataset deliveryDataset;
        
        StockTable stockTable;
        ItemTable itemTable;
        DeliveryRow newDeliveryRow;
        int deliveryId;
        
        public AddDeliveryForm()
        {
            InitializeComponent();
            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IItemService, ItemService>();
            proxyFactory.Register<ITableIdService, TableIdService>();
            proxyFactory.Register<IDeliveryService, DeliveryService>();
            proxyFactory.Register<IStockService, StockService>();
            _itemService = proxyFactory.Resolve<IItemService>();
            _tableIdService = proxyFactory.Resolve<ITableIdService>();
            _deliveryService = proxyFactory.Resolve<IDeliveryService>();
            _stockService = proxyFactory.Resolve<IStockService>();
            InitializeStockCombo();
            InitializeItemDataGirdView();
            FormSetUp();
        }

        public AddDeliveryForm(DeliveryDataset _deliveryDataset, int stockId)
        {
            InitializeComponent();
            deliveryDataset = _deliveryDataset;

            var proxyFactory = new ProxyFactory();
            proxyFactory.Register<IItemService, ItemService>();
            proxyFactory.Register<ITableIdService, TableIdService>();
            proxyFactory.Register<IDeliveryService, DeliveryService>();
            proxyFactory.Register<IStockService, StockService>();
            _itemService = proxyFactory.Resolve<IItemService>();
            _tableIdService = proxyFactory.Resolve<ITableIdService>();
            _deliveryService = proxyFactory.Resolve<IDeliveryService>();
            _stockService = proxyFactory.Resolve<IStockService>();

            InitializeItemDataGirdView();
            InitializeStockCombo();

            var deliveryRow = deliveryDataset.DeliveryTable[0];
            deliveryId = deliveryRow.Id;
            deliveryNumberTxt.Text = deliveryRow.Number.ToString();
            deliveryDatePicker.Value = deliveryDataset.DeliveryTable[0].Date;
            itemDataGrid.DataSource = _deliveryDataset.DeliveryItemsTable;
            var stockRow = stockTable.FirstOrDefault(x => x.Id == stockId);
            var stockRowIndex = stockTable.Rows.IndexOf(stockRow);
            stockCombo.SelectedIndex = stockRowIndex;

            itemDataGrid.Columns["DeliveryId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;

        }

        private void InitializeStockCombo()
        {
            stockTable = _stockService.GetAll().StockTable;
            stockCombo.DataSource = stockTable;
            stockCombo.DisplayMember = "Name";
            stockCombo.ValueMember = "Id";
            stockCombo.SelectedItem = null;
        }

        private void InitializeItemDataGirdView()
        {

            itemTable = _itemService.GetAll().ItemTable;
            itemDataGrid.AllowUserToAddRows = false;
            itemDataGrid.AllowUserToDeleteRows = false;


            var comboBoxColumn = new DataGridViewComboBoxColumn
            {
                Name = "IdColumn",
                HeaderText = "کالا",
                DataPropertyName = "ItemId",
                DisplayMember = "Name",
                ValueMember = "Id",
            };

            itemDataGrid.Columns.Add(comboBoxColumn);

            var textBoxColumn = new DataGridViewTextBoxColumn
            {
                Name = "QuantityColumn",
                HeaderText = "تعداد",
                DataPropertyName = "Quantity",
            };

            itemDataGrid.Columns.Add(textBoxColumn);
            comboBoxColumn.DataSource = itemTable;

        }

        private void FormSetUp()
        {
            itemDataGrid.Enabled = false;
            addItemBtn.Enabled = false;
            stockCombo.SelectedItem = null;
            deliveryDatePicker.Value = DateTime.Now;

            deliveryDataset = new DeliveryDataset();
            itemDataGrid.DataSource = deliveryDataset.DeliveryItemsTable;

            itemDataGrid.Columns["DeliveryId"].Visible = false;
            itemDataGrid.Columns["Id"].Visible = false;
            newDeliveryRow = deliveryDataset.DeliveryTable.GetNewRow();
            newDeliveryRow.Id = _tableIdService.GetId(DbTablesEnum.delivery);
            deliveryId = newDeliveryRow.Id;
            deliveryDataset.DeliveryTable.Add(newDeliveryRow);
        }

        private void AddDeliveryForm_Load(object sender, EventArgs e)
        {
            refreshBtn.Enabled = false;
            addBtn.Enabled = false;
            //MaximizeBox = false;
        }

        internal override void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = stockCombo.SelectedItem;
                _deliveryService.Save(deliveryDataset, selectedItem, deliveryNumberTxt.Text, deliveryDatePicker.Value);
                MessageBox.Show("ذخیره با موفقیت صورت گردید");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal override void deleteBtn_Click(object sender, EventArgs e)
        {
            var selectedRows = itemDataGrid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                DialogResult result = ShowConfirmationMessageBox("آیتم حذف گردد؟");

                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var deliveryItem = (row.DataBoundItem as DataRowView)?.Row as DeliveryItemsRow;
                        if (deliveryItem != null)
                        {
                            try
                            {
                                deliveryItem.Delete();
                            }
                            catch
                            {
                                MessageBox.Show("خطا در حذف آیتم", "خطا");
                            }
                        }
                    }
                }
            }

        }

        private DialogResult ShowConfirmationMessageBox(string message)
        {
            DialogResult result = MessageBox.Show(
                message,
                "تایید حذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            return result;
        }

        private void addItemBtn_Click(object sender, EventArgs e)
        {
            var newReceiptItemRow = deliveryDataset.DeliveryItemsTable.GetNewRow();
            newReceiptItemRow.Id = _tableIdService.GetId(DbTablesEnum.deliveryItems);
            newReceiptItemRow.DeliveryId = deliveryId;
            newReceiptItemRow.Quantity = 0;
            if (itemTable.Rows.Count > 0)
            {
                newReceiptItemRow.ItemId = (int)itemTable.Rows[0]["Id"];
            }
            deliveryDataset.DeliveryItemsTable.Add(newReceiptItemRow);
        }

        private void stockCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemDataGrid.Enabled = true;
            addItemBtn.Enabled = true;
        }
    }
}


/*
If you want to use Castle.DynamicProxy without Castle.Windsor and manually register and resolve dependencies, you can follow this simplified example:

```csharp
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;

public interface IService
{
    void DoSomething();
}

public class Service : IService
{
    public void DoSomething()
    {
        Console.WriteLine("Service is doing something.");
    }
}

public class ProxyFactory
{
    private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();
    private readonly Dictionary<Type, object> _registeredInstances = new Dictionary<Type, object>();

    public void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        if (!_registeredInstances.ContainsKey(typeof(TInterface)))
        {
            _registeredInstances[typeof(TInterface)] = new TImplementation();
        }
    }

    public TInterface Resolve<TInterface>()
    {
        if (_registeredInstances.TryGetValue(typeof(TInterface), out var instance))
        {
            return (TInterface)instance;
        }

        var interceptor = new MyInterceptor();
        return _proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(Activator.CreateInstance<TInterface>(), interceptor);
    }
}

public class MyInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Intercepting method: {invocation.Method.Name}");
        invocation.Proceed();
    }
}

class Program
{
    static void Main()
    {
        var proxyFactory = new ProxyFactory();

        proxyFactory.Register<IService, Service>();

        var service = proxyFactory.Resolve<IService>();

        service.DoSomething();
    }
}
```

In this example:

- `ProxyFactory` manually registers instances for specific interfaces.
- If an interface is not registered, it creates a proxy using `CreateInterfaceProxyWithTarget` and a custom interceptor (`MyInterceptor`).

This is a basic example, and for more complex scenarios, you might need to enhance the registration and resolution logic.
*/