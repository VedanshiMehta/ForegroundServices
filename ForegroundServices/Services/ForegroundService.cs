using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using ForegroundServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForegroundServices.Services
{
    [Service]
    public class ForegroundService : Service, IForegroundServices
    {
        string channelId;
        public static bool isForgroundServiceRunning;
       

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Task.Run(() =>
            {
                while(isForgroundServiceRunning)
                {
                    Log.Debug("Foreground Services", "Foreground  service is running");
                    Thread.Sleep(2000);
                }

            });
            CreateNotificationChannel();
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, channelId)
                .SetContentTitle("ForegroundServiceStarted")
                .SetSmallIcon(Resource.Drawable.ic_notification)
                .SetContentText("Service is Running in foreground")
                .SetPriority(1)
                .SetOngoing(true)
                .SetChannelId(channelId)
                .SetAutoCancel(true);
            StartForeground(1001,builder.Build());
            return base.OnStartCommand(intent, flags, startId);
        }
        public override void OnCreate()
        {
            base.OnCreate();
            isForgroundServiceRunning = true;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            isForgroundServiceRunning=false;
        }
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt< BuildVersionCodes.O)
            {
                return;
            }
            channelId = "ForegroundServiceChannel";
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var notificationchannel = new NotificationChannel(channelId, channelId, NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(notificationchannel);

        }

        public void StartMyForegroundServices()
        {
            var intent = new Intent(Android.App.Application.Context,typeof(ForegroundService));
            Android.App.Application.Context.StartForegroundService(intent);
        }

        public void StopMyForegroundServices()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
            Android.App.Application.Context.StopService(intent);
        }

        public bool IsForegroundServiceRunning()
        {
            return isForgroundServiceRunning;
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}