using System;

namespace Berghs.Nils.EnergyController.Domain.Devices;

public interface ISolarConverter
{
    Task<double> GetCurrentProduction();
}
