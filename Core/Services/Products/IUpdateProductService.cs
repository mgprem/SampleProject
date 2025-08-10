using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name,decimal? price );
    }
}