using System.Collections.Generic;
using System.Linq;
using System;

namespace Structs {

  static class WeatherBuilder {

    internal static WeatherResponse Build(
      in WeatherQueryEntry todayQuery,
      in WeatherQueryList forecastQuery
    ) => new WeatherResponse(
      BuildTodayItem(in todayQuery),
      BuildForecastItems(in todayQuery, in forecastQuery)
    );

    static ResponseItem BuildTodayItem(
      in WeatherQueryEntry todayQuery
    ) => new ResponseItem(
      day: DateFromUnix(todayQuery.dt).DayOfWeek,
      temp: Math.Round(todayQuery.main.temp, 1),
      humidity: todayQuery.main.humidity,
      windspeed: todayQuery.wind.speed
    );

    static IEnumerable<ResponseItem> BuildForecastItems(
      in WeatherQueryEntry todayQuery,
      in WeatherQueryList forecastQuery
    ) {
      int currentDay = DayFromUnix(todayQuery.dt);

      return from entry in forecastQuery.list
        let day = DayFromUnix(entry.dt)
        where day != currentDay
        group entry by day into dayGroup
        select BuildForecastItem(dayGroup);
    }

    static ResponseItem BuildForecastItem(
      IEnumerable<WeatherQueryEntry> dayGroup
    ) => new ResponseItem(
      day: DateFromUnix(dayGroup.First().dt).DayOfWeek,
      temp: Math.Round(dayGroup.Average(day =>
        (day.main.temp_min + day.main.temp_max) / 2d),
      1),
      humidity: (int) dayGroup.Average(day => day.main.humidity),
      windspeed: Math.Round(dayGroup.Average(day => day.wind.speed), 1)
    );

    static int DayFromUnix(string unixTime) => DateFromUnix(unixTime).Day;

    static DateTime DateFromUnix(string unixTime) =>
      DateTime.UnixEpoch.AddSeconds(int.Parse(unixTime));
  }
}
