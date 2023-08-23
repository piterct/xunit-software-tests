using MediatR;

namespace Features.Clients
{
    public class ClientEmailNotification : INotification
    {
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public string Subject { get; private set; }
        public string Message { get; private set; }
        public ClientEmailNotification(string origin, string destination, string subject, string message)
        {
            Origin = origin;
            Destination = destination;
            Subject = subject;
            Message = message;
        }
    }
}
