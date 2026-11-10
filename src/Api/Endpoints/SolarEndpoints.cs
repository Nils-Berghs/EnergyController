
using Berghs.Nils.EnergyController.Application.Features;
using Mediator;

namespace Berghs.Nils.EnergyController.Api.Endpoints;

public static class SolarEndpoints
{
    public static void MapSolarEndpoints(this WebApplication app)
    {
        app.MapGet("solar", async(ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetCurrentSolar(), cancellationToken); 

            return Results.Ok(response); 
        });
    }
}
