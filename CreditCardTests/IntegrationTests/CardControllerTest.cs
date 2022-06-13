using CardAPI;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using Card.Domain.Model;
using NUnit.Framework;
using CardAPI.Dto;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Card.DataAccess.Configuration;

namespace CardManagerTests.IntegrationTests
{
    [TestFixture]
    public class CardControllerTests
    {
        private readonly HttpClient _client;
        private readonly CardDbContext _context;
        public CardControllerTests()
        {

            TestServer server = new TestServer(WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>());
            _context = server.Host.Services.GetService(typeof(CardDbContext)) as CardDbContext;

            GetCardMockData();


            _client = server.CreateClient();
        }

        [Test]
        public async Task Integration_CardController_GetCards_Ok()
        {
            var response = await _client.GetAsync("/api/v1/card/cards");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.IsNotEmpty(responseContent);

            List<CardDto> userList = JsonConvert.DeserializeObject<List<CardDto>>(responseContent);

            Assert.NotNull(userList);

            Assert.IsTrue(userList.Count > 0);
        }

        [Test]
        public async Task Integration_CardController_AddCards_EmptyName()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12345";
            cardDto.Limit = 300;
            cardDto.Name = "";

            var requestContent = new StringContent(JsonConvert.SerializeObject(cardDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/card/addcard", requestContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
            Assert.IsTrue(responseContent.Contains("The Name field is required."));
        }

        [Test]
        public async Task Integration_CardController_AddCards_NegativeLimit()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12345";
            cardDto.Name = "TestName";
            cardDto.Limit = -300;

            var requestContent = new StringContent(JsonConvert.SerializeObject(cardDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/card/addcard", requestContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
            Assert.IsTrue(responseContent.Contains("The field Limit must be between 0 and 100000000."));
        }

        [Test]
        public async Task Integration_CardController_AddCards_EmptyCardNumber()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.Name = "TestName";
            cardDto.Limit = 300;
            cardDto.CardNumber = "";

            var requestContent = new StringContent(JsonConvert.SerializeObject(cardDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/card/addcard", requestContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
            Assert.IsTrue(responseContent.Contains("The CardNumber field is required."));
        }

        [Test]
        public async Task Integration_CardController_AddCards_NumberExceedingLength()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12312345678909789022334567878945";
            cardDto.Name = "TestName";
            cardDto.Limit = 5000;

            var requestContent = new StringContent(JsonConvert.SerializeObject(cardDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/card/addcard", requestContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
            Assert.IsTrue(responseContent.Contains("The field CardNumber must be a string or array type with a maximum length of '19'"));
        }

        [Test]
        public async Task Integration_CardController_AddCards_InvalidLuhnCheckNumber()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "12345";
            cardDto.Name = "TestName";
            cardDto.Limit = 5000;

            var requestContent = new StringContent(JsonConvert.SerializeObject(cardDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/card/addcard", requestContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
            Assert.IsTrue(responseContent.Contains("The card number is not valid."));
        }

        [Test]
        public async Task Integration_CardController_AddCards_InvalidCardNumber()
        {
            AddCardDto cardDto = new AddCardDto();
            cardDto.CardNumber = "123ad45";
            cardDto.Name = "TestName";
            cardDto.Limit = 5000;

            var requestContent = new StringContent(JsonConvert.SerializeObject(cardDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/card/addcard", requestContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
            Assert.IsTrue(responseContent.Contains("The card number contains invalid characters."));
        }

        [Test]
        public async Task Integration_CardController_AddCards_Ok()
        {
            AddCardDto addCardDto = new AddCardDto();
            addCardDto.CardNumber = "347971545665469";
            addCardDto.Name = "TestName";
            addCardDto.Limit = 5000;

            var requestContent = new StringContent(JsonConvert.SerializeObject(addCardDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/card/addcard", requestContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.IsNotEmpty(responseContent);

            CardDto card = JsonConvert.DeserializeObject<CardDto>(responseContent);

            Assert.NotNull(card);
            Assert.AreEqual(addCardDto.CardNumber, card.CardNumber);
        }

        private void GetCardMockData()
        {
            List<CreditCard> cards = new List<CreditCard>
            {
                new CreditCard("TestName1", "3532823997700931", 3000),
                new CreditCard("TestName2", "5283210350299709", 400),
                new CreditCard( "TestName3", "6225842860109074", 7800),
                new CreditCard( "TestName4", "501864693924960085", 2300)
            };

            foreach(CreditCard card in cards)
            {
                _context.Add(card);
            }

            _context.SaveChanges();

        }

    }


}
