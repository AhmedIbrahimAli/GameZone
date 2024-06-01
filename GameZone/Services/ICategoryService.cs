using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface ICategoryService
    {

        public IEnumerable<SelectListItem> GetSelectedCategories();
    }
}
