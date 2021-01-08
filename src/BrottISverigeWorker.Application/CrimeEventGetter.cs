using BrottISverigeWorker.Common;
using BrottISverigeWorker.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrottISverigeWorker.Application
{

    public class CrimeEventGetter : ICrimeEventGetter
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CrimeEventGetter(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<PoliceEvent>> GetPoliceEventsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_configuration.GetValue<string>("POLICE_EVENT_URL"));
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new JsonDateTimeConverter());
            var policeEvents = JsonSerializer.Deserialize<IEnumerable<PoliceEvent>>(jsonResponse, options);

            return policeEvents;
        }

    }
}
