using MediatR;

namespace Features.Clients
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMediator _mediator;

        public ClientService(IClientRepository clientRepository, IMediator mediator)
        {
            _clientRepository = clientRepository;
            _mediator = mediator;
        }

        public IEnumerable<Client> GetAllActive()
        {
            return _clientRepository.GetAll().Where(x => x.Active);
        }

        public void Add(Client client)
        {
            if (!client.IsValid())
                return;

            _clientRepository.Add(client);
            _mediator.Publish(new ClienteEmailNotification("admin@me.com", client.Email, "Hello", "Welcome!"));
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }

        

        public void Inactivate(Client cliente)
        {
            throw new NotImplementedException();
        }

        public void Remove(Client cliente)
        {
            throw new NotImplementedException();
        }

        public void Update(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
