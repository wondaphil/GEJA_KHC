using Android.App;
using Android.Content.PM;
using Android.OS;

namespace GEJA_KHC_MAUI;

[Activity(Label = "Geja KHC", Icon = "@mipmap/geja_khc_icon", Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Portrait)]
public class MainActivity : MauiAppCompatActivity
{
}
