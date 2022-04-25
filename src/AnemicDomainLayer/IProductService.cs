using System.Collections.Generic;

namespace AnemicDomainLayer
{
    public interface IProductService
    {
        IEnumerable<Product> All();
    }
}