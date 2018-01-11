using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Xamarin.Forms;

namespace WeatherApp
{
    public class WeatherInfo
    {
        public static RootObject GetDescription(City city)
        {
            string url = "http://weather.livedoor.com/forecast/webservice/json/v1?city=" + city.ID.ToString();
            WebClient webClient = new WebClient();
            string str = webClient.DownloadString(url);
            var weather = JsonConvert.DeserializeObject<RootObject>(str);

            return weather;
        }

        public static List<Forcast> GetForcast(City city)
        {
            string url2 = "http://weather.livedoor.com/forecast/rss/area/" + city.ID.ToString() + ".xml";
            WebClient webClient2 = new WebClient();
            webClient2.Encoding = System.Text.Encoding.UTF8;
            string str2 = webClient2.DownloadString(url2);
            XDocument xd = XDocument.Parse(str2);
            var ForcastList = new List<Forcast>();

            IEnumerable<XElement> xelist = xd.XPathSelectElements("//item[category='天気予報']");
            foreach (XElement el in xelist)
            {
                var Result = new Forcast();
                Result.description = el.Element("description").Value.ToString();
                var uri = new Uri(el.Element("image").Element("url").Value);
                var img = new Image { Source = ImageSource.FromUri(uri) };
                //img.Source = ImageSource.FromUri(uri);
                Result.icon = img;

                ForcastList.Add(Result);
            }

            return ForcastList;
        }
    }
}