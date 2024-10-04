using Microsoft.AspNetCore.Mvc;
using seminar1._1.Data;
using seminar1._1.Models;

namespace seminar1._1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> AddProduct(string name, string description, double price)
        {
            using (StorageContext storageContext = new StorageContext())
            {
                if (storageContext.Products.Any(p => p.Name == name))
                    return StatusCode(409);

                var product = new Product() { Name = name, Description = description, Price = price };
                storageContext.Products.Add(product);
                storageContext.SaveChanges();
                return Ok(product.Id);
            }
        }
        [HttpGet("get_all_product")]
        public ActionResult<IEnumerable<Product>> GetAllProducts(string name, string description, double price)
        {
            IEnumerable<Product> list;
            using (StorageContext storageContext = new StorageContext())
            {
                list = storageContext.Products.ToList();
                return Ok(list);
            }
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
    }
}
