using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = new List<T>(data);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            //return await Data.ToArray();
            return Task.FromResult((IEnumerable<T>)Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task Create(T t)
        {
            Data.Add(t);
            return Task.CompletedTask;
        }

        public Task Delete(T t)
        {
            Data.Remove(t);
            return Task.CompletedTask;
        }
    }
}