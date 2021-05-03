using Newtonsoft.Json;
using RetornaDadosMoeds.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Resources;
using System.Threading.Tasks;

namespace RetornaDadosMoeds
{
    class Program 
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
            Worksheet.ReadCsvMoeda();
            Worksheet.ReadCsvCotacao();
           

        }

        static HttpClient client = new HttpClient();

        static void ShowMoeda(Moedas moeda)
        {
            Console.WriteLine($"Name: {moeda.Data_Fim}\tPrice: " +
                $"{moeda.Data_Inicio}\tCategory: {moeda.Moeda}");
        }

        static async Task<Uri> CreateMoedaAsync(Moedas moeda)
        {
            Queue<Moedas> filaMoeda = new Queue<Moedas>();
            filaMoeda.Enqueue(moeda);
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "Moeda", JsonConvert.SerializeObject(filaMoeda));
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }

        static async Task<Moedas> GetMoedaAsync(string path)
        {
            Moedas moeda = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                moeda = await response.Content.ReadAsAsync<Moedas>();
            }
            return moeda;
        }
       
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:44316/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Moedas moeda = new Moedas();
            try
            {
                var url = await CreateMoedaAsync(moeda);
                Console.WriteLine($"Created at {url}");

                moeda = await GetMoedaAsync(url.PathAndQuery);
                ShowMoeda(moeda);



            }
            catch (Exception e)

            {

                Console.WriteLine(e.Message);
            }



        }
    }
}
