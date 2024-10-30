using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var obj = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (obj is not null)
            {
                obj.ISBN = product.ISBN;
                obj.ListPrice = product.ListPrice;
                obj.Price = product.Price;
                obj.Price50 = product.Price50;
                obj.Title = product.Title;
                obj.Description = product.Description;
                obj.Price100 = product.Price100;
                obj.Author = product.Author;
                obj.CategoryId = product.CategoryId;
                if (product.ImageUrl is not null)
                {
                    obj.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
