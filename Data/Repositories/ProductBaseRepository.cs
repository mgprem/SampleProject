using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessEntities;
using Common;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductBaseRepository<T> : IProductBaseRepository<T> where T : IdObject
    {
        private readonly Func<T, Guid> _getId;
        private readonly object _gate = new object();
        private readonly List<T> _documentSession;

        public ProductBaseRepository(List<T>  documentSession,Func<T, Guid> getId)
        {
            _documentSession = documentSession;
            _getId = getId;

        }

        public void Save(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            lock (_gate)
            {
                var id = _getId(entity);
                _documentSession.Add(entity); 
            }
        }
        public virtual bool Delete(Guid id)
        {
            lock (_gate)
            {
                var idx = _documentSession.FindIndex(prod => _getId(prod) == id);
                if (idx < 0) return false;
                _documentSession.RemoveAt(idx);
                return true;
            }
        }

        public T Get(Guid id)
        {
           return _documentSession.FirstOrDefault(prod=> _getId(prod) ==id);
             
        }
        public virtual IEnumerable<T> GetAllInMemory()
        {
            lock (_gate)
            {
                // return a copy so callers can’t mutate our store directly
                return _documentSession.ToList();
            }
        }

        protected void DeleteAllInMemory()
        {
            lock (_gate) 
            { 
                _documentSession.Clear(); 
            }
        }
        bool IProductBaseRepository<T>.Delete(T entity)
        {
            lock (_gate)
            {
                var idx = _documentSession.FindIndex(prod => _getId(prod) == entity.Id);
                if (idx < 0) return false;
                _documentSession.RemoveAt(idx);
                return true;
            }
        }

        public bool Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            lock (_gate)
            {
                var id = _getId(entity);
                var idx = _documentSession.FindIndex(prod => _getId(prod) == id);
                if (idx < 0) return false;
                _documentSession[idx] = entity;
                return true;
            }
        }
    }
}