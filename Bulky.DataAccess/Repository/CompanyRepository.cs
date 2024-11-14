using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Company company)
        {
            var obj = _db.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (obj is not null)
            {
                obj.Name = company.Name;
                obj.StreetAddress = company.StreetAddress;
                obj.State = company.State;
                obj.City = company.City;
                obj.PostalCode = company.PostalCode;
                obj.PhoneNumber = company.PhoneNumber;
            }
        }
    }
}
