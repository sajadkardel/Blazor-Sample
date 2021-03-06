using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blazor.Server.Sample.Common
{
    public static class JsRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }

        public static async ValueTask ToastrError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }

        public static async ValueTask ShowDeleteConfirmationModal(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("showConfirmationModal");
        }

        public static async ValueTask HideDeleteConfirmationModal(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("hideConfirmationModal");
        }
    }
}
