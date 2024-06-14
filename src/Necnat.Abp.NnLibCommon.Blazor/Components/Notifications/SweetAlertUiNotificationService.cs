using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.BlazoriseUI;
using Volo.Abp.DependencyInjection;

namespace Necnat.Abp.NnLibCommon.Blazor.Components.Notifications
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IUiNotificationService), typeof(BlazoriseUiNotificationService))]
    public class SweetAlertUiNotificationService : IUiNotificationService, IScopedDependency
    {
        [Inject] protected SweetAlertService? Swal { get; set; }

        public async Task Error(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await Swal!.FireAsync(title, message, SweetAlertIcon.Error);
        }

        public async Task Info(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await Swal!.FireAsync(title, message, SweetAlertIcon.Info);
        }

        public async Task Success(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await Swal!.FireAsync(title, message, SweetAlertIcon.Success);
        }

        public async Task Warn(string message, string? title = null, Action<UiNotificationOptions>? options = null)
        {
            await Swal!.FireAsync(title, message, SweetAlertIcon.Warning);
        }
    }
}
