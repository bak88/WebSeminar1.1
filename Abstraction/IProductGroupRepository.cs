using seminar1._1.Dto;
using seminar1._1.Models;

namespace seminar1._1.Abstraction
{
    public interface IProductGroupRepository
    {
        IEnumerable<ProductGroupDto> GetAllProductsGroup();
        int AddProductGroup(ProductGroupDto productGroupDto);
        void DeleteProductGroup(int id);
    }
}
