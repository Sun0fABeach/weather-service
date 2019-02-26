namespace Structs {

  struct WeatherQueryList {
    public WeatherQueryEntry[] list { get; }

    public WeatherQueryList(WeatherQueryEntry[] list) {
      this.list = list;
    }
  }

  struct WeatherQueryEntry {
    public string dt { get; }
    public Main main { get; }
    public Weather[] weather { get; }
    public Wind wind { get; }

    public WeatherQueryEntry(
      string dt,
      Main main,
      Weather[] weather,
      Wind wind
    ) {
      this.dt = dt;
      this.main = main;
      this.weather = weather;
      this.wind = wind;
    }
  }

  struct Main {
    public double temp { get; }
    public double temp_min { get; }
    public double temp_max { get; }
    public int humidity { get; }

    public Main(double temp, double temp_min, double temp_max, int humidity) {
      this.temp = temp;
      this.temp_min = temp_min;
      this.temp_max = temp_max;
      this.humidity = humidity;
    }
  }

  struct Weather {
    public string id { get; }
    public string description { get; }
    public string icon { get; }

    public Weather(string id, string description, string icon) {
      this.id = id;
      this.description = description;
      this.icon = icon;
    }
  }

  struct Wind {
    public double speed { get; }

    public Wind(double speed) {
      this.speed = speed;
    }
  }
}