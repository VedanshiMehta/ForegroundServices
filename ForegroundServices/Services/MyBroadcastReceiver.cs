﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForegroundServices.Services
{
    [BroadcastReceiver(Enabled = true,Exported =true)]
    [IntentFilter(new[] {Intent.ActionBootCompleted})]
    public class MyBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.Action == Intent.ActionBootCompleted)
            {
                var foreGreound = new Intent(Android.App.Application.Context, typeof(ForegroundService));
                Android.App.Application.Context.StartForegroundService(foreGreound);
                context.StartForegroundService(foreGreound);
            }
        }
    }
}