using Application.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.ViewModels;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMasterServices _masterServices;
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; } = "";

        public List<AppSettingVm> AppSettingVms { get; set; }
        public IndexModel(IMasterServices masterServices)
        {
            _masterServices = masterServices;
        }

        public async Task OnGetAsync()
        {
            var appSettings = await _masterServices.GetAppSettingsAsync();
            if (appSettings == null)
            {
                Message = "No App Setting found.";
                return;
            }

            AppSettingVms = AppSettingVm.ConvertToView(appSettings);
        }
    }
}