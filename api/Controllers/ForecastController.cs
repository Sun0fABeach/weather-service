using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Structs;

namespace api.Controllers
{
  [Route("api/weather/[controller]")]
  public class ForecastController : ControllerBase
  {
    private readonly Models.Weather weather;

    public ForecastController(IHttpClientFactory clientFactory) {
      this.weather = new Models.Weather(clientFactory);
    }

    [HttpGet]
    public async Task<ActionResult> Get(
      [FromQuery] string city,
      [FromQuery] string zipCode
    )
    {
      if(city == null && zipCode == null) {
        return BadRequest("");
      }

      WeatherResponse? queryResult = await this.weather.GetForecast(
        city, zipCode
      );
      if(queryResult == null) {
        return NotFound("");
      }

      return Ok(queryResult);
    }
  }
}
