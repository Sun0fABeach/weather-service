using System.Collections.Generic;
using System.Linq;
using System;

namespace Structs {

  struct WeatherResponse {

    struct ResponseItem {
      public DayOfWeek day { get; set; }
      public double temp { get; set; }
      public int humidity { get; set; }
      public double windspeed { get; set; }
    }

    private WeatherQueryEntry todayQuery { get; }
    private WeatherQueryList forecastQuery { get; }

    public WeatherResponse(
      WeatherQueryEntry todayQuery,
      WeatherQueryList forecastQuery
    ) {
      this.todayQuery = todayQuery;
      this.forecastQuery = forecastQuery;
    }

    public object build() {
      return new {
        current = this.buildTodayItem(),
        forecast = this.buildForecastItems()
      };
    }

    private ResponseItem buildTodayItem() {
      return new ResponseItem {
        day = this.dateFromUnix(this.todayQuery.dt).DayOfWeek,
        temp = Math.Round(this.todayQuery.main.temp, 1),
        humidity = this.todayQuery.main.humidity,
        windspeed = this.todayQuery.wind.speed
      };
    }

    // private IEnumerable<IEnumerable<WeatherQueryEntry>> buildForecast() {
    private IEnumerable<ResponseItem> buildForecastItems() {
      var self = this;
      int currentDay = this.dayFromUnix(todayQuery.dt);

      return this.forecastQuery.list
        .Where(
          item => self.dayFromUnix(item.dt) != currentDay
        )
        .GroupBy(
          item => self.dayFromUnix(item.dt),
          (day, items) => items
        )
        .Select(
          group => self.buildForecastItem(group)
        );
    }

    private ResponseItem buildForecastItem(
      IEnumerable<WeatherQueryEntry> dayGroup
    ) {
      var self = this;

      return dayGroup
        .Select(
          entry => new {
            tempAvg = (entry.main.temp_min + entry.main.temp_max) / 2d,
            humidity = entry.main.humidity,
            windspeed = entry.wind.speed
          }
        )
        .Aggregate(
          new {
            tempAvgAcc = 0d,
            humidityAcc = 0,
            windspeedAcc = 0d
          },
          (acc, next) => new {
            tempAvgAcc = acc.tempAvgAcc + next.tempAvg,
            humidityAcc = acc.humidityAcc + next.humidity,
            windspeedAcc = acc.windspeedAcc + next.windspeed
          },
          item => new ResponseItem {
            day = self.dateFromUnix(dayGroup.First().dt).DayOfWeek,
            temp = Math.Round(item.tempAvgAcc / dayGroup.Count(), 1),
            humidity = item.humidityAcc / dayGroup.Count(),
            windspeed =  Math.Round(item.windspeedAcc / dayGroup.Count(), 1)
          }
        );
    }

    private int dayFromUnix(string unixTime) {
      return this.dateFromUnix(unixTime).Day;
    }

    private DateTime dateFromUnix(string unixTime) {
      return DateTime.UnixEpoch.AddSeconds(int.Parse(unixTime));
    }
  }
}
