using Microsoft.AspNetCore.Components;
using Web.BlazorServer.ViewModels.Configuration.Setup.Api;

namespace Web.BlazorServer.Components.Shared.Others;

public partial class HiddenOptions
{
    [Parameter] public ApiSetupVM FormData { get; set; } = null!;
    [Parameter] public bool Viewing { get; set; } = false;
    [Parameter] public bool IsBusy { get; set; } = false;
}
