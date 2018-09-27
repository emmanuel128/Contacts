using Contacts.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Contacts.Data
{
	public interface IRestService
	{
		Task<List<Customer>> RefreshDataAsync ();

		Task<Customer> GetCustomerById (object customerId);

        Task<HttpResponseMessage> SaveCustomerAsync (Customer customer, bool isNewItem);

		Task<HttpResponseMessage> DeleteTodoItemAsync (object id);
	}
}
