namespace TubeCube.Framework.Helpers;

public static class ServiceLocator
{
    public static TService GetService<TService>()
    {
        if(Current == null)
        {
            throw new InvalidOperationException("Unsuported platform");
        }
        return Current.GetService<TService>() ?? throw new InvalidOperationException($"Sevice not found {typeof(TService).FullName}");
    }

    private static IServiceProvider? Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
        MauiWinUIApplication.Current.Services;
#elif ANDROID
        MauiApplication.Current.Services;
#elif IOS || MACCATALYST
        MauiUIApplicationDelegate.Current.Services;
#else
        null;
#endif
}
