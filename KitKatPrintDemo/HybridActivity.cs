using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using Android.Print;

namespace KitKatPrintDemo
{
    [Activity (Label = "HybridActivity")]
    public class HybridActivity : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Hybrid);

            var webView = FindViewById<WebView> (Resource.Id.webView);
            webView.Settings.JavaScriptEnabled = true;

            var hybridClient = new HybridWebViewClient ();

            hybridClient.PrintClicked += (sender, e) => {
                var printMgr = (PrintManager)GetSystemService(Context.PrintService);
                printMgr.Print("Razor HMTL Hybrid", webView.CreatePrintDocumentAdapter(), null);
            };

            webView.SetWebViewClient (hybridClient);

            var model = new Model1 () { Text = "Text goes here" };
            var template = new RazorView () { Model = model };
            var page = template.GenerateString ();

            webView.LoadDataWithBaseURL ("file:///android_asset/", page, "text/html", "UTF-8", null);

        }

        class HybridWebViewClient : WebViewClient
        {
            public event EventHandler PrintClicked = delegate {};

            public override bool ShouldOverrideUrlLoading (WebView webView, string url)
            {
                var scheme = "hybrid:";

                if (!url.StartsWith (scheme))
                    return false;
                    
                var resources = url.Substring (scheme.Length).Split ('?');
                var method = resources [0];
                var parameters = System.Web.HttpUtility.ParseQueryString (resources [1]);

                if (method == "UpdateLabel") {
                    var textbox = parameters ["textbox"];

                    var prepended = string.Format ("C# says \"{0}\"", textbox);

                    var js = string.Format ("SetLabelText('{0}');", prepended);

                    webView.LoadUrl ("javascript:" + js);

                } else if (method == "Print") {

                    PrintClicked (this, new EventArgs ());
                }

                return true;
            }
        }
    }
}