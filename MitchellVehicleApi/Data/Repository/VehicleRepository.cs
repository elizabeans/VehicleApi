using MitchellVehicleApi.Data.Database;
using MitchellVehicleApi.Data.Model;

namespace MitchellVehicleApi.Data.Repository
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
       public VehicleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
       {

       }
    }
}