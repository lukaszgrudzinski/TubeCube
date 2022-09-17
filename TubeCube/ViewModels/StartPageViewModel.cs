using TubeCube.Services;

namespace TubeCube.ViewModels;

public class StartPageViewModel : AppViewModelBase
{
    public StartPageViewModel(IApiService apiService) : base(apiService)
    {
        Title = "TubeCube";
    }

    public override async Task OnNavigatedTo(object? parameters)
    {
        SetDataLoadingIndicators(true);

        LoadingText = "Hold on, loading";

        await Task.Delay(3000);

        SetDataLoadingIndicators(false);
    }
}
