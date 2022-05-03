using BulkyBook.DataAccessLayer.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccessLayer.Repository
{
    public class CoverTypeRepository : ICoverTypeRepository
    {
        private readonly ApplicationDbContext db;
        public CoverTypeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void UpdateCoverType(CoverType coverType)
        {
            db.CoverTypes.Update(coverType);
        }
    }
}
