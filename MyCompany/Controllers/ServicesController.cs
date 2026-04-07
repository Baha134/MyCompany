using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Infrastructure;
using MyCompany.Models;
using MyCompany.Domain.Entities;

namespace MyCompany.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager _dataManager;

        public ServicesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        public async Task <IActionResult> Index()
        {
            IEnumerable<Service> list = await _dataManager.Services.GetServicesAsync();
            //доменную сущность на клиенте использовать не рекомендуется поэтому оборочиваем ее в ДТО
            IEnumerable<ServiceDTO> listDTO = HelperDTO.TransformServices(list);



            return View(listDTO);
        }

        public async Task<IActionResult> Show(int id)
        {
            Service? entity = await _dataManager.Services.GetServiceByIdAsync(id);
            //услуги с данным айди не найдено отвечаем 404
            if (entity is null)
                return NotFound();

            //обарачиваем в дто
            ServiceDTO entityDTO = HelperDTO.TransformService(entity);

            return View(entityDTO);



        }
    }
}
