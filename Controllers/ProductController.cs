using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using seminar1._1.Abstraction;
using seminar1._1.Data;
using seminar1._1.Dto;
using seminar1._1.Models;
using System.Text;

namespace seminar1._1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private object memoryChache;
        private readonly StorageContext _storageContext;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpPost]
        public ActionResult<int> AddProduct(ProductDto productDto)
        {
            try
            {
                var id = _productRepository.AddProduct(productDto);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(409);
            }



        }
        [HttpGet("get_all_product")]
        public ActionResult<IEnumerable<Product>> GetAllProducts(string name, string description, double price)
        {
            return Ok(_productRepository.GetAllProducts());
        }

        [HttpDelete("groups/{id}")]
        public ActionResult DeleteGroup(int id)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var group = storageContext.ProductGroups.Find(id);
                if (group == null)
                    return NotFound();

                storageContext.ProductGroups.Remove(group);
                storageContext.SaveChanges();
                return NoContent();
            }
        }

        [HttpDelete("products/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var product = storageContext.Products.Find(id);
                if (product == null)
                    return NotFound();

                storageContext.Products.Remove(product);
                storageContext.SaveChanges();
                return NoContent();
            }
        }

        [HttpPut("products/price")]
        public ActionResult UpdatePrice(Product model)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                var product = storageContext.Products.Find(model.Id);
                if (product == null)
                    return NotFound();

                product.Price = model.Price;
                storageContext.SaveChanges();
                return NoContent();
            }
        }

        private string GetCsv(IEnumerable<ProductGroup> products)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var product in products)
            {
                sb.AppendLine(product.Id + product.Name + ";" + product.Description + ";" + "\n");
            }
            return sb.ToString();
        }

        public ActionResult<string> GetProductsCsvUrl()
        {
            var content = "";


            using (_storageContext)
            {
                var products = _storageContext.Procucts.Select(b => new ProductGroup { Id = b.Id, Description = b.Description, Name = b.Name }).ToList();

                content = GetCsv(products);
            }

            string? fileName = null;
            fileName = "products" + DateTime.Now.ToBinary().ToString() + ".csv";
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), content);

            return "https://" + Request.Host.ToString() + "/static/" + fileName;
        }

    }
}
