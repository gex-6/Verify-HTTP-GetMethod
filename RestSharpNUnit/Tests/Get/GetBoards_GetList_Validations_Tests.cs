namespace RestSharpNUnit.Tests.Get
{
    public class GetValidationTests : BaseTest
    {
        #region GET Validation Tests

        [Test]
        public void CheckGetBoardWithInvalidId()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddUrlSegment("id", "invalid");
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo("invalid id"));
        }

        [Test]
        public void CheckGetBoardWithInvalidAuth()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.Content, Is.EqualTo("unauthorized permission requested"));
        }

        [Test]
        public void CheckGetBoardWithAnotherUserCredentials()
        {
            var request = new RestRequest(BoardsEndpoints.GetBoardUrl)
                .AddQueryParameter("key", UrlParamValues.AnotherUserKey)
                .AddQueryParameter("token", UrlParamValues.AnotherUserToken)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.Content, Is.EqualTo("invalid token"));
        }

        [Test]
        public void CheckGetCardWithInvalidId()
        {
            var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
                .AddUrlSegment("id", "631ada019d5b38033e9eebe0");
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content, Is.EqualTo("The requested resource was not found."));
        }

        [Test]
        public void CheckGetCardWithInvalidAuth()
        {
            var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.Content, Is.EqualTo("unauthorized card permission requested"));
        }

        [Test]
        public void CheckGetCarddWithAnotherUserCredentials()
        {
            var request = new RestRequest(CardsEndpoints.GetCardUrl)
                .AddQueryParameter("key", UrlParamValues.AnotherUserKey)
                .AddQueryParameter("token", UrlParamValues.AnotherUserToken)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.Content, Is.EqualTo("invalid token"));
        }

        #endregion
    }
}