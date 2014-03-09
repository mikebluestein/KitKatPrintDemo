using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KitKatPrintDemo
{
    [Activity (Label = "MainActivity", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light")]			
    public class MainActivity : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            var showPrint = FindViewById<Button> (Resource.Id.showPrintButton);

            showPrint.Click += (object sender, EventArgs e) => StartActivity (typeof(PrintActivity));

            var showHybrid = FindViewById<Button> (Resource.Id.showHybridButton);

            showHybrid.Click += (object sender, EventArgs e) => StartActivity (typeof(HybridActivity));
        }
    }
}