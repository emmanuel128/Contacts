using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Contacts.Models
{
    public class HttpHelper
    {
        static async void Get(string url)
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            List<Customer> posts = JsonConvert.DeserializeObject<List<Customer>>(content);
        }
    }
}
