using AutoMapper;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Mappings
{
    public class DomainToViewModelMappinProfile : Profile
    {
        public DomainToViewModelMappinProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
