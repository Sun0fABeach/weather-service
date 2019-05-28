using System.Threading.Tasks;
using System.Net.Http;
using Structs;

namespace api.Models {
  readonly struct Weather {

    const string apiKey = "fcadd28326c90c3262054e0e6ca599cd";
    const string apiBase = "https://api.openweathermap.org/data/2.5";
    readonly string queryBase;
    readonly HttpClient httpClient;

    internal Weather(IHttpClientFactory clientFactory) {
      this.httpClient = clientFactory.CreateClient();
      this.queryBase = $"APPID={Weather.apiKey}&units=metric";
    }

    async internal Task<WeatherResponse?> GetForecast(
      string city, string zipCode
    ) {
      string locationParam = city != null ?
                              $"q={city},de" :
                              $"zip={zipCode},de";
      string queryParams = $"{this.queryBase}&{locationParam}";

      Task<HttpResponseMessage> currentFetch = this.httpClient.GetAsync(
        $"{Weather.apiBase}/weather?{queryParams}"
      );
      Task<HttpResponseMessage> forecastFetch = this.httpClient.GetAsync(
        $"{Weather.apiBase}/forecast?{queryParams}"
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
      return WeatherBuilder.Build(await currentRead, await forecastRead);
    }
  }
}
