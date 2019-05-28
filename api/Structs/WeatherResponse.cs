using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Structs {
  readonly struct ResponseItem {
    [JsonProperty(PropertyName = "day")]
    internal DayOfWeek day { get; }
    [JsonProperty(PropertyName = "temp")]
    internal double temp { get; }
    [JsonProperty(PropertyName = "humidity")]
    internal int humidity { get; }
    [JsonProperty(PropertyName = "windspeed")]
    internal double windspeed { get; }

    internal ResponseItem(
      DayOfWeek day, double temp, int humidity, double windspeed
    ) {
      this.day = day;
      this.temp = temp;
      this.humidity = humidity;
      this.windspeed = windspeed;
    }
  }

  readonly struct WeatherResponse {
    [JsonProperty(PropertyName = "current")]
    internal ResponseItem current { get; }
    [JsonProperty(PropertyName = "forecast")]
    internal IEnumerable<ResponseItem> forecast { get; }

    internal WeatherResponse(
      ResponseItem current,
      IEnumerable<ResponseItem> forecast
    ) {
      this.current = current;
      this.forecast = forecast;
    }
  }
}
