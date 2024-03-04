using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Weather_Console_Application.Class_Weather.Main;

namespace Weather_Console_Application.Class
{
    public class WebRequest
    {
        public static (string, string) GetCityLocation(string city)
        {
            string url =
                $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=&appid=" + new ApiKey().Key;
            string data = HttpsReq(url);
            if (data != null)
            {
                JArray jArray = JArray.Parse(data);
                JObject geoObj = jArray[0] as JObject;
                string lat = geoObj["lat"].ToString();
                string lon = geoObj["lon"].ToString();
                return (lat, lon);
            }
            return ("", "");
        }

        public static Root GetCityWeather(string lat, string lon)
        {
            string url =
                $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid=" + new ApiKey().Key;
            string data = HttpsReq(url);
            if (data != null)
            {
                Root root = JsonConvert.DeserializeObject<Root>(data);
                return root;
            }
            return null;
        }

        private static string HttpsReq(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                HttpResponseMessage response = client.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            return null;
        }
    }
}

