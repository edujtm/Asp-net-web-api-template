

using AutoMapper;
using Entities.Models;

public record FakeCustomer(
    Guid Id,
    string? Name,
    string? Email,
    DateTime DateBirth,
    int Gender,
    string? Address,
    string? Details
// ICollection<Booking>? Bookings
)
{
    public static FakeCustomer DefaultValues = new FakeCustomer(
        Guid.NewGuid(),
        "Testelino Rei Delas",
        "testelino.cabecao@exemplo.com",
        new DateTime(1995, 7, 23),
        1,
        "Big Street Avenue, 420",
        "Some details"
    );

    public class FakeCustomerProfile : Profile
    {
        public FakeCustomerProfile()
        {
            CreateMap<FakeCustomer, Customer>();
        }
    }
};