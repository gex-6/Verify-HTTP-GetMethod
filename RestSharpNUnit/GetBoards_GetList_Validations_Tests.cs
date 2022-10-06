namespace GetBoardGetListValidation
{
    public class Test
    {
        private static RestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient() =>
                    _client = new RestClient("https://api.trello.com");

        private RestRequest RequestWithAuth(string resource) =>
                    new RestRequest(resource)
                       .AddQueryParameter("key", "9e9c82c4e107a4b86b9bb50c9ac025ed")
                       .AddQueryParameter("token", "fb5f62f40da916ee2d60bbd1ea40f4d4fbbd6437bc49038f6f4088626096bf33");

        [Test]
        public void CheckGetBoardWithInvalidId()
        {
            var request = RequestWithAuth("/1/boards/{id}")
                .AddUrlSegment("id", "6136fb1dd053730149f685fb");
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content, Is.EqualTo("The requested resource was not found."));
        }

        [Test]
        public void CheckGetBoardWithInvalidAuth()
        {
            var request = new RestRequest("/1/boards/{id}")
                .AddQueryParameter("key", "2e9c82c4e107a4b86b9bb50c9ac025ed")
                .AddQueryParameter("token", "fb4f62f40da916ee2d60bbd1ea40f4d4fbbd6437bc49038f6f4088626096bf33")
                .AddUrlSegment("id", "6336fb1dd053730149f685fb");
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.Content, Is.EqualTo("invalid key"));
        }

        [Test]
        public void CheckGetBoardWithAnotherUserCredentials()
        {
            var request = new RestRequest("/1/boards/{id}")
                .AddQueryParameter("key", "8b32218e6887516d17c84253faf967b6")
                .AddQueryParameter("token", "492343b8106e7df3ebb7f01e219cbf32827c852a5f9e2b8f9ca296b1cc604955")
                .AddUrlSegment("id", "6336fb1dd053730149f685fb");
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.Content, Is.EqualTo("invalid token"));
        }

        [Test]
        public void CheckGetCardWithInvalidId()
        {
            var request = new RestRequest("/1/cards/{id}")
                .AddQueryParameter("key", "9e9c82c4e107a4b86b9bb50c9ac025ed")
                .AddQueryParameter("token", "fb5f62f40da916ee2d60bbd1ea40f4d4fbbd6437bc49038f6f4088626096bf33")
                .AddUrlSegment("id", "623ada019d5b38033e9eebe0");
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content, Is.EqualTo("The requested resource was not found."));
        }

        [Test]
        public void CheckGetCardWithInvalidAuth()
        {
            var request = new RestRequest("/1/cards/{id}")
                .AddUrlSegment("id", "633ada019d5b38033e9eebe0");
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.Content, Is.EqualTo("unauthorized card permission requested"));
        }
    }
}