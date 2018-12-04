using Newtonsoft.Json.Linq;
using System;
using System.ServiceModel;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WCFService;
using System.Collections.ObjectModel;

namespace WPFHost
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<LogInfo> LogInfos = new ObservableCollection<LogInfo>();
        ServiceHost host;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.listView.ItemsSource = LogInfos;
        }

        /// <summary>
        /// 开始服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StartService_Click(object sender, RoutedEventArgs e)
        {
            WCFService.Service service = new WCFService.Service();
            service.AddConten += AddContent;
            if (host == null)
            {
                host = new ServiceHost(service);
     
            }
            try
            {
                host.Open();
                btn_StartService.IsEnabled = false;
                btn_StopService.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //show log
        public void AddContent(LogInfo loginfo)
        {
            this.Dispatcher.Invoke(() => {
                LogInfos.Add(loginfo);
            });
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StopService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                host.Close();
                btn_StartService.IsEnabled = true;
                btn_StopService.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
