using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
        public interface IOrderBaseRepository<T> where T : IdObject
        {
            void Save(T entity);
            bool Update(T entity);
            bool Delete(T entity);
            T Get(Guid id);
        }
}
