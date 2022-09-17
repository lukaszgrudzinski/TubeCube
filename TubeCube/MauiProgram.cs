using Microsoft.Maui.LifecycleEvents;
using MonkeyCache.FileStore;
using TubeCube.Models;
using TubeCube.Services;
using TubeCube.ViewModels;

namespace TubeCube;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("FiraSans-Light.ttf", "RegularFont");
				fonts.AddFont("FiraSans-Medium.ttf", "MediumFont");
			})
			.ConfigureEssentials(essential =>
			{
				essential.UseVersionTracking();
			})
			.ConfigureLifecycleEvents(events =>
			{
#if ANDROID
				events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

				static void MakeStatusBarTranslucent(Android.App.Activity activity)
				{
					activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);
					activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
					activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
                }
#endif
            });

		RegisterAppServices(builder.Services);

		return builder.Build();
	}

	private static void RegisterAppServices(IServiceCollection services)
	{
		services.AddSingleton(Connectivity.Current);

		Barrel.ApplicationId = Constants.AppId;

		services.AddSingleton(Barrel.Current);

		services.AddSingleton<IApiService, YoutubeRestService>();

		services.AddTransient<StartPageViewModel>();
	}
}
