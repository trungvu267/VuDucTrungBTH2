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
    private bool StudentExists(string id)
    {
      return _context.Students.Any(e=>e.StudentId == id);
    }
    public async Task<IActionResult> Edit(string id)
    {
      if(id == null)
      {
        return NotFound();
      }
      var student = await _context.Students.FindAsync(id);
      if(student == null)
      {
        return NotFound();
      } 
      return View(student);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(string id,[Bind("StudentId, StudentName")] Student std)
    {
      if(id != std.StudentId)
      {
        return NotFound();
      }
      if(ModelState.IsValid)
      {
        try
        {
          _context.Update(std);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if(!StudentExists(std.StudentId))
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
      return View(std);
    }
    public async Task<IActionResult> Delete(string id)
    {
      if(id == null)
      {
        return NotFound();
      }

      var std = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);
      if (std == null)
      {
        return NotFound();
      }
      return View(std);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeleteConfirm(string id)
    {
      var std = await _context.Students.FindAsync(id);
      _context.Students.Remove(std);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
  }
}
