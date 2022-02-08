using Domain.Master;

namespace Application.Master.Dto
{
    public class AppSettingVm
    {
        public int Id { get; set; }
        public string ReferenceKey { get; set; } = String.Empty;
        public string Value { get; set; } = String.Empty;
        public string? Description { get; set; }
        public string Type { get; set; } = String.Empty;
        public static AppSettingVm ConvertToView(AppSetting appSetting)
        {
            return new AppSettingVm
            {
                Id = appSetting.Id,
                ReferenceKey = appSetting.ReferenceKey,
                Value = appSetting.Value,
                Description = appSetting.Description,
                Type = appSetting.Type,
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
