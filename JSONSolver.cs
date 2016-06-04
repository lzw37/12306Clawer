using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Crawing
{
    class JSONSolver
    {
        List<string> TrainList = new List<string>();
        List<string> TrainInfo = new List<string>();
        public void ReadJSON(string TrainID)
        {
            if (!File.Exists("timetable\\" + TrainID + ".txt"))
            {
                Console.WriteLine("Read Train " + TrainID + " failed!");
                return;
            }
            FileStream fs = new FileStream("timetable\\" + TrainID + ".txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string s = sr.ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(s);
            int i = 0;
            try
            {
                string startStation = jo["data"]["data"][i]["start_station_name"].ToString();
                string startTime = jo["data"]["data"][i]["start_time"].ToString();
                string info = TrainID + "," + startStation + "," + startTime + "," + startTime;
                TrainInfo.Add(info);
            }
            catch
            {
                Console.WriteLine("Read Train " + TrainID + " failed!");
                sr.Close();
                fs.Close();
            }
            i++;
            try
            {   
                while (true)
                {
                    string Station = jo["data"]["data"][i]["station_name"].ToString();
                    string arriveT = jo["data"]["data"][i]["arrive_time"].ToString();
                    string startT = jo["data"]["data"][i]["start_time"].ToString();
                    string info = TrainID + "," + Station + "," + arriveT + "," + startT;
                    TrainInfo.Add(info);
                    i++;
                }
            }
            catch
            {
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }
        public void GetTrainList()
        {
            FileStream fs = new FileStream("trainlist.csv", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            char[] splitor = new char[] { ',' };
            string s = sr.ReadLine();
            while (s != "" && s!=null)
            {
                string[] data = new string[2];
                data = s.Split(splitor);
                TrainList.Add(data[0].ToString());
                s = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
        }
        public void Solve()
        {
            GetTrainList();
            foreach (string trainID in TrainList)
            {
                ReadJSON(trainID);
            }
            FileStream fs = new FileStream("TTb.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string info in TrainInfo)
                sw.WriteLine(info);
            sw.Close();
            sw.Close();
            Console.WriteLine("Success!");
            Console.ReadKey();
        }
    }
}
