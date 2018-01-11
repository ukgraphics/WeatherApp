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

            // 初期表示は仙台
            var sendai = new City();
            sendai.CityName = "仙台";
            sendai.ID = "040010";
            cbxCity.Text = sendai.CityName;
            cbxCity.SelectedItem = sendai;

            // 初期表示。仙台の天気概況を取得
            lblDesc.Text = WeatherInfo.GetDescription(sendai).description.text;

            // 初期表示。仙台の天気予報を取得
            grid.ItemsSource = WeatherInfo.GetForcast(sendai);

        }

        private void CbxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectcity = cbxCity.SelectedItem as City;

            // 天気概況を取得
            lblDesc.Text = WeatherInfo.GetDescription(selectcity).description.text;

            // 天気予報を取得
            grid.ItemsSource = WeatherInfo.GetForcast(selectcity);
        }
    }
}
