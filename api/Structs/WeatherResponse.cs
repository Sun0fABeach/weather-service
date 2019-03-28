using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Structs {
  public struct ResponseItem {
    [JsonProperty(PropertyName = "day")]
    public DayOfWeek day { get; set; }
    [JsonProperty(PropertyName = "temp")]
    public double temp { get; set; }
    [JsonProperty(PropertyName = "humidity")]
    public int humidity { get; set; }
    [JsonProperty(PropertyName = "windspeed")]
    public double windspeed { get; set; }
  }

  public struct WeatherResponse {
    [JsonProperty(PropertyName = "current")]
    public ResponseItem current { get; }
    [JsonProperty(PropertyName = "forecast")]
    public IEnumerable<ResponseItem> forecast { get; }

    public WeatherResponse(
      ResponseItem current,
      IEnumerable<ResponseItem> forecast
    ) {
      this.current = current;
      this.forecast = forecast;
    }
  }
}
