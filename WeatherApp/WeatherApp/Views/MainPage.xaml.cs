using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Xamarin.Forms;

namespace WeatherApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            var test = new City();
            var list = test.GetCityId();
            cbxCity.ItemsSource = list;
            cbxCity.DisplayMemberPath = "CityName";
            cbxCity.SelectedIndexChanged += CbxCity_SelectedIndexChanged;
        }

        private void CbxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectcity = cbxCity.SelectedItem as City;

            // 天気概況を取得(JSON)
            string url = "http://weather.livedoor.com/forecast/webservice/json/v1?city=" + selectcity.ID.ToString();
            WebClient webClient = new WebClient();
            string str = webClient.DownloadString(url);
            var weather = JsonConvert.DeserializeObject<RootObject>(str);
            lblDesc.Text = weather.description.text;

            // 天気予報を取得(XML)
            string url2 = "http://weather.livedoor.com/forecast/rss/area/" + selectcity.ID.ToString() + ".xml";
            WebClient webClient2 = new WebClient();
            webClient2.Encoding = System.Text.Encoding.UTF8;
            string str2 = webClient2.DownloadString(url2);
            XDocument xd = XDocument.Parse(str2);
            var ForcastList = new List<Forcast>();

            IEnumerable<XElement> xelist = xd.XPathSelectElements("//item[category='天気予報']");
            foreach (XElement el in xelist)
            {
                Debug.WriteLine(el);
                var Result = new Forcast();
                Result.description = el.Element("description").Value.ToString();
                var uri = new Uri(el.Element("image").Element("url").Value);
                var img = new Image { Source = ImageSource.FromUri(uri) };
                //img.Source = ImageSource.FromUri(uri);
                Result.icon = img;

                ForcastList.Add(Result);
            }

            grid.ItemsSource = ForcastList;
        }
    }
}
