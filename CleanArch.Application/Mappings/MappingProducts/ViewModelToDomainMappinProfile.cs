using AutoMapper;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Mappings
{
    public class ViewModelToDomainMappinProfile :Profile
    {
        public ViewModelToDomainMappinProfile()
        {
            CreateMap<ProductViewModel, Product>();
        }
    }
}
