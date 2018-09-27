namespace Contacts
{
	public static class Constants
	{
		// URL of REST service
		public static string RestUrl { get; set; }
	}

    public static class Endpoints
    {
        public static string Nodejs = "https://nodecustumers.herokuapp.com/api/customers/{0}";
        public static string DotNet = "https://vcustomers-api.azurewebsites.net/api/customers/{0}";
    }
}
