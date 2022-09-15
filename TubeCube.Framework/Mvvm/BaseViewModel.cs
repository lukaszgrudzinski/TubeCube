using CommunityToolkit.Mvvm.ComponentModel;

namespace TubeCube.Framework.Mvvm;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isBusy = false;

    [ObservableProperty]
    private string _loadingText = string.Empty;

    [ObservableProperty]
    private bool _dataLoaded = false;

    [ObservableProperty]
    private bool _isErrorState = false;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _errorImage = string.Empty;

    public BaseViewModel() => IsErrorState = false;

    public async Task OnNavigatedTo(object parameters) => await Task.CompletedTask;

    protected void SetDataLoadingIndicators(bool isStarting = true)
    {
        if(isStarting)
        {
            IsBusy = true;
            DataLoaded = false;
            IsErrorState = false;
            ErrorMessage = string.Empty;
            ErrorImage = string.Empty;
        }
        else
        {
            IsBusy = false;
            LoadingText = string.Empty;
        }
    }
}
