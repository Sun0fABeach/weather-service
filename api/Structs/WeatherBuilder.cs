using System.Collections.Generic;
using System.Linq;
using System;

namespace Structs {

  struct WeatherBuilder {

    private WeatherQueryEntry todayQuery { get; }
    private WeatherQueryList forecastQuery { get; }

    public WeatherBuilder(
      WeatherQueryEntry todayQuery,
      WeatherQueryList forecastQuery
    ) {
      this.todayQuery = todayQuery;
      this.forecastQuery = forecastQuery;
    }

    public WeatherResponse Build() => new WeatherResponse(
      this.BuildTodayItem(),
      this.BuildForecastItems()
    );

    private ResponseItem BuildTodayItem() => new ResponseItem {
      day = this.DateFromUnix(this.todayQuery.dt).DayOfWeek,
      temp = Math.Round(this.todayQuery.main.temp, 1),
      humidity = this.todayQuery.main.humidity,
      windspeed = this.todayQuery.wind.speed
    };

    private IEnumerable<ResponseItem> BuildForecastItems() {
      var self = this;
      int currentDay = this.DayFromUnix(todayQuery.dt);

      return this.forecastQuery.list
        .Where(
          item => self.DayFromUnix(item.dt) != currentDay
        )
        .GroupBy(
          item => self.DayFromUnix(item.dt),
          (day, items) => items
        )
        .Select(
          group => self.BuildForecastItem(group)
        );
    }

    private ResponseItem BuildForecastItem(
      IEnumerable<WeatherQueryEntry> dayGroup
    ) => new ResponseItem {
      day = this.DateFromUnix(dayGroup.First().dt).DayOfWeek,
      temp = Math.Round(dayGroup.Average(day =>
        (day.main.temp_min + day.main.temp_max) / 2d),
      1),
      humidity = (int) dayGroup.Average(day => day.main.humidity),
      windspeed = Math.Round(dayGroup.Average(day => day.wind.speed), 1)
    };

    private int DayFromUnix(string unixTime) => this.DateFromUnix(unixTime).Day;

    private DateTime DateFromUnix(string unixTime) =>
      DateTime.UnixEpoch.AddSeconds(int.Parse(unixTime));
  }
}
