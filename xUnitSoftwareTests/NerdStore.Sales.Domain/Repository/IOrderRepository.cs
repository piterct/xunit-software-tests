using NerdStore.Core.Data;

namespace NerdStore.Sales.Domain.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Add(Order order);
    }
}
