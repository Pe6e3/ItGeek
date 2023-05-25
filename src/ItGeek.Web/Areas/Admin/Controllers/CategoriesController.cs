using ItGeek.BLL;
using ItGeek.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : Controller
{

    private readonly UnitOfWork _uow;
    public CategoriesController(UnitOfWork uow) 
    {
        _uow = uow;
    }
    public async Task<IActionResult> Index()
    {
        return View(await _uow.CategoryRepository.ListAllAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _uow.CategoryRepository.GetByIDAsync(id));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Category category = await _uow.CategoryRepository.GetByIDAsync(id);
        if (category != null)
            await _uow.CategoryRepository.DeleteAsync(category);

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            await _uow.CategoryRepository.InsertAsync(category);
            return RedirectToAction(nameof(Index));

        }
        return View(category);
    }

    public async Task<IActionResult> Update(int id)
    {
        Category category = await _uow.CategoryRepository.GetByIDAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Category category)
    {
        if (ModelState.IsValid)
        {
            await _uow.CategoryRepository.UpdateAsync(category);
            return RedirectToAction(nameof(Index));

        }
        return View(category);
    }

}

