using System;
using System.Threading;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace WisdomAutoTest
{
    static class Log
    {
        /// <summary>
        /// 扩展方法,控制台输入日志,并把日志写入日志文件
        /// </summary>
        /// <param name="Message">日志内容</param>
        public static void log(this string Message)
        {
            Console.WriteLine(Message);
            StreamWriter SW = new StreamWriter("log.txt", true);
            SW.WriteLine(Message);
            SW.Flush();
            SW.Close();
            SW.Dispose();
        }

        /// <summary>
        /// 扩展方法,控制台输入结果,并把结果写入结果文件和日志文件
        /// </summary>
        /// <param name="Message">日志内容</param>
        public static void result(this string Message)
        {
            Message.log();
            StreamWriter SW = new StreamWriter("result.txt", true);
            SW.WriteLine(Message);
            SW.Flush();
            SW.Close();
            SW.Dispose();
        }
    }
    class Action
    {

      
        public static RemoteWebDriver remoteWebDriver;
        public Action(RemoteWebDriver WebDriver)
        {
            remoteWebDriver = WebDriver;

           
        }
        int overtime = 100;
        /// <summary>
        /// 登录系统
        /// </summary>
        public void Login()
        {
           

            int timeout = 2;
            while (!remoteWebDriver.PageSource.Contains("忘记密码？"))
            {
                Thread.Sleep(2000);
                timeout += 2;
                (DateTime.Now + "APP初始化中,等待进入登录界面,已等待" + timeout + "秒... ...").log();
                if (remoteWebDriver.PageSource.Contains("重试"))
                {
                    remoteWebDriver.FindElementById("android:id/button1").Click();                   
                }
            }
           

            timeout = 0;//初始化超时时间
                        // remoteWebDriver.FindElementByXPath("//android.widget.EditText[contains(@bounds,'[54,780][1026,870]')]").SendKeys("");
                        //remoteWebDriver.FindElementByXPath("//android.widget.EditText[contains(@bounds,'[54,780][1026,870]')]").SendKeys("cj_linxf5");
                        // remoteWebDriver.FindElementByXPath("//android.widget.EditText[contains(@bounds,'[54,903][1026,993]')]").Clear();
                        // remoteWebDriver.FindElementByXPath("//android.widget.EditText[contains(@bounds,'[54,903][1026,993]')]").SendKeys("123456!Ab");
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View/android.view.View/android.view.View[3]/android.widget.EditText").Text == "请输入用户名")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View/android.view.View/android.view.View[3]/android.widget.EditText").SendKeys("cj_huangyh");
            }
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View/android.view.View/android.view.View[4]/android.widget.EditText").Text == "•••••")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View/android.view.View/android.view.View[4]/android.widget.EditText").SendKeys("123456!Ab");
            }
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"登录 \"]").Click();
            "用户cj_zhongxx登录中... ...".log();

            while (!remoteWebDriver.PageSource.Contains("，欢迎您"))//尊敬的林晓帆，欢迎您
            {
                Thread.Sleep(2000);
                (DateTime.Now + "用户登录中,等待进入用户主界面,已等待" + timeout + "秒... ...").log();
            }
           
            (DateTime.Now + "用户登录成功!").log();
            (DateTime.Now + "用户登录成功!").result();
            Thread.Sleep(2000);
        }

        public void verifyIndex(string path)
        {
            if (remoteWebDriver.PageSource.Contains("未读公告"))
	{
               remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'知道了')]").Click();
	}
            ("当前待办单:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[0,951][372,1008]')]").GetAttribute("name")).result();
            ("今日完成量:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[372,951][744,1008]')]").GetAttribute("name")).result();
            ("当月指标:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[744,951][1080,1008]')]").GetAttribute("name")).result();
            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);//
            
        }
        //帐号无权限，无法跳转
        #region
        ////装移机跳转
        //public void moveMachine(string path)
        //{

        //    string fix = "";
        //    remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[72,1161][192,1296]')]").Click();
        //    (DateTime.Now + ":点击装移机按钮").log();
        //    int fixTimeOut = 0;
        //    while (!remoteWebDriver.PageSource.Contains("联系客户") && fixTimeOut < 30)
        //    {
        //        Thread.Sleep(2000);
        //        (DateTime.Now.ToString() + "正在进入装移机界面... ...").log();
        //        fixTimeOut += 2;

        //    }

        //    if (!remoteWebDriver.PageSource.Contains("联系客户") && fixTimeOut > 26)
        //    {

        //        "跳转超时".log();
        //    }
        //    else
        //    {
        //        "跳转成功".log();
        //        if (remoteWebDriver.PageSource.Contains("没有找到符合条件的工单"))
        //        {
        //            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
        //            "【装移机】没有找到符合条件的工单".result();
        //        }
        //        else
        //        {
        //            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);

        //            ("用户名:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[201,504][1035,561]')]").GetAttribute("name")).result();
        //            ("接入号:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[201,558][1035,618]')]").GetAttribute("name")).result();
        //            ("地址:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[201,699][1014,792]')]").GetAttribute("name")).result();
        //        }
        //    }
        //    //关闭装移机界面

        //    (DateTime.Now + "装移机" + fix).result();
        //    while (!remoteWebDriver.PageSource.Contains("，欢迎您"))
        //    {

        //        Thread.Sleep(2000);
        //        remoteWebDriver.FindElementByXPath("//android.widget.Button[contains(@bounds,'[30,75][120,207]')]").Click();
        //        (DateTime.Now + "等待进入主界面... ...").log();
        //    }
        //    (DateTime.Now + "关闭装移机界面,进入到主界面成功!").log();
        //}


        ////修障跳转
        //public void fixProblem(string path)
        //{

        //    string fix = "";
        //    remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[336,1161][456,1281]')]").Click();
        //    (DateTime.Now + ":点击修障按钮").log();
        //    int fixTimeOut = 0;
        //    while (!remoteWebDriver.PageSource.Contains("联系客户") && fixTimeOut < 30)
        //    {
        //        Thread.Sleep(2000);
        //        (DateTime.Now.ToString() + "正在进入修障界面... ...").log();
        //        fixTimeOut += 2;

        //    }

        //    if (!remoteWebDriver.PageSource.Contains("联系客户") && fixTimeOut > 30)
        //    {
        //        fix = "跳转超时";
        //    }
        //    else
        //    {
        //        fix = "跳转成功";
        //        if (remoteWebDriver.PageSource.Contains("没有找到符合条件的工单"))
        //        {

        //            "【修障】没有找到符合条件的工单".result();
        //            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
        //        }
        //        else
        //        {


        //            //  ("用户名:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[201,504][1035,561]')]").GetAttribute("name")).result();
        //            //  ("接入号:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[201,558][1035,618]')]").GetAttribute("name")).result();
        //            //  ("地址:" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[201,699][1014,792]')]").GetAttribute("name")).result();
        //        }
        //    }
        //    //关闭装移机界面

        //    (DateTime.Now + "修障" + fix).result();
        //    while (!remoteWebDriver.PageSource.Contains("，欢迎您"))
        //    {
        //        Thread.Sleep(2000);
        //        remoteWebDriver.FindElementByXPath("//android.widget.Button[contains(@bounds,'[30,75][120,207]')]").Click();
        //        (DateTime.Now + "等待进入主界面... ...").log();
        //    }
        //    (DateTime.Now + "关闭修障界面,进入到主界面成功!").log();
        //}

        ////营销跳转
        //public void sales(string path)
        //{

        //    string fix = "";
        //    remoteWebDriver.FindElementByXPath("//android.widget.Image[contains(@bounds,'[600,1161][720,1281]')]").Click();
        //    (DateTime.Now + ":点击营销按钮").log();
        //    int fixTimeOut = 0;
        //    while (!remoteWebDriver.PageSource.Contains("com.gdtel.eshore.socialmanagernew:id/webview") && fixTimeOut < 30)
        //    {
        //        Thread.Sleep(2000);
        //        (DateTime.Now.ToString() + "正在进入营销界面... ...").log();
        //        fixTimeOut += 2;

        //    }

        //    if (!remoteWebDriver.PageSource.Contains("com.gdtel.eshore.socialmanagernew:id/webview") && fixTimeOut > 30)
        //    {
        //        "跳转超时".result();
        //    }
        //    else
        //    {
        //        "跳转成功".result();

        //    }
        //    Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
        //    //关闭装移机界面

        //    (DateTime.Now + "营销" + fix).result();

        //    while (!remoteWebDriver.PageSource.Contains("，欢迎您"))
        //    {

        //        Help.sendCMD("adb shell input keyevent 4");
        //        Thread.Sleep(5000);
        //        (DateTime.Now + "等待进入主界面... ...").log();
        //    }
        //    (DateTime.Now + "关闭营销界面,进入到主界面成功!").log();
        //}

        #endregion
        //智慧家庭
        public void WiseFamily(string path)
            {

            remoteWebDriver.FindElementByXPath("//android.widget.Image[contains(@content-desc,'ZHYW_homePage_icon_fisrt_zhjt.426ee19')]").Click();
            while (!remoteWebDriver.PageSource.Contains("测试记录")&& !remoteWebDriver.PageSource.Contains("提示"))
            {
               
                    Thread.Sleep(2000);
                    (DateTime.Now.ToString() + ":进入WI-FI质量评估界面中...").log();
                
               
            }

            if (remoteWebDriver.PageSource.Contains("提示") && remoteWebDriver.PageSource.Contains("确定"))
            {
                //输出提示报错内容
                remoteWebDriver.PageSource.Split(new string[] { "提示", "确定" }, StringSplitOptions.RemoveEmptyEntries)[1].result();

                Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
                remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'确定')]").Click();
                Thread.Sleep(2000);
                //remoteWebDriver.FindElementByXPath("//android.widget.Image[contains(@bounds,'[876,1161][996,1281]')]").Click();
                //(DateTime.Now.ToString() + ":点击智慧家庭按钮，重新进入WI-FI质量评估界面...").log();
            }
            else if (remoteWebDriver.PageSource.Contains("测试记录"))
           
            {
                remoteWebDriver.FindElementByXPath("//android.widget.Image[contains(@content-desc,'ZHYW_wifi_main_btn_bfan.bcf6a38')]").Click();
                (DateTime.Now.ToString() + ":点击扫描按钮，开始WI-FI质量评估...").log();
                remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'竣工前')]").Click();//[27,1038][540,1212]
                (DateTime.Now.ToString() + ":点击竣工前按钮，开始竣工前WI-FI质量评估...").log();
                while (!remoteWebDriver.PageSource.Contains("是否检测其他户型"))
                {
                    Thread.Sleep(2000);
                    (DateTime.Now.ToString() + ":检测中... ...").log();
                }
                remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'结束检测')]").Click();//[540,1029][1014,1188]
                int time = 0;
                while (!remoteWebDriver.PageSource.Contains("评估人") && time < overtime)
                {

                    (DateTime.Now.ToString() + ":评估中... ...").log();
                    Thread.Sleep(5000);
                    time += 5;
                }
                if (remoteWebDriver.PageSource.Contains("评估人"))
                {
                    //输出报告得分
                    Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
                    (DateTime.Now.ToString() + ":" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[183,237][357,378]')]").GetAttribute("text").ToString()).result();
                    //返回wifi质量评估主界面
                    remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[51,120][111,180]')]").Click();
                    Thread.Sleep(2000);
                        
                    (DateTime.Now.ToString() + ":返回wifi质量评估主界面").log();
                    //返回主界面
                    remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[51,120][111,180]')]").Click();
                    Thread.Sleep(2000);

                    (DateTime.Now.ToString() + ":返回主界面").log();
                }
                else
                {
                    Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);

                }

            }



        }

        /// <summary>
        /// 固话诊断→常用工具
        /// </summary>
        public void Telephonediagnose(string path)
        {
            
            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"固话诊断\"]").Click();
            Thread.Sleep(2000);
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[1]/android.view.View/android.view.View[3]/android.view.View[2]").Click();
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            //选择地市广州
            //  remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"广州\"]").Click();
            //  remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,342][1035,408]')]").Click();
            //选择常用地市 [0,777][1080,903]
            Thread.Sleep(5000);
            //(//android.view.View[@content-desc="广州"])[2]      [45,684][1035,750]
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])").Click();
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[3]/android.view.View[2]/android.view.View[1]").Click();
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[3]/android.view.View[2]/android.view.View[1]/android.view.View[4]/android.view.View").Click();
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,1389][996,1455]')]").Click();
            //[39,1599][1041,1731]
            while (!remoteWebDriver.PageSource.Contains("故障时间段"))
            {
                Thread.Sleep(2000);

            }
            //选择时间
            // remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[1]/android.view.View[1]/android.view.View").Click();

            // remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[1]/android.view.View[2]").Click();

            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            Thread.Sleep(2000);
            while (!remoteWebDriver.PageSource.Contains("确定"))
            {
                Thread.Sleep(5000);
                (DateTime.Now.ToString() + ":等待诊断中... ...").log();
            }
          
            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
            "############固话诊断:".result();
            string [] result = remoteWebDriver.PageSource.Split(new string[] { "instance=\"0\"><android.view.View index=\"0\" text=\"\" class=\"android.view.View\" package=\"net.gdyuhui.wisdomcampd\"", "确定" }, StringSplitOptions.None)[3].Split(new string
                [] { "\" checkable=\"false\"","content-desc=\""},StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 1)
                {
                    (DateTime.Now.ToString() + ":固话诊断结果：" + result[i].ToString()).result();
                }
            }

            //诊断结束
            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.widget.Image[@content-desc=\"AgwA8Ugpedjm6vkAAAAASUVORK5CYII=\"]").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面  
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[540,1026][1053,1200]')]").Click();
            //返回时间选择界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            //while (!remoteWebDriver.PageSource.Contains("故障时间段"))
            //{
            //    Thread.Sleep(2000);
            //}
            ////返回固话诊断主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            ////返回工具主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(3000);
            }
            (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();
            

        }


        /// <summary>
        /// ITV诊断→常用工具
        /// </summary>
        public void ITVdiagnose(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"ITV诊断\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(50000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])").Click();
            Thread.Sleep(1000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(5000);
                Help.sendCMD("adb shell input keyevent 4");
            }

            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]
            Thread.Sleep(5000);
            while (!remoteWebDriver.PageSource.Contains("确定"))
            {
                Thread.Sleep(10000);
                (DateTime.Now.ToString() + ":等待诊断中... ...").log(); 
            }
           
            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);

            string[] result = remoteWebDriver.PageSource.Split(new string[] { "instance=\"0\"><android.view.View index=\"0\" text=\"\" class=\"android.view.View\" package=\"net.gdyuhui.wisdomcampd\"", "确定" }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new string
                [] { "\" checkable=\"false\"", "content-desc=\"" }, StringSplitOptions.RemoveEmptyEntries);
            "############ITV诊断:".result();
            for (int i = 0; i < result.Length; i++)
            {
                if (i%2==1)
                {
                    (DateTime.Now.ToString() + "ITV:诊断结果：" + result[i].ToString()).result();
                }
               

            }

            //诊断结束
            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.widget.Image[@content-desc=\"AgwA8Ugpedjm6vkAAAAASUVORK5CYII=\"]").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面 
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[540,1026][1053,1200]')]").Click();
            //返回ITV诊断主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();

           
        }



        /// <summary>
        /// 资源树→常用工具
        /// </summary>
        public void getResourceTree(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"资源树\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(5000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"广州\"]").Click();
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }

            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]

            while (!remoteWebDriver.PageSource.Contains("选择需要操作的账号")) 
            {
                Thread.Sleep(1000);
            }
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[3]/android.view.View[2]/android.view.View[1]").Click(); //坐标
            (DateTime.Now.ToString() + ":选择宽带接入号查询资源树").log();

            while (!remoteWebDriver.PageSource.Contains("OLT") && !remoteWebDriver.PageSource.Contains("查询异常中断"))
            {
                Thread.Sleep(2000);
                (DateTime.Now.ToString() + ":等待诊断中... ...").log();
            }
            if (remoteWebDriver.PageSource.Contains("OLT"))
            {
                "############资源树查询:成功！".result();
               
            }
            else
            {
                (DateTime.Now.ToString() + ":诊断结果：查询异常中断").result();
            }

            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
            Thread.Sleep(10000);
            if (remoteWebDriver.PageSource.Contains("确定"))
            {
                remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
            }
            remoteWebDriver.FindElementByXPath("//android.widget.Image[@content-desc=\"AgwA8Ugpedjm6vkAAAAASUVORK5CYII=\"]").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[540,1026][1053,1200]')]").Click();
            //返回主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            ////返回工具主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();
            
        }

        /// <summary>
        /// 反向决策→常用工具
        /// </summary>
        public void ReverseStrategy(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"反向决策\"]").Click();
            Thread.Sleep(2000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            //Thread.Sleep(5000);
            ////remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            //remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])[2]").Click();
            //Thread.Sleep(2000);
            //if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            //{
            //    remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
            //    Thread.Sleep(2000);
            //    Help.sendCMD("adb shell input keyevent 4");
            //}


            //Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]

            while (!remoteWebDriver.PageSource.Contains("选择账号"))
            {
                Thread.Sleep(1000);
            }
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[0,279][1080,459]')]").Click();
            (DateTime.Now.ToString() + ":选择宽带接入号进行反向决策").log();
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[contains(@bounds,'[39,1422][1041,1554]')]").Click();
            (DateTime.Now.ToString() + ":选择一级OBD查询进行反向决策").log();


            while (!remoteWebDriver.PageSource.Contains("[948,381][1080,516]") && !remoteWebDriver.PageSource.Contains("查询异常中断"))
            {
                Thread.Sleep(2000);
                (DateTime.Now.ToString() + ":等待诊断中... ...").log();
            }
            "############反向决策:".result();

            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            Thread.Sleep(1000);
            //返回主界面
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("查询条件"))
            {
                Thread.Sleep(2000);
            }
            //返回工具主界面
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(3000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();

        }



        /// <summary>
        /// 性能查询→常用工具
        /// </summary>
        public void Perform(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"性能查询\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(5000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            //Thread.Sleep(1000);
            //remoteWebDriver.FindElementByXPath("//android.widget.RadioButton[contains(@bounds,'[33,465][93,528]')]").Click();
            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])[2]").Click();
            Thread.Sleep(1000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }
            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]
            while (!remoteWebDriver.PageSource.Contains("选择需要操作的账号"))
            {
                Thread.Sleep(1000);
            }
          //  remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[0,1530][1080,1713]')]").Click();//[0,1530][1080,1713]//[0,1359][1080,1542]
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'宽带接入号    广州')]").Click();//[0,1530][1080,1713]//[0,1359][1080,1542]

            (DateTime.Now.ToString() + ":选择宽带接入号查询资源树").log();

            Thread.Sleep(1000);
            while (!remoteWebDriver.PageSource.Contains("ONU信息"))
            {
                Thread.Sleep(5000);
                (DateTime.Now.ToString() + ":等待诊断中... ...").log();
            }
            "############性能查询:".result();
           // (DateTime.Now.ToString() + ":ONU信息：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[267,378][1041,471]')]").GetAttribute("content-desc").ToString()).result();
          //  (DateTime.Now.ToString() + ":OLT发送功率：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[345,1005][1041,1098]')]").Text).result();
           // (DateTime.Now.ToString() + ":OLT发送功率：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[345,1005][1041,1098]')]").GetAttribute("content-desc").ToString()).result();

            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);

            //返回ITV诊断主界面
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("查询条件"))
            {
                Thread.Sleep(2000);
            }
            //返回工具主界面
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();


        }


        /// <summary>
        /// ADSLD诊断→常用工具
        /// </summary>
        public void ADSLDdiagnose(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"宽带诊断\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(5000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();

            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])").Click();
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }
            //if (remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,1080][963,1155]')]").Text == "")
            //{
            //    remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,1080][963,1155]')]").SendKeys("ADSLD2131800323");
            //    Thread.Sleep(2000);
            //    Help.sendCMD("adb shell input keyevent 4");
            //}


            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]
            Thread.Sleep(1000);
            while (!remoteWebDriver.PageSource.Contains("确定"))
            {
                Thread.Sleep(5000);
                (DateTime.Now.ToString() + ":等待诊断中... ...").log();
            }
            //(DateTime.Now.ToString() + ":诊断结果：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[381,888][699,945]')]").GetAttribute("text").ToString()).result();
            // (DateTime.Now.ToString() + ":诊断结果TEXT：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[381,888][699,945]')]").Text).result();

            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);

            string[] result = remoteWebDriver.PageSource.Split(new string[] { "instance=\"0\"><android.view.View index=\"0\" text=\"\" class=\"android.view.View\" package=\"net.gdyuhui.wisdomcampd\"", "确定" }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new string
                [] { "\" checkable=\"false\"", "content-desc=\"" }, StringSplitOptions.RemoveEmptyEntries);
            "############宽带诊断:".result();
            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 1)
                {
                    (DateTime.Now.ToString() + "宽带:诊断结果：" + result[i].ToString()).result();
                }


            }

            //诊断结束
            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.widget.Image[@content-desc=\"AgwA8Ugpedjm6vkAAAAASUVORK5CYII=\"]").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面   
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[540,1026][1053,1200]')]").Click();
            //返回ITV诊断主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            ////返回工具主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();


        }


        /// <summary>
        /// 点名测试→常用工具
        /// </summary>
        public void  ChoosenTest(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"点名测试\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(5000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])").Click();
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }


            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]

            while (!remoteWebDriver.PageSource.Contains("选择需要操作的账号"))
            {
                Thread.Sleep(1000);
            }
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[3]/android.view.View[2]/android.view.View[1]").Click();
            (DateTime.Now.ToString() + ":选择宽带接入号进行点名测试").log();

            while (!remoteWebDriver.PageSource.Contains("[27,639][1053,1212]") && !remoteWebDriver.PageSource.Contains("查询异常中断") && !remoteWebDriver.PageSource.Contains("提示"))
            {
                Thread.Sleep(2000);
                (DateTime.Now.ToString() + ":正在远程测试......").log();
            }
            //if (remoteWebDriver.PageSource.Contains("[27,639][1053,1212]"))
            //{
            //    (DateTime.Now.ToString() + ":点名测试结果：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[111,852][969,924]')]").Text).result();
            //    "############点名测试:成功！".result();
            //    //(DateTime.Now.ToString() + ":诊断结果：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[264,300][816,717]')]").GetAttribute("text").ToString()).result();
            //    remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
            //    Thread.Sleep(2000);
            //    //(DateTime.Now.ToString() + ":诊断结果：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[264,300][816,717]')]")).result();
            //    //(DateTime.Now.ToString() + ":诊断结果TEXT：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[381,888][699,945]')]").Text).result();
            //    //remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
            //    Thread.Sleep(1000);

            //}
            //else
            //{
            //    (DateTime.Now.ToString() + ":诊断结果：查询失败").result();
            //    remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
            //}
            if (remoteWebDriver.PageSource.Contains("提示"))
            {


                string[] resultone = remoteWebDriver.PageSource.Split(new string[] { "<android.view.View index=\"1\" text=\"\" class=\"android.view.View\" package=\"net.gdyuhui.wisdomcampd\"", "确定" }, StringSplitOptions.RemoveEmptyEntries);
                string[] result = resultone[resultone.Length - 1].Split(new string[] { "\" checkable=\"false\"", "content-desc=\"" }, StringSplitOptions.RemoveEmptyEntries);
                "############点名测试:".result();
                for (int i = 0; i < result.Length; i++)
                {
                    if (i % 2 == 1)
                    {
                        (DateTime.Now.ToString() + "点名测试结果：" + result[i].ToString()).result();
                    }


                }
                Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
                //诊断结束
                remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
                Thread.Sleep(1000);

            }

            else
            {
                (DateTime.Now.ToString() + ":诊断结果：查询异常中断").result();
                remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[27,1038][1053,1212]')]").Click();
            }
            Thread.Sleep(1000);
            remoteWebDriver.FindElementByXPath("//android.widget.Image[@content-desc=\"AgwA8Ugpedjm6vkAAAAASUVORK5CYII=\"]").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面   
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[540,1026][1053,1200]')]").Click();

            ////返回主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            ////返回工具主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();

        }

        /// <summary>
        /// 上网记录日志→常用工具
        /// </summary>
        public void getNetworkLog(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"上网记录与认证\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(5000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])").Click();
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }


            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]

            while (!remoteWebDriver.PageSource.Contains("选择需要操作的账号"))
            {
                Thread.Sleep(1000);
            }
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[3]/android.view.View[2]/android.view.View[1]").Click();
            (DateTime.Now.ToString() + ":选择宽带接入号进行查询").log();

            while (!remoteWebDriver.PageSource.Contains("筛选") && !remoteWebDriver.PageSource.Contains("查询异常中断"))
            {
                Thread.Sleep(2000);
                (DateTime.Now.ToString() + ":等待查询中... ...").log();
            }
            if (remoteWebDriver.PageSource.Contains("筛选"))
            {
                "############宽带与上网认证查询:成功！".result();
             ("24小时内有"+   remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[939,378][1035,441]')]").Text+"条记录！").result();
            
            }
            else
            {
                (DateTime.Now.ToString() + ":诊断结果：查询异常中断").result();
            }

            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
            remoteWebDriver.FindElementByXPath("//android.widget.Image[@content-desc=\"AgwA8Ugpedjm6vkAAAAASUVORK5CYII=\"]").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面   
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[540,1026][1053,1200]')]").Click();
            //返回主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            ////返回工具主界面
            //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();

        }

        /// <summary>
        /// 欠费查询→常用工具
        /// </summary>
        public void lackFee(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"欠费查询\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(5000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])").Click();
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }


            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]

            while (!remoteWebDriver.PageSource.Contains("选择需要操作的账号"))
            {
                Thread.Sleep(1000);
            }
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[3]/android.view.View[2]/android.view.View[1]").Click();
            (DateTime.Now.ToString() + ":选择宽带欠费查询").log();

            while (!remoteWebDriver.PageSource.Contains("客户名称") && !remoteWebDriver.PageSource.Contains("查询异常中断"))
            {
                Thread.Sleep(2000);
                (DateTime.Now.ToString() + ":等待查询中... ...").log();
            }
            if (remoteWebDriver.PageSource.Contains("客户名称"))
            {
                "############宽带欠费查询:成功！".result();
               // ("用户名：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[108,654][972,705]')]").Text).result();

                //("用户状态：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[447,876][996,933]')]").Text ).result();

            }
            else
            {
                (DateTime.Now.ToString() + ":诊断结果：查询异常中断").result();
            }

            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);

            //返回主界面 点击常用工具返回按钮
            remoteWebDriver.FindElementByXPath("//android.widget.Image[@content-desc=\"AgwA8Ugpedjm6vkAAAAASUVORK5CYII=\"]").Click();
            //while (!remoteWebDriver.PageSource.Contains("查询条件"))
            //{
            //    Thread.Sleep(2000);
            //}
            //返回工具主界面   
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[540,1026][1053,1200]')]").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();

        }

        /// <summary>
        /// 用户内网查询→常用工具
        /// </summary>
        public void UserInnerNet(string path)
        {

            remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"用户内网\"]").Click();
            Thread.Sleep(2000);
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@content-desc,'区域')]").Click();
            Thread.Sleep(5000);
            //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[45,807][1035,873]')]").Click();
            remoteWebDriver.FindElementByXPath("(//android.view.View[@content-desc=\"广州\"])").Click();
            Thread.Sleep(2000);
            if (remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").Text == "输入接入号")
            {
                remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[2]/android.view.View[3]/android.view.View/android.view.View/android.widget.EditText").SendKeys("ADSLD2131800323");
                Thread.Sleep(2000);
                Help.sendCMD("adb shell input keyevent 4");
            }


            Thread.Sleep(3000);
            remoteWebDriver.FindElementByXPath("//android.widget.Button[@content-desc=\"确定 \"]").Click();
            //[39,1599][1041,1731]

            while (!remoteWebDriver.PageSource.Contains("选择需要操作的账号"))
            {
                Thread.Sleep(1000);
            }
            remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[0,1362][1080,1545]')]").Click();
            (DateTime.Now.ToString() + ":选择宽带用户内网查询").log();

            while (!remoteWebDriver.PageSource.Contains("重试") && !remoteWebDriver.PageSource.Contains("提示") && !remoteWebDriver.PageSource.Contains("确定"))
            {
                Thread.Sleep(2000);
                (DateTime.Now.ToString() + ":等待诊断中... ...").log();
            }
            if (remoteWebDriver.PageSource.Contains("提示")|| !remoteWebDriver.PageSource.Contains("确定"))
            {
                "############用户内网查询:成功！".result();
                //("用户内网查询结果：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[111,852][969,924]')]").Text).result();
                Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);
                Thread.Sleep(2000);
                remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();
                // remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[27,1038][1053,1212]')]").Click();
            }
            else
            {
                "############用户内网查询:成功！".result();
                //("用户内网查询结果：" + remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[111,852][969,924]')]").Text).result();

                string[] resultone = remoteWebDriver.PageSource.Split(new string[] { "instance=\"0\"><android.view.View index=\"0\" text=\"\" class=\"android.view.View\" package=\"net.gdyuhui.wisdomcampd\"", "确定" }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new string
                    [] { "\" checkable=\"false\"", "content-desc=\"" }, StringSplitOptions.RemoveEmptyEntries);//获取弹出框内容
                string[] result = resultone[resultone.Length - 3].Split(new string[] { "\" checkable=\"false\"", "content-desc=\"" }, StringSplitOptions.RemoveEmptyEntries);
                "############宽带诊断:".result();
                for (int i = 0; i < result.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        (DateTime.Now.ToString() + "用户内网查询:诊断结果：" + result[i].ToString()).result();
                    }
                }
                Thread.Sleep(2000);
                remoteWebDriver.FindElementByXPath("//android.view.View[@content-desc=\"确定\"]").Click();

                Thread.Sleep(2000);
                //remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();

                //remoteWebDriver.FindElementByXPath("//android.view.View[contains(@bounds,'[27,1038][1053,1212]')]").Click();
            }
            Help.BytetoImg(remoteWebDriver.GetScreenshot().AsBase64EncodedString, path);

            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();


            //返回主界面
            while (!remoteWebDriver.PageSource.Contains("查询条件"))
            {
                Thread.Sleep(2000);
            }
            //返回工具主界面
            remoteWebDriver.FindElementByXPath("//android.webkit.WebView[@content-desc=\"智慧营维\"]/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[2]/android.view.View").Click();
            while (!remoteWebDriver.PageSource.Contains("常用工具"))
            {
                Thread.Sleep(2000);
            }
           (DateTime.Now.ToString() + ":系统返回工具主界面成功！").log();

        }
    }
}
