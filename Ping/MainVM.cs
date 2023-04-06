using System.Diagnostics;
using System;
using System.Windows.Input;
using System.Data.Common;
using System.CodeDom.Compiler;
using System.ComponentModel;

namespace 云智慧
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool CanPing { get; set; } = true;
        public string LOGText { get; set; }
        public string Host { get; set; } = "baidu.com";
        public ICommand ClearLOG { get; set; }
        public ICommand Ping { get; set; }
        Process myPro;
        public MainVM()
        {
            ClearLOG = new DelegateCommand(ExecuteClearLOG, CanExecuteClearLOG);
            Ping = new DelegateCommand(ExecutePing, CanExecutePing);
        }

        private void ExecutePing()
        {
            CanPing = false;
            ProcessCallBack("ping", $"{Host} -t", MyPro_OutputDataReceived);
            ExecuteClearLOG();
            CanPing = true;
        }

        private bool CanExecutePing()
        {
            return CanPing;
        }

        private bool CanExecuteClearLOG()
        {
            return true;
        }

        private void ExecuteClearLOG()
        {
            LOGText = "";
            FirePropertyChanged(nameof(LOGText));
        }

        private void MyPro_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //Debug.WriteLine(e.Data);
            LOGText += e.Data;
            LOGText += "\n";
            FirePropertyChanged(nameof(LOGText));
        }

        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        public static string RunCmd(string cmdExe, string cmdStr)
        {
            string result = "";
            try
            {
                using (Process myPro = new Process())
                {
                    myPro.StartInfo.FileName = "cmd.exe";
                    myPro.StartInfo.UseShellExecute = false;
                    myPro.StartInfo.RedirectStandardInput = true;
                    myPro.StartInfo.RedirectStandardOutput = true;
                    myPro.StartInfo.RedirectStandardError = true;
                    myPro.StartInfo.CreateNoWindow = true;
                    myPro.Start();
                    //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    string str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                    //myPro.StandardInput.WriteLine("cls");
                    myPro.StandardInput.WriteLine(str);
                    myPro.StandardInput.AutoFlush = true;
                    //获取cmd窗口的输出信息
                    string output = myPro.StandardOutput.ReadToEnd();
                    myPro.WaitForExit();

                    result = output;
                }
            }
            catch
            {

            }
            return result;
        }
        public static void RunCmdCallBack(string cmdExe, string cmdStr, DataReceivedEventHandler dataReceivedEvent)
        {
            try
            {
                using (Process myPro = new Process())
                {
                    myPro.StartInfo.FileName = "cmd.exe";
                    myPro.StartInfo.UseShellExecute = false;//不显示shell   
                    myPro.StartInfo.RedirectStandardInput = true;//打开流输入
                    myPro.StartInfo.RedirectStandardOutput = true;//打开流输出
                    myPro.StartInfo.RedirectStandardError = true;//打开错误流
                    myPro.StartInfo.CreateNoWindow = true;//不创建窗口 
                    //myPro.StandardInput.AutoFlush = true;             //每次调用 Write()之后，将其缓冲区刷新到基础流
                    myPro.OutputDataReceived += dataReceivedEvent;
                    myPro.ErrorDataReceived += dataReceivedEvent;
                    myPro.Start();
                    myPro.BeginOutputReadLine();
                    myPro.BeginErrorReadLine();
                    //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    //myPro.StandardInput.WriteLine("cls");
                    myPro.StandardInput.WriteLine($"\"{cmdExe}\" {cmdStr}");
                    //myPro.StandardInput.AutoFlush = true;
                    myPro.WaitForExit();
                }
            }
            catch (Exception ex)
            {

            }
            return;
        }
        public void ProcessCallBack(string cmdExe, string cmdStr, DataReceivedEventHandler dataReceivedEvent)
        {
            try
            {
                if (myPro != null && !ConsoleInterop.StopConsoleProgram(myPro, 2000))
                {
                    myPro.Kill();
                }
                //if (myPro != null)
                //{
                //    myPro.CloseMainWindow();
                //    myPro.Kill();
                //}
                myPro = new Process();
                myPro.StartInfo.FileName = "cmd.exe";
                myPro.StartInfo.UseShellExecute = false;//不显示shell   
                myPro.StartInfo.RedirectStandardInput = true;//打开流输入
                myPro.StartInfo.RedirectStandardOutput = true;//打开流输出
                myPro.StartInfo.RedirectStandardError = true;//打开错误流
                myPro.StartInfo.CreateNoWindow = true;//不创建窗口 
                myPro.OutputDataReceived += dataReceivedEvent;
                myPro.ErrorDataReceived += dataReceivedEvent;
                myPro.Start();
                myPro.BeginOutputReadLine();
                myPro.BeginErrorReadLine();
                //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                //myPro.StandardInput.WriteLine("cls");
                myPro.StandardInput.AutoFlush = true;
                myPro.StandardInput.WriteLine($"\"{cmdExe}\" {cmdStr}");
            }
            catch (Exception ex)
            {

            }
            return;
        }
    }
}