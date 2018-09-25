using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Contacts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TodoREST;

namespace Contacts.Data
{
	public class RestService : IRestService
	{
		HttpClient client;

		public List<Customer> Customers { get; private set; }
		public Customer Customer { get; private set; }

        public RestService ()
		{
			client = new HttpClient ();
		}

		public async Task<List<Customer>> RefreshDataAsync ()
		{
			Customers = new List<Customer> ();

			var uri = new Uri (string.Format (Constants.RestUrl, string.Empty));

			try {
				var response = await client.GetAsync (uri);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();
					Customers = JsonConvert.DeserializeObject <List<Customer>> (content);
				}
			} catch (Exception ex) {
				Debug.WriteLine (@"ERROR {0}", ex.Message);
			}

			return Customers;
		}

        public async Task<Customer> GetCustomerById(string customerId)
        {
            Customer = new Customer();

            var uri = new Uri(string.Format(Constants.RestUrl, customerId));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Customer = JsonConvert.DeserializeObject<Customer>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Customer;
        }

        public async Task<HttpResponseMessage> SaveCustomerAsync (Customer item, bool isNewItem = false)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems
			var uri = new Uri (string.Format (Constants.RestUrl, string.Empty));

			try {
				var json = JsonConvert.SerializeObject (item);
				var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem) {
					response = await client.PostAsync (uri, content);
				} else {
                    uri = new Uri(string.Format(Constants.RestUrl, item.CustomerID));
                    response = await client.PutAsync (uri, content);
				}
				
				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully saved.");
				}

                return response;
				
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponseMessage> DeleteTodoItemAsync (string id)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}
			var uri = new Uri (string.Format (Constants.RestUrl, id));

			try {
				var response = await client.DeleteAsync (uri);

				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully deleted.");	
				}
                return response;
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
	}
}
