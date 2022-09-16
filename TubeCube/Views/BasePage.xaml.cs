using TubeCube.Framework.UI;

namespace MauiApp1;

public partial class BasePage : ContentPage
{
	public IList<IView> PageContent => PageContentGrid.Children;
	public IList<IView> PageIcons => PageIconGrid.Children;

	protected bool IsBackButtonEnabled
	{
		get => NavigateBackButton.IsEnabled;
		set => NavigateBackButton.IsEnabled = value;
	}

	public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(
		nameof(PageTitle),
		typeof(string),
		typeof(BasePage),
		string.Empty,
		propertyChanged: OnPageTitleChanged);

	private static void OnPageTitleChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if(bindable is BasePage page)
		{
			page.TitleLabel.Text = (string)newValue;
			page.TitleLabel.IsVisible = true;
		}
	}

	public string PageTitle
	{
		get => (string)GetValue(PageTitleProperty);
		set => SetValue(PageTitleProperty, value);
	}

    public static readonly BindableProperty PageModeProperty = BindableProperty.Create(
        nameof(PageMode),
        typeof(PageMode),
        typeof(BasePage),
        TubeCube.Framework.UI.PageMode.None,
        propertyChanged: OnPageModeChanged);

    private static void OnPageModeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BasePage page)
        {
			page.SetPageMode((PageMode)newValue);
        }
    }

	private void SetPageMode(PageMode pageMode)
	{
		switch (pageMode)
		{
			case PageMode.Menu:
				HamburgerButton.IsVisible = true;
				NavigateBackButton.IsVisible = false;
				CloseButton.IsVisible = false;
				break;
            case PageMode.Navigate:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = true;
                CloseButton.IsVisible = false;
                break;
            case PageMode.Modal:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = true;
                break;
			default:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = false;
                break;
        }
	}

	public PageMode PageMode
    {
        get => (PageMode)GetValue(PageModeProperty);
        set => SetValue(PageModeProperty, value);
    }

	public static readonly BindableProperty ContentDisplayModeProperty = BindableProperty.Create(
		nameof(ContentDisplayMode),
		typeof(ContentDisplayMode),
		typeof(BasePage),
		ContentDisplayMode.NoNavigationBar,
		propertyChanged: OnContentDisplayModePropertyChanged);

	private static void OnContentDisplayModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
	{
        if (bindable is BasePage page)
        {
            page.SetContentDisplayMode((ContentDisplayMode)newValue);
        }
    }

	private void SetContentDisplayMode(ContentDisplayMode contentDisplayMode)
	{
		switch(contentDisplayMode)
		{
			case ContentDisplayMode.NavigationBar:
				Grid.SetRow(PageContentGrid, 2);
				Grid.SetRowSpan(PageContentGrid, 1);
				break;
			case ContentDisplayMode.NoNavigationBar:
                Grid.SetRow(PageContentGrid, 0);
                Grid.SetRowSpan(PageContentGrid, 3);
				break;
			default:
				break;
        }
	}

	public ContentDisplayMode ContentDisplayMode
	{
		get => (ContentDisplayMode)GetValue(ContentDisplayModeProperty);
		set => SetValue(ContentDisplayModeProperty, value);
	}

	public BasePage()
	{
		InitializeComponent();

		NavigationPage.SetHasNavigationBar(this, false);

		SetPageMode(PageMode.None);

		SetContentDisplayMode(ContentDisplayMode.NoNavigationBar);
	}
}