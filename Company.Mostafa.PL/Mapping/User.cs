using AutoMapper;
using Company.Mostafa.DAL.Models;
using Company.Mostafa.PL.ViewModels;

namespace Company.Mostafa.PL.Mapping
{
	public class User : Profile
	{
        public User()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}

