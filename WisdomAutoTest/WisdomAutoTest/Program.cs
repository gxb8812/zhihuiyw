using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WisdomAutoTest;

namespace WisdomAutoTest
{
    class Program
    {
        public static DesiredCapabilities capabilities = new DesiredCapabilities();
        public static RemoteWebDriver WebDriver;
        static void Main(string[] args)
        {

            //string[] result = "content-desc=\"用户光猫不在线\" checkable=\"false\" checked=\"false\"".Split(new string[] { "\" checkable=\"false\"", "content-desc=\"" }, StringSplitOptions.RemoveEmptyEntries);

            //string[] a = { "1","2","3"};
            //Console.WriteLine(a[a.Length-1]);
            /* Console.Read();   */
            //配置日志路径
            string Path = "result\\" + DateTime.Now.ToShortDateString();
            Directory.CreateDirectory(Path);

            string PageIndex = Path + @"\首页.bmp";
            string moveMachine= Path + @"\装移机跳转.bmp";
            string fixProblem = Path + @"\修障跳转.bmp";
            string sales = Path + @"\营销跳转.bmp";
            string WiseFamily = Path + @"\智慧家庭.bmp";
            string Telephonediagnose = Path + @"\固话诊断.bmp";
            string ITVdiagnose = Path + @"\ITV诊断.bmp";
            string getResourceTree = Path + @"\资源树.bmp";
            string ReverseStrategy = Path + @"\反向决策.bmp";
            string Perform = Path + @"\性能查询.bmp";
            string ADSLDdiagnose = Path + @"\宽带诊断.bmp";
            string ChoosenTest = Path + @"\点名测试.bmp";
            string getNetworkLog = Path + @"\上网记录.bmp";
            string lackFee = Path + @"\欠费查询.bmp";
            string UserInnerNet = Path + @"\用户内网查询.bmp";

            string deviceName = Help.InitDevices()[0].ToString();//默认取第一台设备进行测试
            Console.WriteLine("当前运行设备为:" + deviceName);

            capabilities.SetCapability("browserName", "Android");
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("deviceName", deviceName);
            capabilities.SetCapability("version", "5.1.1");
            capabilities.SetCapability("app", "net.gdyuhui.wisdomcampd");
            capabilities.SetCapability("newCommandTimeout", 180000);
            //capabilities.SetCapability("unicodeKeyboard", true);
            capabilities.SetCapability("appActivity", "net.gdyuhui.wisdomcampd.StartActivity");
            WebDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            Action action = new Action(WebDriver);
            Thread.Sleep(2000);
            while (WebDriver.PageSource.Contains("重试"))
            {
                WebDriver.FindElementById("android:id/button1").Click();
                Thread.Sleep(2000);
            }
            //登录过程
            action.Login();
            Thread.Sleep(2000);
            //验证首页数据
            action.verifyIndex(PageIndex);
            
            //进入工具页
            WebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'常用工具')]").Click();
            (DateTime.Now.ToString() + "开始进入工具页").log();
            Thread.Sleep(5000);
            
            if (WebDriver.PageSource.Contains("固话诊断"))
            {
                //执行工具功能操作
                //(DateTime.Now.ToString() + "开始固话诊断").log();
                //action.Telephonediagnose(Telephonediagnose);
            }

            if (WebDriver.PageSource.Contains("ITV诊断"))
            {
                //(DateTime.Now.ToString() + "开始ITV诊断").log();
                //action.ITVdiagnose(ITVdiagnose);
            }

            if (WebDriver.PageSource.Contains("资源树"))
            {
                //(DateTime.Now.ToString() + "开始资源树查询").log();
                //action.getResourceTree(getResourceTree);
            }

            if (WebDriver.PageSource.Contains("反向决策"))
            {
                //(DateTime.Now.ToString() + "开始反向决策").log();
                //action.ReverseStrategy(ReverseStrategy);
            }

            if (WebDriver.PageSource.Contains("性能查询"))
            {
               //(DateTime.Now.ToString() + "开始性能查询").log();
               //action.Perform(Perform);
            }

            if (WebDriver.PageSource.Contains("宽带诊断"))
            {
                //(DateTime.Now.ToString() + "开始宽带诊断").log();
                //action.ADSLDdiagnose(ADSLDdiagnose);
            }

            if (WebDriver.PageSource.Contains("点名测试"))
            {
                (DateTime.Now.ToString() + "开始点名测试").log();
                action.ChoosenTest(ChoosenTest);
            }

            if (WebDriver.PageSource.Contains("上网记录与认证"))
            {
            //    (DateTime.Now.ToString() + "开始上网记录与认证查询").log();
            //    action.getNetworkLog(getNetworkLog);
            }
            if (WebDriver.PageSource.Contains("欠费查询"))
            {
                //(DateTime.Now.ToString() + "开始欠费查询").log();
                //action.lackFee(lackFee);
            }

            if (WebDriver.PageSource.Contains("用户内网"))
            {
                //(DateTime.Now.ToString() + "开始用户内网").log();
                //action.UserInnerNet(UserInnerNet);
            }
            //返回首页
            //WebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'首页')]").Click();
            //Thread.Sleep(5000);
            //if (WebDriver.PageSource.Contains("智慧家庭"))
            //{

            //    (DateTime.Now.ToString() + "开始智慧家庭").log();
            //    action.WiseFamily(WiseFamily);
            //}

            "运行完毕！".log();
            Console.Read();

        }


    }
}
