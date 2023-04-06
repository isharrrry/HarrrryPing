using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace 云智慧
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM MainVM;
        public MainWindow()
        {
            InitializeComponent();
            //MessageBox.Show("正在检测网络。\nChecking Network.");
            //MessageBox.Show();
            //Application.Current.Shutdown();
            Loaded += MainWindow_Loaded;
            Unloaded +=MainWindow_Unloaded;
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = MainVM = new MainVM();
        }
    }

}
