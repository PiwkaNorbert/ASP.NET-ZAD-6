using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //IConfiguration
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using lab6API.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebMvc.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly HttpClient client;
        private readonly string WebApiPath;
        private readonly IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiPath = _configuration["lab6API:Url"];   //read from appsettings.json
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["lab6API:ApiKey"]);   //use on any http calls      
        }


        // GET: ClientController
        public async Task<ActionResult> Index()
        {
            List<WheaterItem> data = null;
            HttpResponseMessage response = await client.GetAsync(WebApiPath);
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<List<WheaterItem>>();  //requires System.Net.Http.Formatting.Extension
            }
            return View(data);
        }



        // GET: ClientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                WheaterItem data = await response.Content.ReadAsAsync<WheaterItem>();
                return View(data);
            }
            return NotFound();
        }


        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  //todo add deoc to slides
        public async Task<ActionResult> Create([Bind("Id,Date,TemperatureC,summary")] WheaterItem data)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(WebApiPath, data);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }



        // GET: ClientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                WheaterItem data = await response.Content.ReadAsAsync<WheaterItem>();
                return View(data);
            }
            return NotFound();
        }


        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Date,TemperatureC,summary")] WheaterItem data)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(WebApiPath + id, data);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }




        // GET: ClientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                WheaterItem data = await response.Content.ReadAsAsync<WheaterItem>();
                return View(data);
            }
            return NotFound();
        }


        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, int notUsed = 0)
        {
            HttpResponseMessage response = await client.DeleteAsync(WebApiPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }
    }
}

