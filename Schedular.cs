using System.Text;

namespace ArgaamSchedular
{
    public class Schedular
    {
        public static async Task SendHttpRequest(string url)
       {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Handle the response as needed
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("HTTP Request successful. Response content: " + content);
                }
                else
                {
                    // Handle the error
                    Console.WriteLine("HTTP Request failed. Status code: " + response.StatusCode);
                }
            }
        }
        public static async Task SendHttpPostRequest(string url, string requestBody)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle the response as needed
                    var responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("HTTP POST Request successful. Response content: " + responseData);
                }
                else
                {
                    // Handle the error
                    Console.WriteLine("HTTP POST Request failed. Status code: " + response.StatusCode);
                }
            }
        }
        public static async Task SendHttpPutRequest(string url, string requestBody)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle the response as needed
                    var responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("HTTP PUT Request successful. Response content: " + responseData);
                }
                else
                {
                    // Handle the error
                    Console.WriteLine("HTTP PUT Request failed. Status code: " + response.StatusCode);
                }
            }
        }
    }
}
