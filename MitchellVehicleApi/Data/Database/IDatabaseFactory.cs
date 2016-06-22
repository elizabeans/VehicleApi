using System;

namespace MitchellVehicleApi.Data.Database
{
    public interface IDatabaseFactory : IDisposable
    {
        VehicleDataContext GetDataContext();
    }
}
