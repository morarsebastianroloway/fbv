using AutoMapper;
using FBV.API.ViewModels;
using FBV.Domain.Entities;

namespace FBV.API.Profiles
{
    public class PurchaseOrderProfile : Profile
    {
        public PurchaseOrderProfile()
        {
            CreateMap<PurchaseOrder, PurchaseOrderViewModel>()
                .ForMember(s => s.Lines, c => c.MapFrom(m => m.Lines))
                .ReverseMap();
        }
    }
}
