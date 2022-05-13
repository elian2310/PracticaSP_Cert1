using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Logic.Managers;
using Logic.Entities;

namespace Practica2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductManager _productMgr;
        public ProductController(ProductManager productManager)
        {
            _productMgr = productManager;
        }
        [HttpGet]
        
        public IActionResult GetProducts()
        {
            return Ok(_productMgr.GetProducts());
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody]Practica2.Models.Product product)
        {
            Product createdProduct = _productMgr.CreateProduct(product.Name, product.Type.ToUpper(), product.Stock);
            return Ok(createdProduct);
        }
        [HttpPut]
        public IActionResult UpdateProduct([FromHeader]string codeToFind,[FromBody]Practica2.Models.Product product)
        {
            int res = _productMgr.UpdateProuct(product.Name, product.Stock, codeToFind);
            return Ok(res);
        }
        [HttpDelete]
        public IActionResult DeleteProduct([FromHeader] string codeToFind)
        {
            int res = _productMgr.DeleteProduct(codeToFind);
            return Ok(res);
        }

        [HttpGet]
        [Route("/calculate-prices")]
        public IActionResult CalculatePrices()
        {
            return Ok(_productMgr.CalculatePrices());
        }
    }
}