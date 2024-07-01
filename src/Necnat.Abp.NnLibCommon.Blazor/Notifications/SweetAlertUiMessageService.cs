using CurrieTechnologies.Razor.SweetAlert2;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Blazor.Notifications
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IUiMessageService))]
    public class SweetAlertUiMessageService : IUiMessageService, IScopedDependency
    {
        protected readonly IStringLocalizer<AbpUiResource> _localizer;
        protected readonly SweetAlertService _swal;

        public SweetAlertUiMessageService(
            IStringLocalizer<AbpUiResource> stringLocalizer,
            SweetAlertService swal)
        {
            _localizer = stringLocalizer;
            _swal = swal;
        }

        public async Task<bool> Confirm(string message, string? title = null, Action<UiMessageOptions>? options = null)
        {
            var sweetAlertOptions = new SweetAlertOptions();
            sweetAlertOptions.Title = title == null ? title : _localizer[title];
            sweetAlertOptions.Text = message == null ? message : _localizer[message];
            sweetAlertOptions.Icon = SweetAlertIcon.Warning;
            sweetAlertOptions.ReverseButtons = true;
            sweetAlertOptions.ShowCancelButton = true;
            sweetAlertOptions.ConfirmButtonText = _localizer["Ok"];
            sweetAlertOptions.CancelButtonText = _localizer["Cancel"];

            var sweetAlertResult = await _swal!.FireAsync(sweetAlertOptions);
            return sweetAlertResult.IsConfirmed;
        }

        public async Task Error(string message, string? title = null, Action<UiMessageOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Error);
        }

        public async Task Info(string message, string? title = null, Action<UiMessageOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Info);
        }

        public async Task Success(string message, string? title = null, Action<UiMessageOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Success);
        }

        public async Task Warn(string message, string? title = null, Action<UiMessageOptions>? options = null)
        {
            await _swal!.FireAsync(
                title == null ? title : _localizer[title],
                message == null ? message : _localizer[message],
                SweetAlertIcon.Warning);
        }
    }
}
