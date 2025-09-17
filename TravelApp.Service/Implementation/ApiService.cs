using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TravelApp.Domain.DTOs;
using TravelApp.Service.Interface;

namespace TravelApp.Service.Implementation
{
    public class ApiService : IApiService
    {
        private readonly string _apiKey;

        public ApiService(IConfiguration config)
        {
            _apiKey = config["ApiSettings:API_KEY"];
        }
        public async Task<CountryInfoDetailsDto?> GetCountryInfoAsync(string iso2)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.countrystatecity.in/v1/countries/{iso2}"),
                Headers =
            {
                { "X-CSCAPI-KEY", _apiKey },
            },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<CountryInfoApiDto>();

                return new CountryInfoDetailsDto
                {
                    Name = data.Name,
                    Capital = data.Capital,
                    Region = data.Region + ", " + data.Subregion,
                    Currency = data.Currency_Name + " ( " + data.Currency_Symbol + " ) ",
                    Phonecode = data.Phonecode
                };
            }
        }

        public async Task<FlagDataDto> GetCountryFlagAsync(string iso2)
        {
            var client = new HttpClient();
            var url = "https://countriesnow.space/api/v0.1/countries/flag/images";

            var requestBody = new { iso2 = iso2 };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            //response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<FlagResponseDto>();
            return data.Data;
        }
    }
}
