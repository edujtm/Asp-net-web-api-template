

namespace Shared.DTOs
{
    public record VehicleDto(Guid Id, int Mileage, string Model, string Manufacturer, int Category, int Color, double DailyRentalFee);
}
