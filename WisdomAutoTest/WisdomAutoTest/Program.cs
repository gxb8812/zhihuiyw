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

            //string[] result = "content-desc=\"�û���è������\" checkable=\"false\" checked=\"false\"".Split(new string[] { "\" checkable=\"false\"", "content-desc=\"" }, StringSplitOptions.RemoveEmptyEntries);

            //string[] a = { "1","2","3"};
            //Console.WriteLine(a[a.Length-1]);
            /* Console.Read();   */
            //������־·��
            string Path = "result\\" + DateTime.Now.ToShortDateString();
            Directory.CreateDirectory(Path);

            string PageIndex = Path + @"\��ҳ.bmp";
            string moveMachine= Path + @"\װ�ƻ���ת.bmp";
            string fixProblem = Path + @"\������ת.bmp";
            string sales = Path + @"\Ӫ����ת.bmp";
            string WiseFamily = Path + @"\�ǻۼ�ͥ.bmp";
            string Telephonediagnose = Path + @"\�̻����.bmp";
            string ITVdiagnose = Path + @"\ITV���.bmp";
            string getResourceTree = Path + @"\��Դ��.bmp";
            string ReverseStrategy = Path + @"\�������.bmp";
            string Perform = Path + @"\���ܲ�ѯ.bmp";
            string ADSLDdiagnose = Path + @"\������.bmp";
            string ChoosenTest = Path + @"\��������.bmp";
            string getNetworkLog = Path + @"\������¼.bmp";
            string lackFee = Path + @"\Ƿ�Ѳ�ѯ.bmp";
            string UserInnerNet = Path + @"\�û�������ѯ.bmp";

            string deviceName = Help.InitDevices()[0].ToString();//Ĭ��ȡ��һ̨�豸���в���
            Console.WriteLine("��ǰ�����豸Ϊ:" + deviceName);

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
            while (WebDriver.PageSource.Contains("����"))
            {
                WebDriver.FindElementById("android:id/button1").Click();
                Thread.Sleep(2000);
            }
            //��¼����
            action.Login();
            Thread.Sleep(2000);
            //��֤��ҳ����
            action.verifyIndex(PageIndex);
            
            //���빤��ҳ
            WebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'���ù���')]").Click();
            (DateTime.Now.ToString() + "��ʼ���빤��ҳ").log();
            Thread.Sleep(5000);
            
            if (WebDriver.PageSource.Contains("�̻����"))
            {
                //ִ�й��߹��ܲ���
                //(DateTime.Now.ToString() + "��ʼ�̻����").log();
                //action.Telephonediagnose(Telephonediagnose);
            }

            if (WebDriver.PageSource.Contains("ITV���"))
            {
                //(DateTime.Now.ToString() + "��ʼITV���").log();
                //action.ITVdiagnose(ITVdiagnose);
            }

            if (WebDriver.PageSource.Contains("��Դ��"))
            {
                //(DateTime.Now.ToString() + "��ʼ��Դ����ѯ").log();
                //action.getResourceTree(getResourceTree);
            }

            if (WebDriver.PageSource.Contains("�������"))
            {
                //(DateTime.Now.ToString() + "��ʼ�������").log();
                //action.ReverseStrategy(ReverseStrategy);
            }

            if (WebDriver.PageSource.Contains("���ܲ�ѯ"))
            {
               //(DateTime.Now.ToString() + "��ʼ���ܲ�ѯ").log();
               //action.Perform(Perform);
            }

            if (WebDriver.PageSource.Contains("������"))
            {
                //(DateTime.Now.ToString() + "��ʼ������").log();
                //action.ADSLDdiagnose(ADSLDdiagnose);
            }

            if (WebDriver.PageSource.Contains("��������"))
            {
                (DateTime.Now.ToString() + "��ʼ��������").log();
                action.ChoosenTest(ChoosenTest);
            }

            if (WebDriver.PageSource.Contains("������¼����֤"))
            {
            //    (DateTime.Now.ToString() + "��ʼ������¼����֤��ѯ").log();
            //    action.getNetworkLog(getNetworkLog);
            }
            if (WebDriver.PageSource.Contains("Ƿ�Ѳ�ѯ"))
            {
                //(DateTime.Now.ToString() + "��ʼǷ�Ѳ�ѯ").log();
                //action.lackFee(lackFee);
            }

            if (WebDriver.PageSource.Contains("�û�����"))
            {
                //(DateTime.Now.ToString() + "��ʼ�û�����").log();
                //action.UserInnerNet(UserInnerNet);
            }
            //������ҳ
            //WebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'��ҳ')]").Click();
            //Thread.Sleep(5000);
            //if (WebDriver.PageSource.Contains("�ǻۼ�ͥ"))
            //{

            //    (DateTime.Now.ToString() + "��ʼ�ǻۼ�ͥ").log();
            //    action.WiseFamily(WiseFamily);
            //}

            "������ϣ�".log();
            Console.Read();

        }


    }
}
