using System;

namespace Berghs.Nils.EnergyController.Domain.Devices;

public class ModbusConfiguration
{
    public string Host { get; set; }

    public int Port { get; set; }

    public int UnitId { get; set; }

    public int TimeoutMilliseconds { get; set; }
}
