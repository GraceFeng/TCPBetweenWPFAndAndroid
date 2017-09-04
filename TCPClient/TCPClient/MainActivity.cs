using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Sockets;
using System.Net;
using System;

namespace TCPClient
{
    [Activity(Label = "TCPClient", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TcpClient tcpclient;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btn = FindViewById<Button>(Resource.Id.btn);
            btn.Click += (sender, e) =>
           {
               Toast.MakeText(this, "Connection waiting", ToastLength.Long);
               try
               {
                   IPAddress ip = IPAddress.Parse("10.106.93.197"); //Ip from textbox
                   tcpclient = new TcpClient();
                   tcpclient.ConnectAsync(ip, 8126);

                   if (tcpclient.Connected)
                   {
                       Toast.MakeText(this, "Connected Successfully!", ToastLength.Long);
                   }
               }
               catch (System.Exception ex)
               {
                   Toast.MakeText(this, ex.Message, ToastLength.Long);
               }
           };
        }
    }
}