using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/weather/[controller]")]
  public class ForecastController : Controller
  {
    private readonly Models.Weather weather;

    public ForecastController(IHttpClientFactory clientFactory) {
      this.weather = new Models.Weather(clientFactory);
    }

    // GET stuff?city=leipzig
    [HttpGet]
    public async Task<ActionResult> Get(
      [FromQuery] string city,
      [FromQuery] string zipCode
    )
    {
      if(city == null && zipCode == null) {
        return BadRequest("");
      }

      object queryResult = await this.weather.getForecast(city, zipCode);
      if(queryResult == null) {
        return NotFound("");
      }

      return Json(queryResult);
    }
  }
}