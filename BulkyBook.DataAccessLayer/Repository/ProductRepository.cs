using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;

namespace BulkyBook.DataAccessLayer.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        public void UpSert(Product product)
        {
            Product productInDb = db.Products.Find(product.Id);
            if (productInDb != null)
            {
                productInDb.Title = product.Title;
                productInDb.Description = product.Description;
                productInDb.ISBN = product.ISBN;
                productInDb.Author = product.Author;
                productInDb.ListPrice = product.ListPrice;
                productInDb.Price = product.Price;
                productInDb.Price50 = product.Price50;
                productInDb.Price100 = product.Price100;
                productInDb.CategoryId = product.CategoryId;
                productInDb.CoverTypeId = product.CoverTypeId;

                if (product.ImageUrl != null)
                {
                    productInDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
