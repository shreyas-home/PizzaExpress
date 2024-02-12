using Microsoft.AspNetCore.Mvc;
using PizzaExpress.Services.Interfaces;

namespace PizzaExpress.WebUI.ViewComponents
{
    public class PizzaMenuViewComponent : ViewComponent
    {
        ICatalogService _catalogService;
        public PizzaMenuViewComponent(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IViewComponentResult Invoke()
        {
            var items = _catalogService.GetItems();
            return View("~/Views/Shared/_PizzaMenu.cshtml", items);
        }
    }
}
