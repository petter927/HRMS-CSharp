using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Skygate_AspNet_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skygate_AspNet_MVC.Controllers
{
    [Authorize(Roles = "人事,系統管理員")] 
    public class EmployeesController : Controller
    {
        private readonly SkyGateContext _context;
        public EmployeesController(SkyGateContext context) => _context = context;
        
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string DeptId, string EmpId, string EmpName)
        {
            // 使用 IQueryable 實作動態條件篩選 (Dynamic Filtering)
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(DeptId))
                query = query.Where(e => e.DeptId.Contains(DeptId));
            if (!string.IsNullOrEmpty(EmpId))
                query = query.Where(e => e.EmpId.Contains(EmpId));
            if (!string.IsNullOrEmpty(EmpName))
                query = query.Where(e => e.EmpName.Contains(EmpName));
            
            ViewData["DeptId"] = DeptId;
            ViewData["EmpId"] = EmpId;
            ViewData["EmpName"] = EmpName;

            return View(await query.ToListAsync());
        }
        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        
        public IActionResult Create()
        {
            var model = new Employee
            {
                Status = "1" , // 預設為在職                
                HireDate = DateOnly.FromDateTime(DateTime.Now) // 預設為今天
            }; 
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpName,DeptId,Title,SupervisorId,Status,HireDate,LeaveDate,Email,Phone")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMsg"] = $"員工 {employee.EmpName} 已成功新增！";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {                    
                    ModelState.AddModelError("", "資料庫寫入失敗：" + ex.Message);
                }
            }
            
            ViewBag.ErrorMsg = "資料驗證失敗，請檢查欄位格式是否正確。";
            
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var err in errors) { /* 可以在這裡觀察錯誤訊息 */ }

            return View(employee);            
        }
        
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpId,EmpName,DeptId,Title,SupervisorId,Status,HireDate,LeaveDate,Email,Phone")] Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
