using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.Enums;

namespace Repository.Configuration
{
    public class CustomerSeed : IEntityTypeConfiguration<Customer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
        {
            builder.HasData
            (
                new Customer
                {
                    Id = new Guid("2c52045b-b323-424e-bf04-c27dcc2e0a33"),
                    Name = "Fulano",
                    Email = "fulano@email.com",
                    DateBirth = new DateTime(1990, 12, 01, 12, 00, 0),
                    Gender = (int)GenderEnum.Male,
                    Address = "Campus Universitário - Lagoa Nova, Natal - RN, 59078-970",
                    Details = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI"
                },
                new Customer
                {
                    Id = new Guid("20c11b5c-243c-4e41-acff-a2f1072aee2f"),
                    Name = "Cricrana",
                    Email = "cricrana@email.com",
                    DateBirth = new DateTime(2000, 04, 01, 12, 00, 0),
                    Gender = (int)GenderEnum.Female,
                    Address = "Av. Nevaldo Rocha, 3775 - Tirol, Natal - RN, 59015-450",
                    Details = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI"
                }
            );
        }
    }
}
