using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Crawing
{
    internal class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public AcceptAllCertificatePolicy()
        {
        }

        public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert, WebRequest wRequest, int certProb)
        {
            return true;
        }
    }
    class GetTrainInfo
    {
        public void GetMethod()
        {
            FileStream fs = new FileStream("SJZ-ZZD.csv", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            char[] splitor = new char[] { ',' };
            string data = sr.ReadLine();
            while (data != null && data != "")
            {
                string[] sp = new string[2];
                sp = data.Split(splitor);
                string trainID = sp[0].ToString();
                string trainQueryString = sp[1].ToString();
                Get(trainQueryString, trainID);
                data = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
        }
        public void Get(string trainQueryString,string TrainID)
        {
            string url = "https://kyfw.12306.cn/otn/czxx/queryByTrainNo?train_no=" + trainQueryString + "&from_station_telecode=EAY&to_station_telecode=ZZF&depart_date=2016-07-02";
            WebRequest rquest = WebRequest.Create(url);
            HttpWebRequest rq=(HttpWebRequest)rquest;
            rq.Accept = "*/*";
            rq.Method = "GET";
            rq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36 SE 2.X MetaSr 1.0";
            rq.Referer = "https://kyfw.12306.cn/otn/leftTicket/init";
            rq.Host = "kyfw.12306.cn";
            ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
            rq.CookieContainer = new CookieContainer();
            Cookie c = new Cookie("JSESSIONID", "0A01E81DFCDC2890ED930B6777530FC92E67D3EF89", "/otn", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            c = new Cookie("__NRF", "54683DC93F4EDE56AA28C07F851D5EBB", "/otn", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            c = new Cookie("_jc_save_toDate", "2016-07-02", "/", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            c = new Cookie("BIGipServerotn", "501743882.64545.0000", "/", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            c = new Cookie("_jc_save_fromStation", "%u90D1%u5DDE%u4E1C%2CZAF", "/", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            c = new Cookie("_jc_save_toStation", "%u77F3%u5BB6%u5E84%2CSJP", "/", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            c = new Cookie("_jc_save_fromDate", "2016-07-02", "/", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            c = new Cookie("_jc_save_wfdc_flag", "dc", "/", ".kyfw.12306.cn");
            rq.CookieContainer.Add(c);
            WebResponse response = rq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            try
            {
                string sr = reader.ReadToEnd();
                reader.Close();
                FileStream fs = new FileStream("timetable\\" + TrainID + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(sr);
                sw.Close();
                fs.Close();
                Console.WriteLine("Train " + TrainID + " is finished!");
            }
            catch(Exception ex)
            {
                reader.Close();
                Console.WriteLine(ex.Message);
            }
        }
    }


}
