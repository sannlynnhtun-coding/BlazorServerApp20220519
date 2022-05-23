using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace BlazorServerApp20220519.ConsoleApp
{
    class Program
    {
        static RestClient client = new RestClient("https://localhost:44383/");
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                //var request = new RestRequest("user", Method.Get);
                //var response = await client.GetAsync(request);
                //if (response.IsSuccessful)
                //{
                //    Console.WriteLine(response.Content);
                //}
                //else
                //{
                //    Console.WriteLine("API Error.");
                //}
                //await Add();
                //await Update();
                //await Edit();
                //await Delete();
                //await Edit();
                //await List();
                await Pagination();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        static async Task List()
        {
            var response = await client.GetJsonAsync<UserModel[]>("user");
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        static async Task Pagination()
        {
            var response = await client.GetJsonAsync<UserModel[]>("user/2/30");
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        static async Task Add()
        {
            var item = new UserModel
            {
                userCode = Guid.NewGuid().ToString(),
                userName = "mg mg"
            };
            //var request = new RestRequest("user", Method.Post);
            var response = await client.PostJsonAsync<UserModel, ResponseModel>("user", item);
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        static async Task Edit()
        {
            var response = await client.GetJsonAsync<UserModel>("user/" + 82);
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        static async Task Update()
        {
            var item = new UserModel
            {
                userCode = Guid.NewGuid().ToString(),
                userName = "kg kg"
            };
            var response = await client.PutJsonAsync<UserModel, ResponseModel>("user/" + 82, item);
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        static async Task Delete()
        {
            var item = new UserModel
            {
                userCode = Guid.NewGuid().ToString(),
                userName = "ma ma"
            };
            var request = new RestRequest("user/" + 82);
            var response = await client.DeleteAsync<ResponseModel>(request);
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }

    public class ResponseModel
    {
        public string respCode { get; set; }
        public string respDesp { get; set; }
    }

    public class UserModel
    {
        public int userId { get; set; }
        public string userCode { get; set; }
        public string userName { get; set; }
        public bool delFlag { get; set; }
    }
}
