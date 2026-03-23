using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skygate_AspNet_MVC.Models;

namespace Skygate_AspNet_MVC.Controllers
{
    [Authorize]    
    public class AttendanceController : Controller
    {
        private readonly SkyGateContext _context;
        public AttendanceController(SkyGateContext context) => _context = context;
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Punch(string type)
        {    
            // 從 User Claims 中提取登入時儲存的 EmpID        
            var empId = User.FindFirst("EmpID")?.Value;
            if (string.IsNullOrEmpty(empId)) return RedirectToAction("Login", "Account");

            // 建立出勤紀錄並存入資料庫
            var log = new AttendanceLog
            {
                LogId = DateTime.Now.ToString("yyyyMMddHHmmss") + empId,
                EmpId = empId,
                LogTime = DateTime.Now,
                LogType = type, // 上班或下班
                DeviceId = "WEB",
                Remark = "網頁端打卡"
            };

            _context.AttendanceLogs.Add(log);
            await _context.SaveChangesAsync();

            TempData["Msg"] = $"{type}打卡成功！時間：{log.LogTime:yyyy-MM-dd HH:mm:ss}";
            return RedirectToAction("Index");
        }
    }
}
