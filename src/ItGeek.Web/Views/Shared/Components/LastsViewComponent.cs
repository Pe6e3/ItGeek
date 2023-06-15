using ItGeek.BLL;
using Microsoft.AspNetCore.Mvc;

public class LastsViewComponent : ViewComponent
{
    private readonly UnitOfWork _uow;

    public LastsViewComponent(UnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _uow.PostRepository.GetLastAsync(5));
    }
}