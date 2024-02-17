using Core.Entites;
using System.Data;


namespace Infrastructure.Repositories
{
    public abstract class NonMasterDetailDAO<TDataSet, TMaster, TMasterRow>
    where TDataSet : NonMasterDetailDataset<TMaster, TMasterRow>, new()
    where TMaster : MasterDataTable<TMasterRow>, new()
    where TMasterRow : DataRow
    {
        private readonly GenericRepository<TMaster, TMasterRow> repository;

        public NonMasterDetailDAO(GenericRepository<TMaster, TMasterRow> repository)
        {
            this.repository = repository;
        }

        public TDataSet GetAll()
        {
            TDataSet ds = new TDataSet();
            ds.MasterTable = repository.GetAll();
            return ds;
        }

        public TDataSet GetById(int id)
        {
            TDataSet ds = new TDataSet();
            ds.MasterTable = repository.GetById(id);
            return ds;
        }

        public void Save(TDataSet dataSet)
        {
            repository.Save(dataSet.MasterTable);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}