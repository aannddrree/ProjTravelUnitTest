using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using ProjTravelUnitTest.Api;
using ProjTravelUnitTest.Api.Controllers;
using ProjTravelUnitTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace ProjTravelUnitTest.Tests
{
    public class ClientUnitTest
    {
        public HttpClient _client { get; }

        public ClientUnitTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            _client.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async void GetAllClients()
        {
            var response = await _client.GetAsync("/api/Client");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetCountClients()
        {
            var response = await _client.GetAsync("/api/Client");
            var clients = JsonConvert.DeserializeObject<Client[]>(await response.Content.ReadAsStringAsync());
            Assert.True(clients.Length >= 1);
        }

        [Fact]
        public async void PostClient()
        {

            Client client = new Client()
            {
                BirthdayDate = DateTime.Now,
                Name = "José Silva",
                NumberOfChildren = 2,
                Telephone = "16 99999 2222"
            };

            var jsonContent = JsonConvert.SerializeObject(client);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Client", contentString);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async void PutClient()
        {
            int id = 3;
            Client client = new Client()
            {
                Id = 3,
                BirthdayDate = DateTime.Now,
                Name = "José Silva Alterado",
                NumberOfChildren = 1,
                Telephone = "16 99999 2222"
            };

            var jsonContent = JsonConvert.SerializeObject(client);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/Client/" + id, contentString);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void DeleteClient()
        {
            int id = 3;
            var response = await _client.DeleteAsync("/api/Client/" + id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
