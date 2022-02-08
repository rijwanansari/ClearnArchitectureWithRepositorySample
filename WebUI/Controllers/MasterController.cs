using Application.Master;
using Domain.Master;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterServices _masterServices;

        public MasterController(IMasterServices masterServices)
        {
            _masterServices = masterServices;
        }
        public async Task<IActionResult> Index()
        {
            var appSettingDto = (await _masterServices.GetAppSettingsAsync()).Select(i => AppSettingVm.ConvertToView(i)).ToList();
            
            return View(appSettingDto);
        }
    }
}
