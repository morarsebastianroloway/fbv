using AutoMapper;
using FBV.API.ViewModels;
using FBV.Domain.Entities;

namespace FBV.API.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ReverseMap();
        }
    }
}
