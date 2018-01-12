using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WeatherApp
{
    // Livedoor Weather Web Serviceから取得した地域を格納するデータモデル
    public class City
    {
        // 地域名
        public string CityName {get; set;}

        // 地域ID
        public string ID { get; set; }

        // リクエストする地域とIDを取得
        public List<City> GetCityId()
        {
            // コンボボックスに返す地域リスト
            var CityList = new List<City>();

            // Livedoor Weather Web Serviceで提供している全国の地点定義表
            string url = "http://weather.livedoor.com/forecast/rss/primary_area.xml";
            
            // XMLを文字列として取得
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string str = webClient.DownloadString(url);
            
            // XDocumentを作成
            XDocument xd = XDocument.Parse(str);
            
            // <city ****>の要素を取得
            IEnumerable<XElement> xelist = xd.XPathSelectElements("//city");
            
            // 地域名と地域IDのリストを作成
            foreach (XElement el in xelist)
            {
                var CityData = new City();
               
                CityData.CityName = el.Attribute("title").Value.ToString();
                CityData.ID = el.Attribute("id").Value.ToString();

                CityList.Add(CityData);
            }

            return CityList;
        }
    }
}