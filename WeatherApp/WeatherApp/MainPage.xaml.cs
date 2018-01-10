using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            string url = "http://weather.livedoor.com/forecast/webservice/json/v1?city=" + selectcity.ID.ToString();
            WebClient webClient = new WebClient();
            string str = webClient.DownloadString(url);
            var weather = JsonConvert.DeserializeObject<RootObject>(str);
            
            lblDesc.Text = weather.description.text;
            grid.ItemsSource = GetWeatherInfo(weather.forecasts);
        }

        public static ObservableCollection<Forecast> GetWeatherInfo(List<Forecast> forecasts)
        {
            var list = new ObservableCollection<Forecast>();
            for (int i = 0; i < forecasts.Count; i++)
            {
                list.Add(forecasts[i]);
            }  
            return list;
        }
    }
}
