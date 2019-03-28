using System.Threading.Tasks;
using System.Net.Http;
using Structs;

namespace api.Models {
  public class Weather {
    private readonly HttpClient httpClient;
    private readonly string apiKey = "fcadd28326c90c3262054e0e6ca599cd";
    private readonly string apiBase =
      "https://api.openweathermap.org/data/2.5";
    private readonly string queryBase;

    public Weather(IHttpClientFactory clientFactory) {
      this.httpClient = clientFactory.CreateClient();
      this.queryBase = $"APPID={this.apiKey}&units=metric";
    }

    async public Task<WeatherResponse?> GetForecast(
      string city, string zipCode
    ) {
      string locationParam = city != null ?
                              $"q={city},de" :
                              $"zip={zipCode},de";
      string queryParams = $"{this.queryBase}&{locationParam}";

      Task<HttpResponseMessage> currentFetch = this.httpClient.GetAsync(
        $"{this.apiBase}/weather?{queryParams}"
      );
      Task<HttpResponseMessage> forecastFetch = this.httpClient.GetAsync(
        $"{this.apiBase}/forecast?{queryParams}"
      );

      HttpResponseMessage current = await currentFetch;
      HttpResponseMessage forecast = await forecastFetch;

      if(!current.IsSuccessStatusCode || !forecast.IsSuccessStatusCode) {
        return null;
      }

      return await this.BuildWeatherResponse(current.Content, forecast.Content);
    }

    async Task<WeatherResponse> BuildWeatherResponse(
      HttpContent current,
      HttpContent forecast
    ) {
      var currentRead = current.ReadAsAsync<WeatherQueryEntry>();
      var forecastRead = forecast.ReadAsAsync<WeatherQueryList>();
      var weather = new WeatherBuilder(await currentRead, await forecastRead);
      return weather.Build();
    }
  }
}
