using AutoMapper;
using Company.Mostafa.BLL.Interfaces;
using Company.Mostafa.DAL.Data.Contexts;
using Company.Mostafa.DAL.Models;
using Company.Mostafa.PL.Helpers;
using Company.Mostafa.PL.ViewModels;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Mostafa.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        /*private readonly IEmployee _employeeRepository;
        private readonly IDepartment _departmentRepository;*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Index(string searchInput)
        {
            var result=Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInput))
            {
                result= await _unitOfWork.employeeRepository.GetAllAsync();
            }
            else
            {
                result= await _unitOfWork.employeeRepository.GetByNameAsync(searchInput);
            }
            var employees=_mapper.Map<IEnumerable<EmployeeViewModel>>(result);
            /*ViewData["Message"] = "Hello from ViewData";
            ViewBag.Message = "Hello from ViewBag";*/
            //var result=_employeeRepository.GetAll();
            return View(employees);
        }

        public async Task<IActionResult> Search(string searchInput)
        {
            var result = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInput))
            {
                result = await _unitOfWork.employeeRepository.GetAllAsync();
            }
            else
            {
                result = await _unitOfWork.employeeRepository.GetByNameAsync(searchInput);
            }
            var employees = _mapper.Map<IEnumerable<EmployeeViewModel>>(result);
            /*ViewData["Message"] = "Hello from ViewData";
            ViewBag.Message = "Hello from ViewBag";*/
            //var result=_employeeRepository.GetAll();
            return PartialView("EmployeesTablePartialView",employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments= await _unitOfWork.departmentRepository.GetAllAsync();
            ViewData["Departments"]=departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if(ModelState.IsValid)
            {
                if (employee.Image is not null)
                {
                    employee.ImageName = DocumentSettings.UploadFile(employee.Image, "images");
                }
                var model=_mapper.Map<Employee>(employee);
                var count =await _unitOfWork.employeeRepository.AddAsync(model);
                if(count > 0)
                {
                    TempData["Message"] = "Employee Created Successfully";
                }
                else
                {
                    TempData["Message"] = "Employee Not Created Successfully";
                }
                    return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id,string viewname="Details")
        {
            if (id is null) return BadRequest();
            var employee=await _unitOfWork.employeeRepository.GetAsync(id);
            var result=_mapper.Map<EmployeeViewModel>(employee);
            if(employee is null) return NotFound();
            return View(viewname, result);
        }

        public async Task<IActionResult> Update(int? id)
        {
            var departments = await _unitOfWork.departmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;
            return await Details(id,"Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]int id,EmployeeViewModel employee)
        {
            if(id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                if(employee.Image is not null)
                {
                    if(employee.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(employee.ImageName, "images");
                    }
                    employee.ImageName = DocumentSettings.UploadFile(employee.Image, "images");
                }
                else
                {
                    employee.ImageName = employee.ImageName;
                }


                /*if(employee.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employee.ImageName, "images");
                    employee.ImageName = DocumentSettings.UploadFile(employee.Image, "images");
                }*/
                var result=_mapper.Map<Employee>(employee);
                var count =await _unitOfWork.employeeRepository.UpdateAsync(result);
                if(count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int id,EmployeeViewModel employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var result=_mapper.Map<Employee>(employee);
                var count =await _unitOfWork.employeeRepository.DeleteAsync(result);
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(employee.ImageName, "images");
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }
    }
}

