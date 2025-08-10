using BusinessEntities;

namespace Core.Services.Users
{
    public interface IDeleteProductService
    {
        void Delete(Product product);
        void DeleteAll();
    }
}