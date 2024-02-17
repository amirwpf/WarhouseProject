using System;
using System.Data;

public abstract class MasterDetailDataset<TMaster, TDetail, TMasterRow, TDetailRow> : DataSet
where TDetail : DetailDataTable<TDetailRow>, new()
where TDetailRow : DataRow
where TMaster : MasterDataTable<TMasterRow>, new()
where TMasterRow : DataRow
{
    public MasterDetailDataset()
    {
        //MasterTable = Activator.CreateInstance<TMaster>();
        //DetailTable = Activator.CreateInstance<TDetail>();
        //MasterTable = new MasterDataTable<TMasterRow>();
        //DetailTable = new DetailDataTable<TDetailRow>();


        //Tables.Add(MasterTable);
        //Tables.Add(DetailTable);


        //DataRelation relation = new DataRelation("MasterDetailRelation", MasterTable.IdColumn, DetailTable.DeliveryIdColumn);
        //Relations.Add(relation);
    }


    public  abstract TMaster MasterTable { get; set; }
    public abstract TDetail DetailTable { get; set; }
}
