using System.Data.Entity;
using MitchellVehicleApi.Data.Model;

namespace MitchellVehicleApi.Data.Database
{
    public class VehicleDataContext : DbContext
    {
        public VehicleDataContext() : base("Vehicle")
        {
            //var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public IDbSet<Vehicle> Vehicles { get; set; }
    }
}