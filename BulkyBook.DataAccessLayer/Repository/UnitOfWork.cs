using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public CategoryRepository CategoryRepository { get; private set; }
        public CoverTypeRepository CoverTypeRepository { get; private set; }
        public ProductRepository ProductRepository { get; private set; }

        private readonly ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            CategoryRepository = new CategoryRepository(db);
            CoverTypeRepository = new CoverTypeRepository(db);
            ProductRepository = new ProductRepository(db);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
