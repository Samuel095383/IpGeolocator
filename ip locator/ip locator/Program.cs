using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;

namespace ip_locator
{


    public class Data
    {
        public string IP { get; set; }
        public string city { get; set; }
        public string region { get; set; }

        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }


        class Program
        {
            static async Task Main(string[] args)
            {
                Console.Title = "IP GEOLOCATOR";
                Console.Write("IP DA STOLKERARE: ");
                string ip = Console.ReadLine();
                string url = ($"https://ipinfo.io/{ip}/json");
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine("Richiesta effettuata con successo; ecco le informazioni: ");

                        string responseData = await response.Content.ReadAsStringAsync();

                        Data ipinfo = JsonConvert.DeserializeObject<Data>(responseData);

                        Console.Clear();
                        Console.WriteLine($"IP: {ipinfo.IP}");
                        Console.WriteLine($"Città: {ipinfo.city}");
                        Console.WriteLine($"Regione: {ipinfo.region}");
                        Console.WriteLine($"Paese: {ipinfo.country}");
                        Console.WriteLine($"Coordinate: {ipinfo.loc}");
                        Console.WriteLine($"Organizzazione: {ipinfo.org}");
                        Console.WriteLine("");

                        string[] coord = ipinfo.loc.Split(',');
                        Console.WriteLine($"Google Maps: https://www.google.it/maps/?q={coord[0]},{coord[1]}");

                        await Task.Delay(555000);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
