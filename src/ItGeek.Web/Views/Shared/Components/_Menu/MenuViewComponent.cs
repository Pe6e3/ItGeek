using ItGeek.BLL;
using Microsoft.AspNetCore.Mvc;

//здесь имя папки в Components, в которой должно лежать представоение Default.cshtml
[ViewComponent(Name = "_Menu")] 
public class MenuViewComponent : ViewComponent
{
    private readonly UnitOfWork _uow;

    public MenuViewComponent(UnitOfWork uow)
    {
        _uow = uow;
    }
	public async Task<IViewComponentResult> InvokeAsync()
	{
		var menuItems = await _uow.MenuItemRepository.GetByMenuIdAsync(1);
		return View(menuItems);
	}

}


