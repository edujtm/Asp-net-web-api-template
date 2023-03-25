using AutoMapper;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Shared.DTOs;

namespace Asp_net_web_api_template
{
    public class MappingProfile : Profile
    {
        private readonly string FORMAT_DATE = "dd/MM/yyyy HH:mm:ss";
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForCtorParam("Age", opt => opt.MapFrom(x => DateTime.Today.Year - x.DateBirth.Year));

            CreateMap<Booking, BookingDto>()
                .ForCtorParam("From", opt => opt.MapFrom(x => x.From.ToString(FORMAT_DATE)))
                .ForCtorParam("To", opt => opt.MapFrom(x => x.To.ToString(FORMAT_DATE)))
                .ForCtorParam("TotalFee", opt => opt.MapFrom((src, dest) =>
                {
                    // calculate renting days x price day of a vehicle
                    return 00.00;
                }))
                .ForCtorParam("Status", opt => opt.MapFrom((src, dest) =>
                {
                    return src.Status switch
                    {
                        (int)BookingStatusEnum.OnProcessing => "Choosing a rent option on site",
                        (int)BookingStatusEnum.Rent => "Currently rent",
                        (int)BookingStatusEnum.Finished => "Finished",
                        _ => "Error"
                    };
                }));

            CreateMap<CustomerCreationDto, Customer>();
            CreateMap<BookingCreationDto, Booking>();

            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleCreationDto, Vehicle>();
        }
    }
}
