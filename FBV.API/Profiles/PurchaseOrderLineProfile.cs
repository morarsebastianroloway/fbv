using AutoMapper;
using FBV.API.ViewModels;
using FBV.Domain.Entities;

namespace FBV.API.Profiles
{
    public class PurchaseOrderLineProfile : Profile
    {
        public PurchaseOrderLineProfile()
        {
            CreateMap<PurchaseOrderLine, PurchaseOrderLineViewModel>()
                .ReverseMap();
        }
    }
}
