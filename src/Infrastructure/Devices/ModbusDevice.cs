using System;
using System.Net.Sockets;
using Berghs.Nils.EnergyController.Domain.Devices;
using NModbus;

namespace Berghs.Nils.EnergyController.Infrastructure.Devices;

public abstract class ModbusDevice
{
    protected readonly ModbusConfiguration _configuration;

    public ModbusDevice(ModbusConfiguration modbusConfiguration)
    {
        _configuration = modbusConfiguration;
    }

    protected async Task<int> ReadInt32(ushort startAddress)
    {
        using var client = new TcpClient();
        await client.ConnectAsync(_configuration.Host, _configuration.Port)
            .WaitAsync(TimeSpan.FromMilliseconds(_configuration.TimeoutMilliseconds));

        using var master = new ModbusFactory().CreateMaster(client);
        master.Transport.ReadTimeout = _configuration.TimeoutMilliseconds;
        master.Transport.WriteTimeout = _configuration.TimeoutMilliseconds;

        var registers = await master.ReadHoldingRegistersAsync(
            checked((byte)_configuration.UnitId),
            startAddress,
            2);

        if (registers.Length != 2)
        {
            throw new InvalidOperationException(
                $"Expected 2 Modbus registers, received {registers.Length}.");
        }
        
        return unchecked((int)(((uint)registers[0] << 16) | registers[1]));
    }
}
