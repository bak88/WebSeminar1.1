using AutoMapper;
using seminar1._1.Abstraction;
using seminar1._1.Data;
using seminar1._1.Dto;
using seminar1._1.Models;

namespace seminar1._1.Repository
{
    public class ProductRepository : IProductRepository
    {
        StorageContext storageContext = new StorageContext();
        private readonly IMapper _mapper;
        public ProductRepository(StorageContext storageContext, IMapper mapper)
        {
            this.storageContext = storageContext;
            this._mapper = mapper;
        }
        public int AddProduct(ProductDto productDto)
        {
            if (storageContext.Products.Any(p => p.Name == productDto.Name))
                throw new Exception("Уже есть продукт с таким именем");

            var entity = _mapper.Map<Product>(productDto);
            storageContext.Products.Add(entity);
            storageContext.SaveChanges();
            return entity.Id;
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {            
            var listDto = storageContext.Products.Select(_mapper.Map<ProductDto>).ToList();
            return listDto;
        }
    }
}
