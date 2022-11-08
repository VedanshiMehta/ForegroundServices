using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using ForegroundServices.Services;
using System;

namespace ForegroundServices
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button _buttonStart;
        private Button _buttonStop;
        private ForegroundService _service;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            _service = new ForegroundService();
            UIReferences();
            UIClickEvents();
        }

        private void UIClickEvents()
        {
            _buttonStart.Click += _buttonStart_Click;
            _buttonStop.Click += _buttonStop_Click;

        }

        private void _buttonStop_Click(object sender, EventArgs e)
        {
            _service.StopMyForegroundServices();
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            if (_service.IsForegroundServiceRunning())
            {
                Toast.MakeText(this, "Foreground Service ids already running", ToastLength.Short).Show();
            }
            else
            {
                _service.StartMyForegroundServices();
            }
        }

        private void UIReferences()
        {
            _buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            _buttonStop = FindViewById<Button>(Resource.Id.buttonStop);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}