using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccessLayer.Repository.IRepository
{
    public interface IUnitOfWork
    {
        CategoryRepository CategoryRepository { get; }
        void Save();
    }
}
