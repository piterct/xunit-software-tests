using NerdStore.Core.Data;

namespace NerdStore.Sales.Domain.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Add(Order order);
        void Update(Order order);
        Task<Order> GetDraftOrderByClientId(Guid clientId);
       
    }
}
