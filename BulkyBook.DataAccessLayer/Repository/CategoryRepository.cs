using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccessLayer.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void UpdateCategory(Category category)
        {
            db.Categories.Update(category);
        }
    }
}
