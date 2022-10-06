namespace RestSharpNUnit.Tests
{
    public class BaseTest
    {
        protected static IRestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient() =>
            _client = new RestClient("https://api.trello.com");

        protected IRestRequest RequestWithAuth(string resource) =>
            RequestWithoutAuth(resource)
                .AddQueryParameter("key", "9e9c82c4e107a4b86b9bb50c9ac025ed")
                .AddQueryParameter("token", "fb5f62f40da916ee2d60bbd1ea40f4d4fbbd6437bc49038f6f4088626096bf33");

        protected IRestRequest RequestWithoutAuth(string resource) =>
            new RestRequest(resource);
    }
}
