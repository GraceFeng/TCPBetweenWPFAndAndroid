using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpListener listner = new TcpListener(IPAddress.Any, 8126);
        private NetworkStream netstream;
        private TcpClient client;
        private byte[] datalength = new byte[4];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread startconnection = new Thread(new ThreadStart(connect));
            startconnection.Start();
        }

        public void connect()
        {
            try
            {
                listner.Start(); //Starts Listening to any IP with port 8126
                //Invoked Just because of CrossThreading Error

                this.lbl_status.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() => { this.lbl_status.Text = "Status : Waiting for Client"; }));

                //Invoke Complete.

                //listner.Server.Listen(1);
                client = listner.AcceptTcpClient();

                if (client.Connected)
                {
                    this.lbl_status.Dispatcher.Invoke(DispatcherPriority.Normal,
                       new Action(() => { this.lbl_status.Text = "Status : Connected Successfully"; }));

                    //serverReceive();
                }
            }
            catch (Exception ex)
            {
                //lbl_status.Text = "Status : " + ex.Message;
                MessageBox.Show(ex.Message);
            }
        }
    }
}