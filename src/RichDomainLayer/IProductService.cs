using System.Collections.Generic;

namespace RichDomainLayer
{
    public interface IProductService
    {
        IEnumerable<Product> All();
    }
}
