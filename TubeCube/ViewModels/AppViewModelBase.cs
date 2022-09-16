using CommunityToolkit.Mvvm.Input;
using TubeCube.Framework.Mvvm;
using TubeCube.Services;

namespace TubeCube.ViewModels;

internal partial class AppViewModelBase : BaseViewModel
{
    public INavigation NavigationService { get; set; }
    public Page PageService { get; set; }
    protected IApiService ApiService { get; set; }

    public AppViewModelBase(IApiService apiService) : base()
    {
        ApiService = apiService;
    }

    [RelayCommand]
    private async Task NavigateBack() => await NavigationService.PopAsync();

    [RelayCommand]
    private async Task CloseModal() => await NavigationService.PopModalAsync();
}
