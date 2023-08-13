﻿using Features.Clients;
using Xunit;

namespace Features.Tests._02___Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClientTestsFixture>
    { }
    public class ClientTestsFixture : IDisposable
    {

        public Client GenerateValidClient()
        {
            var client = new Client(
           Guid.NewGuid(),
           "Michael",
           "Peter",
           DateTime.Now.AddYears(-30),
           "michael@edu.com",
           true,
           DateTime.Now);

            return client;
        }

        public Client GenerateInvalidClient()
        {
            var client  = new Client(
             Guid.NewGuid(),
             "",
             "",
             DateTime.Now,
             "michael@edu.com",
             true,
             DateTime.Now);

            return client;
        }

        public void Dispose()
        {

        }
    }
}
