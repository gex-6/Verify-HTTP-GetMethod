namespace GetBoardGetList
{
    public class TrelloTest
    {
        private static RestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient() 
        {
            _client = new RestClient("https://api.trello.com");
        }

        private RestRequest RequestWithAuth(string resource)
        {
            return new RestRequest(resource)
                       .AddQueryParameter("key", "fb04999a731923c2e3137153b1ad5de0")
                       .AddQueryParameter("token", "b73120fb537fceb444050a2a4c08e2f96f47389931bd80253d2440708f2a57e1");
        }

        #region GET tests

        [Test]
        public void CheckTrelloAPI()
        {
            var request = new RestRequest();    
            var response = _client.Get(request);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
        }

        [Test]
        public void CheckGetBoards()
        {
            var request = RequestWithAuth("/1/members/{member}/boards")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("member", "taras_stepaniuk");
            var response = _client.Get(request);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));

            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = new RestRequest("/1/boards/{id}")
                .AddQueryParameter("key", "9e9c82c4e107a4b86b9bb50c9ac025ed")
                .AddQueryParameter("token", "fb5f62f40da916ee2d60bbd1ea40f4d4fbbd6437bc49038f6f4088626096bf33")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", "6336fb1dd053730149f685fb");
            var response = _client.Get(request);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That("Test", Is.EqualTo(JToken.Parse(response.Content).SelectToken("name").ToString()));

            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_specific_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetCardsInList()
        {
            var request = new RestRequest("/1/lists/{list_id}/cards")
                .AddQueryParameter("key", "9e9c82c4e107a4b86b9bb50c9ac025ed")
                .AddQueryParameter("token", "fb5f62f40da916ee2d60bbd1ea40f4d4fbbd6437bc49038f6f4088626096bf33")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("list_id", "6336fb2a08a14904075436a1");
            var response = _client.Get(request);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards_in_list.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetCard()
        {
            var request = new RestRequest("/1/cards/{id}")
                .AddQueryParameter("key", "9e9c82c4e107a4b86b9bb50c9ac025ed")
                .AddQueryParameter("token", "fb5f62f40da916ee2d60bbd1ea40f4d4fbbd6437bc49038f6f4088626096bf33")
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", "633ad57e0f4a10013e851b33");
            var response = _client.Get(request);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
            Assert.That("Test card doing", Is.EqualTo(JToken.Parse(response.Content).SelectToken("name").ToString()));

            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        #endregion


    }
}