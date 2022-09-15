namespace MauiApp1;

public partial class ErrorIndicator : VerticalStackLayout
{
	public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
		nameof(IsError),
		typeof(bool),
		typeof(ErrorIndicator),
		false,
		BindingMode.OneWay,
		null,
		SetIsError);

	private static void SetIsError(BindableObject bindable, object oldValue, object newValue)
	{
		((ErrorIndicator)bindable).IsVisible = (bool)newValue;
	}

	public bool IsError
	{
		get => (bool)GetValue(IsErrorProperty);
		set => SetValue(IsErrorProperty, value);
	}

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText),
        typeof(string),
        typeof(ErrorIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetErrorText);

	private static void SetErrorText(BindableObject bindable, object oldValue, object newValue)
	{
		((ErrorIndicator)bindable).ErrorLabel.Text = (string)newValue;
    }

	public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public static readonly BindableProperty ErrorImageProperty = BindableProperty.Create(
       nameof(ErrorImage),
       typeof(string),
       typeof(ErrorIndicator),
       string.Empty,
       BindingMode.OneWay,
       null,
       SetErrorImage);

    private static void SetErrorImage(BindableObject bindable, object oldValue, object newValue)
    {
        ((ErrorIndicator)bindable).ErrorImageView.Source = (string)newValue;
    }

    public string ErrorImage
    {
        get => (string)GetValue(ErrorImageProperty);
        set => SetValue(ErrorImageProperty, value);
    }

    public ErrorIndicator()
	{
		InitializeComponent();
	}
}