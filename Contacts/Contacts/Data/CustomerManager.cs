using Contacts.Models;
using System;
using System.Collections.Generic;
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

		public Task SaveTaskAsync (Customer customer, bool isNewItem = false)
		{
			return restService.SaveTodoItemAsync (customer, isNewItem);
		}

		public Task DeleteTaskAsync (Customer customer)
		{
			return restService.DeleteTodoItemAsync (customer.CustomerID);
		}
	}
}
