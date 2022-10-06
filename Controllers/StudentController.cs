using MvcDotNet_BTH.Data;
using MvcDotNet_BTH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MvcDotNet_BTH.Controllers
{
  public class StudentController : Controller
  {
    private readonly ApplicationDbContext _context;
    public StudentController (ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<IActionResult> Index()
    {
      var model = await _context.Students.ToListAsync();
      return View(model);
    }
    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create (Student std)
    {
      if(ModelState.IsValid)
      {
        _context.Add(std);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(std);
    }
  }
}
