namespace Features.Clients
{
    public interface IClientService : IDisposable
    {
        IEnumerable<Client> GetAllActive();
        void Add(Client client);
        void Update(Client client);
        void Remove(Client cliente);
        void Inactivate(Client cliente);
    }
}
