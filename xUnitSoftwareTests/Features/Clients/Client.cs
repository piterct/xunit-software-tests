using Features.Core;

namespace Features.Clients
{
    public class Client : Entity
    {
        public string Name { get; private set; }
        public string LasName { get; private set; }
        public DateTime BornDate { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }
    }
}
