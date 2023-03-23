using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.Enums;

namespace Repository.Configuration
{
    public class VehicleSeed : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasData
            (
                new Vehicle
                {
                    Id = new Guid("acf49e7d-4d0c-4ea0-b3cc-9aee88b88a7a"),
                    Mileage = 60000,
                    Model = "Fiat Siena",
                    Manufacturer = "Fiat",
                    Category = (int)VehicleCategoryEnum.Sedan,
                    Color = (int)VehicleColorEnum.White,
                    DailyRentalFee = 118.35
                },
                new Vehicle
                {
                    Id = new Guid("afb387cd-9ca2-4fe6-aff2-a2163c92cb59"),
                    Mileage = 17500,
                    Model = "HB20",
                    Manufacturer = "Hyundai",
                    Category = (int)VehicleCategoryEnum.Sedan,
                    Color = (int)VehicleColorEnum.Red,
                    DailyRentalFee = 156.00
                },
                new Vehicle
                {
                    Id = new Guid("3462f9e4-2bda-4d44-af37-5521a76f1419"),
                    Mileage = 35000,
                    Model = "Fiat Doblo",
                    Manufacturer = "Fiat",
                    Category = (int)VehicleCategoryEnum.Minivan,
                    Color = (int)VehicleColorEnum.Black,
                    DailyRentalFee = 256.00
                }
            );
        }
    }
}
