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
    public class City
    {
        public string CityName {get; set;}
        public string ID { get; set; }

        public List<City> GetCityId()
        {
            var CityList = new List<City>();
            string url = "http://weather.livedoor.com/forecast/rss/primary_area.xml";
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string str = webClient.DownloadString(url);
            XDocument xd = XDocument.Parse(str);

            IEnumerable<XElement> xelist = xd.XPathSelectElements("//city");
            foreach (XElement el in xelist)
            {
                //Debug.WriteLine(el);
                var CityData = new City();
               
                CityData.CityName = el.Attribute("title").Value.ToString();
                CityData.ID = el.Attribute("id").Value.ToString();

                CityList.Add(CityData);
            }

            return CityList;
        }
    }
}