using MauiApp1;
using TubeCube.Framework.Helpers;
using TubeCube.ViewModels;

namespace TubeCube.Views;

public class ViewBase<TViewModel> : BasePage where TViewModel : AppViewModelBase
{
    protected bool IsViewModelLoaded = false;

    protected TViewModel? ViewModel { get; set; }

    protected object? ViewModelParameters { get; set; }

    protected EventHandler? ViewModelInitialized;

    public ViewBase() : base()
    {

    }

    public ViewBase(object viewModelParameters) : base()
    {
        ViewModelParameters = viewModelParameters;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if(!IsViewModelLoaded)
        {
            BindingContext = ViewModel = ServiceLocator.GetService<TViewModel>();

            ViewModel.NavigationService = Navigation;

            ViewModel.PageService = this;

            ViewModelInitialized?.Invoke(this, EventArgs.Empty);

            ViewModel.OnNavigatedTo(ViewModelParameters);

            IsViewModelLoaded = true;
        }
    }
}
