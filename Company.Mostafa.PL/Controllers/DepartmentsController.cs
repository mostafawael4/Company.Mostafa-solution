using Company.Mostafa.BLL.Interfaces;
using Company.Mostafa.BLL.Repositories;
using Company.Mostafa.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Mostafa.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartment _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsController(IDepartment departmentRepository , IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IActionResult> Index(string searchInput)
        {
            var result = Enumerable.Empty<Department>();
            if (string.IsNullOrEmpty(searchInput))
            {
             result=await _departmentRepository.GetAllAsync();

            }
            else
            {
                 result = await _departmentRepository.GetByNameAsync(searchInput);
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if(ModelState.IsValid)
            {
                var count =await _departmentRepository.AddAsync(model);
                if(count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id , string viewname="Details")
        {
            if (id is null) return BadRequest();
            var model=await _departmentRepository.GetAsync(id);
            if(model is null) return NotFound();
            return View(viewname,model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            /*if (id is null) return BadRequest();
            var model = _departmentRepository.Get(id);
            if (model is null) return NotFound();
            return View(model);*/
            return await Details(id,"Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]int? id,Department model)
        {
            if(id != model.Id) return BadRequest();
            try
            {
                if (ModelState.IsValid)
                {
                    var count =await _departmentRepository.UpdateAsync(model);
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            /* if(id is null) return BadRequest();
             var model = _departmentRepository.Get(id);
             return View(model);*/
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            var employees = await _unitOfWork.employeeRepository.GetAllAsync();
            foreach (var employee in employees)
            {
                if(employee.WorkFor.Name == department.Name)
                {
                    return RedirectToAction("NotDeleted");
                }
            }
                var count =await _departmentRepository.DeleteAsync(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            
            return View(department);
        }

        public IActionResult NotDeleted()
        {
            return View();
        }
    }
}

