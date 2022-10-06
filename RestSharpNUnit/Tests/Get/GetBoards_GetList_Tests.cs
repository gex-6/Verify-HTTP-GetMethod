namespace RestSharpNUnit.Tests.Get
{
    public class GetTests : BaseTest
    {
        #region GET tests

        [Test]
        public void CheckTrelloAPI()
        {
            var request = new RestRequest();
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckGetBoards()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("member", UrlParamValues.UserName);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(JToken.Parse(response.Content).SelectToken("name").ToString(), Is.EqualTo("Test"));

            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_specific_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetCardsInList()
        {
            var request = RequestWithAuth(CardsEndpoints.GetCardsInListUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards_in_list.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetCard()
        {
            var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(JToken.Parse(response.Content).SelectToken("name").ToString(), Is.EqualTo("Test card doing"));

            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        #endregion


    }
}