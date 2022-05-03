using BulkyBookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccessLayer.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void UpdateCategory(Category category);

    }
}
