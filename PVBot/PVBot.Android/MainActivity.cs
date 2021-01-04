﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Auth0.OidcClient;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using PVBot.Clients.UI;
using PVBot.Clients.UI.Properties;

namespace PVBot.Droid
{
    [Activity(Label = "PVBot", Theme = "@style/MainTheme",
                MainLauncher = true, LaunchMode = LaunchMode.SingleTask,
                ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "org.volatileprogramming.pvbot",
        DataHost = "volatileprogramming.us.auth0.com",
        DataPathPrefix = "/android/org.volatileprogramming.pvbot/callback")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.tabbar;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::XF.Material.Droid.Material.Init(this, savedInstanceState);
            AppCenter.Start("8f350a9e-67fb-4e89-8225-ab22b1bcfe9d",
                   typeof(Analytics), typeof(Crashes));

            LoadApplication(new App(new AndroidInitializer(this)));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            ActivityMediator.Instance.Send(intent.DataString);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        private readonly MainActivity _mainActivity;

        public AndroidInitializer(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = Secrets.Auth0Domain,
                ClientId = Secrets.Auth0ClientId,
                Scope = "openid offline_access profile email"
            }, _mainActivity);

            containerRegistry.RegisterInstance<IAuth0Client>(client);
        }
    }
}

