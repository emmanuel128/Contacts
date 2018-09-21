using Contacts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Data
{
	public interface IRestService
	{
		Task<List<Customer>> RefreshDataAsync ();

		Task SaveTodoItemAsync (Customer item, bool isNewItem);

		Task DeleteTodoItemAsync (string id);
	}
}
