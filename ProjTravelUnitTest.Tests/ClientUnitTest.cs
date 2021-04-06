using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProjTravelUnitTest.Api.Controllers;
using ProjTravelUnitTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjTravelUnitTest.Tests
{
    public class ClientUnitTest
    {
        private DbContextOptions<TravelContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<TravelContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new TravelContext(options))
            {
                context.Client.Add(new Client { Id = 1, Name = "Name 1", NumberOfChildren = 1, Telephone = "16 99999 8888", BirthdayDate = DateTime.Now });
                context.Client.Add(new Client { Id = 2, Name = "Name 1", NumberOfChildren = 2, Telephone = "16 99999 6666", BirthdayDate = DateTime.Now });
                context.Client.Add(new Client { Id = 3, Name = "Name 1", NumberOfChildren = 3, Telephone = "16 99999 8877", BirthdayDate = DateTime.Now });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new TravelContext(options))
            {
                ClientController clientController = new ClientController(context);
                IEnumerable<Client> clients = clientController.GetClient().Result.Value;
    
                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new TravelContext(options))
            {
                int clientId = 2;
                ClientController clientController = new ClientController(context);
                Client client = clientController.GetClient(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Client client = new Client()
            {
                Id = 4,
                Name = "José Silva",
                Telephone = "16 98888 7777",
                NumberOfChildren = 2,
                BirthdayDate = DateTime.Now
            };

            // Use a clean instance of the context to run the test
            using (var context = new TravelContext(options))
            {
                ClientController clientController = new ClientController(context);
                Client cli = clientController.PostClient(client).Result.Value;
                Assert.Equal(4, cli.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Client client = new Client()
            {
                Id = 3,
                Name = "José Silva",
                Telephone = "16 98888 7777",
                NumberOfChildren = 2,
                BirthdayDate = DateTime.Now
            };

            // Use a clean instance of the context to run the test
            using (var context = new TravelContext(options))
            {
                ClientController clientController = new ClientController(context);
                Client cli = clientController.PutClient(3, client).Result.Value;
                Assert.Equal("José Silva", cli.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new TravelContext(options))
            {
                ClientController clientController = new ClientController(context);
                Client client = clientController.DeleteClient(2).Result.Value;
                Assert.Null(client);
            }
        }
    }
}
