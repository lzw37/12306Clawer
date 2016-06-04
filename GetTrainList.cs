using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Crawing
{
    class GetTrainList
    {
        List<string> TrainInfoList = new List<string>();
        XmlTextReader xr = new XmlTextReader("SJZ-ZZD.xml");
        public void ReadFile()
        {
            while (xr.Read())
            {
                xr.MoveToContent();
                if (xr.NodeType == XmlNodeType.EndElement && xr.Name=="File")
                    break;
                if (!xr.IsStartElement())
                    continue;
                if (xr.Name == "tr")
                {
                    if (xr["datatran"] != null)
                    {
                        //这个tr是个车
                        string datatran = xr["datatran"].ToString();
                        string id = xr["id"].ToString();
                        string info = datatran + "," + id;
                        TrainInfoList.Add(info);
                    }
                }
            }
            xr.Close();
            FileStream fs = new FileStream("SJZ-ZZD.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string s in TrainInfoList)
            {
                sw.WriteLine(s);
            }
            sw.Close();
            fs.Close();
        }
    }
}
