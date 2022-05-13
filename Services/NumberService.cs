using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class NumberService
    {
        private IConfiguration _configuration;
        public NumberService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Number> GetNumber()
        {
            try
            {
                Log.Information("Obtaining value from the backing service");
                string addressURL = _configuration.GetSection("addressURL").Value;
                HttpClient client = new HttpClient();
                HttpResponseMessage reponse = await client.GetAsync(addressURL);

                Number number;
                if (reponse.IsSuccessStatusCode)
                {
                    string responsenumber = await reponse.Content.ReadAsStringAsync();
                    number = JsonConvert.DeserializeObject<Number>(responsenumber);
                }
                else
                {
                    string errorMessage = "The number server had problems";
                    throw new NumberServiceNotFoundException(errorMessage);
                }
                return number;
            }
            catch (Exception)
            {
                string errorMessage = "The number server experienced unexpected problems";
                throw new NumberServiceException(errorMessage);
            }
            
        }
    }
}