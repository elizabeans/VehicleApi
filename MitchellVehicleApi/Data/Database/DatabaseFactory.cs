namespace MitchellVehicleApi.Data.Database
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly VehicleDataContext _dataContext;

        public VehicleDataContext GetDataContext()
        {
            return _dataContext ?? new VehicleDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new VehicleDataContext();
        }

        protected override void DisposeCore()
        {
            _dataContext.Dispose();
        }
    }
}