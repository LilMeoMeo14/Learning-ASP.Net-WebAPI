using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using WebAPICode.Models;

namespace WebAPICode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }


        // get product by id 
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = products.SingleOrDefault(_product => _product.Id == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();// 404 not found
                }
                return Ok(product);
            }
            catch (Exception ex) { 
                // loi khong lay duoc id
                return BadRequest(ex.Message);
            }
            // LINQ (object) query 
            
        }
         
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };
            products.Add(newProduct);
            return Ok( new {
                Success = true,Data = newProduct
            });
        }


        // Edit 
        [HttpPut("{id}")]
        public IActionResult Edit(string id , Product product)
        {
            try
            {
                var newproduct = products.SingleOrDefault(_product => _product.Id == Guid.Parse(id));
                if (newproduct == null)
                {
                    return NotFound();
                }
                if (product.Id.ToString() != id)
                {
                    return BadRequest();
                }

                // update
                newproduct.Name = product.Name;
                newproduct.Price = product.Price;
                newproduct.Description = product.Description;
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }


        //Delete 
        [HttpDelete("{id}")]
        public IActionResult Delete(string id , Product product)
        {
            try
            {
                var newProduct = products.SingleOrDefault(_product => _product.Id == Guid.Parse(id));
                if (newProduct == null)
                {
                    return NotFound();
                }
                
                // remove product 
                products.Remove(newProduct);
                return Ok();

            }catch(Exception ex) 
            {
                return BadRequest(ex.Message);    
            }
        }
    }
}
