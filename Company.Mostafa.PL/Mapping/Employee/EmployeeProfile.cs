using AutoMapper;
using Company.Mostafa.DAL.Models;
using Company.Mostafa.PL.ViewModels;

namespace Company.Mostafa.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
           
        }
    }
}

