namespace MauiApp1;

public partial class LoadingIndicator : VerticalStackLayout
{
	public static BindableProperty IsBusyProperty = BindableProperty.Create(
		nameof(IsBusy),
		typeof(bool),
		typeof(LoadingIndicator),
		false,
		BindingMode.OneWay,
		null,
		SetIsBusy);

	private static void SetIsBusy(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (LoadingIndicator)bindable;

		control.IsVisible = (bool)newValue;
		control.ActivityIndicator.IsRunning = (bool)newValue;
    }

	public bool IsBusy
	{
		get => (bool)GetValue(IsBusyProperty);
		set => SetValue(IsBusyProperty, value);
	}

    public static BindableProperty LoadingTextProperty = BindableProperty.Create(
        nameof(LoadingText),
        typeof(string),
        typeof(LoadingIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetLoadingText);

	private static void SetLoadingText(BindableObject bindable, object oldValue, object newValue)
	{
		LoadingIndicator control = (LoadingIndicator)bindable;

		control.label.Text = (string)newValue;
	}

	public string LoadingText
	{
		get => (string)GetValue(LoadingTextProperty);
		set => SetValue(LoadingTextProperty, value);
	}


	public LoadingIndicator()
	{
		InitializeComponent();
	}
}