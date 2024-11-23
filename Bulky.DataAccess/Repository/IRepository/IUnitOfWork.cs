using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
       IOrderDetailRepository OrderDetail { get;  }
       IOrderHeaderRepository OrderHeader { get;  }

        void Save();
    }
}
