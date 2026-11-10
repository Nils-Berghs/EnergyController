using Berghs.Nils.EnergyController.Domain.Devices;
using Microsoft.Extensions.Configuration;

namespace Berghs.Nils.EnergyController.Infrastructure.Devices;

public class SunnyBoySolarConverter : ModbusDevice, ISolarConverter
{
    private const int REG_SERIAL_NUMBER = 30005;
    private const int REG_CURRENT_POWER = 30775;

    public SunnyBoySolarConverter(IConfiguration configuration) : base(configuration.GetSection("Devices:SunnyBoy").Get<ModbusConfiguration>())
    {
    }

    public async Task<double> GetCurrentProduction()
    {
        return await ReadInt32(REG_CURRENT_POWER);
    }
}
