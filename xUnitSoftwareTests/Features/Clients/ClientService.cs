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
            _mediator.Publish(new ClientEmailNotification("admin@me.com", client.Email, "Hello", "Welcome!"));
        }


        public void Update(Client client)
        {
            if (!client.IsValid())
                return;

            _clientRepository.Update(client);
            _mediator.Publish(new ClientEmailNotification("admin@me.com", client.Email, "Changes", "Take a look!"));
        }


        public void Inactivate(Client client)
        {
            if (!client.IsValid())
                return;

            client.Inactivate();
            _clientRepository.Update(client);
            _mediator.Publish(new ClientEmailNotification("admin@me.com", client.Email, "See you soon", "Until Later!"));
        }

        public void Remove(Client client)
        {
            _clientRepository.Remove(client.Id);
            _mediator.Publish(new ClientEmailNotification("admin@me.com", client.Email, "GoodBye", "Have a good journey!"));
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }
    }
}
