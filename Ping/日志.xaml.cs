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

namespace 云智慧
{
    /// <summary>
    /// 配置和日志.xaml 的交互逻辑
    /// </summary>
    public partial class 日志 : UserControl
    {
        public 日志()
        {
            InitializeComponent();
            Loaded +=日志_Loaded;
        }

        private void 日志_Loaded(object sender, RoutedEventArgs e)
        {
            //if (this.DataContext is MainVM vm)
            //{
            //    vm.PropertyChanged +=Vm_PropertyChanged;
            //}
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LOGText")
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    txtLog.ScrollToEnd();
                }));
            }
        }
    }
}
