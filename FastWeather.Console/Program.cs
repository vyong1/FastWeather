using FastWeather.Model.ZipCodes;
using FastWeather.Models.ZipCodes;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        ZipCodeDBContext dbContext = new ZipCodeDBContext();

        if (!File.Exists(ZipCodeDBContext.DB_FILENAME))
        {
            dbContext.Init();

            var zips = ZipCodeInfo.ReadFromFile(@"ZipCodes/US_ZipCodes.txt");
            foreach (var zip in zips)
            {
                dbContext.Add(zip);
            }

            dbContext.SaveChanges();
        }
        
        //await Request(100, -100);
    }

    public static async Task<string> Request(int latitude, int longitude)
    {
        // https://www.weather.gov/documentation/services-web-api
        string requestUrl = $"https://api.weather.gov/points/{latitude},{longitude}";
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response is an error code
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

    }
}

