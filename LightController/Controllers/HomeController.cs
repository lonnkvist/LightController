using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LightController.Models;
using Newtonsoft.Json;

namespace LightController.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri("http://10.10.0.160/api/781DA7EBEC/lights/")
        };

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LightOn(int id)
        {
            await Client.PutAsync(id + "/state", new StringContent("{ \"on\": true }"));
            return Ok();
        }

        public async Task<IActionResult> LightOff(int id)
        {
            await Client.PutAsync(id + "/state", new StringContent("{ \"on\": false }"));
            return Ok();
        }

        public async Task<IActionResult> Sophia()
        {
            var ids = new[] { 4, 5, 6 };
            var lights = (await GetLights(ids)).ToArray();

            ViewData["ids"] = String.Join(",", ids);
            ViewData["name"] = nameof(Sophia);
            ViewData["initialLevel"] = lights.Max(x => x.state.bri);
            ViewData["on"] = lights.Any(x => x.state.on);

            return View();
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private static async Task SetLightState(int[] ids, string state)
        {
            await Task.WhenAll(ids.Select(i => SetLightState(i, state)));
        }

        private static async Task<IEnumerable<Light>> GetLights(int[] ids)
        {
            return await Task.WhenAll(ids.Select(GetLight));
        }

        private static async Task<Light> GetLight(int id)
        {
            var json = await Client.GetStringAsync(id.ToString());
            return JsonConvert.DeserializeObject<Light>(json);
        }

        private static async Task SetLightState(int id, string state)
        {
            await Client.PutAsync(id + "/state", new StringContent("{ \"on\": " + state + " }"));
        }
    }
}
