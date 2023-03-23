using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.Enums;

namespace Repository.Configuration
{
    public class BookingSeed : IEntityTypeConfiguration<Booking>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Booking> builder)
        {
            builder.HasData
            (
                new Booking
                {
                    Id = new Guid("9987b490-5b67-41b8-a9d6-9cc45b50481d"),
                    From = new DateTime(2019, 05, 09, 9, 15, 0), // this will initialize variable with a specific date(09/05/2019) and time(9:15:00).
                    To = new DateTime(2019, 06, 09, 9, 15, 0),
                    Status = (int)BookingStatusEnum.Finished,
                    PaymentReceived = true,
                    CustomerId = new Guid("2c52045b-b323-424e-bf04-c27dcc2e0a33"), // Fulano
                    VehicleId = new Guid("acf49e7d-4d0c-4ea0-b3cc-9aee88b88a7a"), // Fiat Siena
                },
                new Booking
                {
                    Id = new Guid("c3e363e2-9550-4d5e-8b33-de3a23f69884"),
                    From = new DateTime(2022, 12, 01, 12, 00, 0),
                    To = new DateTime(2022, 12, 02, 12, 00, 0),
                    Status = (int)BookingStatusEnum.Finished,
                    PaymentReceived = true,
                    CustomerId = new Guid("2c52045b-b323-424e-bf04-c27dcc2e0a33"), // Fulano
                    VehicleId = new Guid("afb387cd-9ca2-4fe6-aff2-a2163c92cb59"), // HB20
                },
                new Booking
                {
                    Id = new Guid("bd8512a0-dfc9-4572-b9b6-7e73db3b8c23"),
                    From = new DateTime(2023, 02, 01, 12, 00, 0),
                    To = new DateTime().AddMonths(2),
                    Status = (int)BookingStatusEnum.Rent,
                    PaymentReceived = false,
                    CustomerId = new Guid("20c11b5c-243c-4e41-acff-a2f1072aee2f"), // Cicrana
                    VehicleId = new Guid("3462f9e4-2bda-4d44-af37-5521a76f1419"), // Fiat doblo
                }
            );
        }
    }
}
