using Domain.Master;
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public class AppSettingVm
    {
        public int Id { get; set; }
        [Required]
        public string ReferenceKey { get; set; } = String.Empty;
        public string Value { get; set; } = String.Empty;
        public string? Description { get; set; }
        public string Type { get; set; } = String.Empty;
        public static AppSettingVm ConvertToView(AppSetting appSetting)
        {
            return  new AppSettingVm
            {
                Id = appSetting.Id,
                ReferenceKey = appSetting.ReferenceKey,
                Value = appSetting.Value,
                Description = appSetting.Description,
                Type = appSetting.Type,
            };
        }
        public static AppSetting ConvertToDomain(AppSettingVm appSettingVm)
        {
            return new AppSetting
            {
                Id = appSettingVm.Id,
                ReferenceKey = appSettingVm.ReferenceKey,
                Value = appSettingVm.Value,
                Description = appSettingVm.Description,
                Type = appSettingVm.Type,
            };
        }
        public static List<AppSettingVm> ConvertToView(List<AppSetting> appSettings)
        {
            List<AppSettingVm> appSettingVms = new List<AppSettingVm>();
            foreach (var appSetting in appSettings)
                appSettingVms.Add(ConvertToView(appSetting));

            return appSettingVms;
        }
    }
}
