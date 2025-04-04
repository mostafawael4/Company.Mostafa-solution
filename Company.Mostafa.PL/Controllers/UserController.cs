using AutoMapper;
using Company.Mostafa.DAL.Models;
using Company.Mostafa.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Mostafa.PL.Controllers
{
	[Authorize(Roles ="Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
        public UserController(UserManager<ApplicationUser> userManager , IMapper mapper)
        {
            _userManager = userManager;
			_mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchInput)
		{
			var users = Enumerable.Empty<UserViewModel>();
			if (string.IsNullOrEmpty(searchInput))
			{
				users=await _userManager.Users.Select(X => new UserViewModel()
				{
					Id = X.Id,
					FirstName = X.FirstName,
					LastName = X.LastName,
					Email = X.Email,
					Roles = _userManager.GetRolesAsync(X).GetAwaiter().GetResult()
				}).ToListAsync();            
            }
			else
			{
				users=await _userManager.Users.Where(U=>U.Email.ToLower().Contains(searchInput.ToLower())).Select(X=>new UserViewModel()
				{
                    Id = X.Id,
                    FirstName = X.FirstName,
                    LastName = X.LastName,
                    Email = X.Email,
                    Roles = _userManager.GetRolesAsync(X).GetAwaiter().GetResult()
                }).ToListAsync();
			}            
			return View(users);
		}
		[HttpGet]
		public async Task<IActionResult> Details(string? Id,string viewname="Details")
		{
			if (Id is null) return BadRequest();

			var result =await _userManager.FindByIdAsync(Id);
			if(result is null) return NotFound();

			var user = new UserViewModel()
			{
				Id = result.Id,
				FirstName = result.FirstName,
				LastName = result.LastName,
				Email = result.Email,
				Roles = _userManager.GetRolesAsync(result).GetAwaiter().GetResult()
            };
			return View(viewname,user);
		}
		[HttpGet]
		public async Task<IActionResult> Update(string? id)
		{
			return await Details(id,"Update");
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromRoute]string id,UserViewModel user)
		{
			if(id != user.Id) return BadRequest();

			if(ModelState.IsValid)
			{
				var result =await _userManager.FindByIdAsync(user.Id);
				if (result is null) return NotFound();

				result.FirstName = user.FirstName;
				result.LastName = user.LastName;
				result.Email = user.Email;

				var flag=await _userManager.UpdateAsync(result);
				if (flag.Succeeded)
				{
					return RedirectToAction("Index");
				}
			}
			return View(user);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute] string id,UserViewModel user)
		{
			if(id !=user.Id) return BadRequest();

			if(ModelState.IsValid)
			{
				var result =await _userManager.FindByIdAsync(user.Id);
				if(result is null) return NotFound();
				var flag =await _userManager.DeleteAsync(result);
				if (flag.Succeeded)
				{
					return RedirectToAction("Index");
				}
			}
			return View(user);
		}
	}
}

