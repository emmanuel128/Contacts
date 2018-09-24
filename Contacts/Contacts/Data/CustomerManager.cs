using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Contacts.Data
{
	public class CustomerManager
	{
		IRestService restService;

		public CustomerManager (IRestService service)
		{
			restService = service;
		}

		public Task<List<Customer>> GetTasksAsync ()
		{
			return restService.RefreshDataAsync ();	
		}

        public Task<Customer> GetByIdTaskAsync(string id)
        {
            return restService.GetCustomerById(id);
        }

        public Task<HttpResponseMessage> SaveTaskAsync (Customer customer, bool isNewItem = false)
		{
			return restService.SaveCustomerAsync (customer, isNewItem);
		}

		public Task<HttpResponseMessage> DeleteTaskAsync (Customer customer)
		{
			return restService.DeleteTodoItemAsync (customer.CustomerID);
		}
	}
}
