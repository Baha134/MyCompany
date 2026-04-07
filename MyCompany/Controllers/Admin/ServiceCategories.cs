using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain.Entities;


namespace MyCompany.Controllers.Admin
{
    public partial class AdminController
    {
        public async Task<IActionResult> ServiceCategoriesEdit(int id)
        {
            // в зависимости от наличия айди либо добавляем либо изменям запись
            ServiceCategory? entity = id == default
                ? new ServiceCategory()
                : await _dataManager.ServiceCategories.GetServiceCategoryByIdAsync(id);

            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> ServiceCategoriesEdit(ServiceCategory entity)
        {
            //в модели присутствуют ошибки возвращаем на доработку
            if (!ModelState.IsValid)
                return View(entity);
            await _dataManager.ServiceCategories.SaveServiceCategoryAsync(entity);

            _logger.LogInformation($"Добавлена/обновлена категория услуг с ID: {entity.Id}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ServiceCategoriesDelete(int id)
        {
            //так как в целях безопасности отключено каскадное удаление, то прежде чем удалить категорию убедитесь,
            // что на нем нет ссылки ни у одной из услуг
            await _dataManager.ServiceCategories.DeleteServiceCategoryAsync(id);
            _logger.LogInformation($"удалена категория услуг с ID: {id}");
            return RedirectToAction("Index");

        }
     }
}
