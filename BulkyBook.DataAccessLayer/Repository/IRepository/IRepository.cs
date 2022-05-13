using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccessLayer.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string includeTheseNavigationProperties = null);
        void AddItem(T item);
        T GetItemByExpression(Expression<Func<T, bool>> filter);
        void RemoveItem(T item);
        void RemoveItemsInRange(IEnumerable<T> items);
    }
}
