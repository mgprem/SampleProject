using BusinessEntities;

namespace Core.Services.Users
{
    public interface IDeleteOrderService
    {
        void Delete(Order order);
        void DeleteAll();
    }
}