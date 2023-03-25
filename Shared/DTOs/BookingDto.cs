

namespace Shared.DTOs
{
    public record BookingDto (Guid Id, string From, string To, string Status, bool PaymentReceived, double TotalFee, Guid VehicleId);
}
