using Microsoft.AspNetCore.Mvc;
using seminar1._1.Dto;
using seminar1._1.Models;

namespace seminar1._1.Abstraction
{
    public interface IProductRepository
    {
        IEnumerable<ProductDto> GetAllProducts();
        int AddProduct(ProductDto productDto);
        void DeleteProduct(int id);
    }
}
