using CurrieTechnologies.Razor.SweetAlert2;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Blazor.Notifications
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IUiNotificationService))]
    public class SweetAlertUiNotificationService : IUiNotificationService, IScopedDependency
    {
        protected readonly IStringLocalizer<AbpUiResource> _localizer;
        protected readonly SweetAlertService _swal;

        public SweetAlertUiNotificationService(
            IStringLocalizer<AbpUiResource> stringLocalizer,
            SweetAlertService swal)
        {
            _localizer = stringLocalizer;
            _swal = swal;
        }

        public async Task Error(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Error);
        }

        public async Task Info(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Info);
        }

        public async Task Success(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Success);
        }

        public async Task Warn(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Warning);
        }
    }
}
