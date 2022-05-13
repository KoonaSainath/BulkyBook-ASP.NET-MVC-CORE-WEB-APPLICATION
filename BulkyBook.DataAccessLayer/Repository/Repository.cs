using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        private DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }
        public void AddItem(T item)
        {
            dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(string includeTheseNavigationProperties = null)
        {
            IQueryable<T> query = dbSet.AsQueryable<T>();
            if(includeTheseNavigationProperties != null)
            {
                //"Category,CoverType"
                foreach (string navigationProperty in includeTheseNavigationProperties.Split(",",StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(navigationProperty);
                }
            }
            return query;
        }

        public T GetItemByExpression(Expression<Func<T, bool>> filter)
        {
            T item = dbSet.Where(filter).FirstOrDefault();
            return item;
        }

        public void RemoveItem(T item)
        {
            dbSet.Remove(item);
        }

        public void RemoveItemsInRange(IEnumerable<T> items)
        {
            dbSet.RemoveRange(items);
        }
    }
}
