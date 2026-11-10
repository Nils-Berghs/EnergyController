using System;
using Berghs.Nils.EnergyController.Application.Features.Solar.Types;
using Berghs.Nils.EnergyController.Domain.Devices;
using Mediator;
using Microsoft.VisualBasic;

namespace Berghs.Nils.EnergyController.Application.Features;

public class GetCurrentSolar : IQuery<CurrentSolarProduction>
{

}

public class GetCurrentSolarHandler(ISolarConverter SolarConverter) : IQueryHandler<GetCurrentSolar, CurrentSolarProduction>
{
    public async ValueTask<CurrentSolarProduction> Handle(GetCurrentSolar query, CancellationToken cancellationToken)
    {
        var result = await SolarConverter.GetCurrentProduction();

        return new CurrentSolarProduction
        {
            Production = result
        };
    }
}