using System.Collections.Generic;
using System.Linq;
using System;

namespace Structs {

  readonly struct WeatherBuilder {

    WeatherQueryEntry todayQuery { get; }
    WeatherQueryList forecastQuery { get; }

    internal WeatherBuilder(
      WeatherQueryEntry todayQuery,
      WeatherQueryList forecastQuery
    ) {
      this.todayQuery = todayQuery;
      this.forecastQuery = forecastQuery;
    }

    internal WeatherResponse Build() => new WeatherResponse(
      this.BuildTodayItem(),
      this.BuildForecastItems()
    );

    ResponseItem BuildTodayItem() => new ResponseItem(
      day: this.DateFromUnix(this.todayQuery.dt).DayOfWeek,
      temp: Math.Round(this.todayQuery.main.temp, 1),
      humidity: this.todayQuery.main.humidity,
      windspeed: this.todayQuery.wind.speed
    );

    IEnumerable<ResponseItem> BuildForecastItems() {
      var self = this;
      int currentDay = this.DayFromUnix(todayQuery.dt);

      return from item in this.forecastQuery.list
        where self.DayFromUnix(item.dt) != currentDay
        group item by self.DayFromUnix(item.dt) into dayGroup
        select self.BuildForecastItem(dayGroup);
    }

    ResponseItem BuildForecastItem(
      IEnumerable<WeatherQueryEntry> dayGroup
    ) => new ResponseItem(
      day: this.DateFromUnix(dayGroup.First().dt).DayOfWeek,
      temp: Math.Round(dayGroup.Average(day =>
        (day.main.temp_min + day.main.temp_max) / 2d),
      1),
      humidity: (int) dayGroup.Average(day => day.main.humidity),
      windspeed: Math.Round(dayGroup.Average(day => day.wind.speed), 1)
    );

    int DayFromUnix(string unixTime) => this.DateFromUnix(unixTime).Day;

    DateTime DateFromUnix(string unixTime) =>
      DateTime.UnixEpoch.AddSeconds(int.Parse(unixTime));
  }
}
