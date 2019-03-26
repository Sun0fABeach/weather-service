using System.Collections.Generic;
using System;

namespace Structs {
  public struct ResponseItem {
    public DayOfWeek day { get; set; }
    public double temp { get; set; }
    public int humidity { get; set; }
    public double windspeed { get; set; }
  }

  public struct WeatherResponse {
    public ResponseItem current { get; }
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
