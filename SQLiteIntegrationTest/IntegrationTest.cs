using Microsoft.AspNetCore.Mvc.Testing;
using SQLiteApplication.DTO;
using SQLiteApplication.Entity;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace SQLiteIntegrationTest
{
    public class Tests
    {
        private readonly WebApplicationFactory<Program> applicationInstance;
        private HttpClient Client;

        public Tests()
        {
            applicationInstance = new ApplicationInstance<Program>();
        }
        

        [SetUp]
        public void Setup()
        {
            Client = applicationInstance.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown1()
        {
            applicationInstance.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            Client.Dispose();            
        }

        [Test, Order(1)]
        public async Task InsertProduct()
        {
            try
            {
                var url = "product";

                var productDTO = new Product("Iphone", 1000);
                var jsonContent = JsonContent.Create(productDTO);
                var request = await Client.PostAsync(url, jsonContent);
                var result = await request.Content.ReadAsStringAsync();
                var userList = JsonSerializer.Deserialize<ProductDTO>(result);

                Assert.NotNull(userList);
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [Test, Order(2)]
        public async Task GetProduct()
        {
            var url = "product";

            var request = await Client.GetAsync(url);
            var result = await request.Content.ReadAsStringAsync();
            var userList = JsonSerializer.Deserialize<List<ProductDTO>>(result);

            Assert.IsNotNull(userList);
            Assert.GreaterOrEqual(userList.Count, 1);

        }
    }
}