using System.Text.Json;
using API.Data;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext context;
        public ProductsController(StoreContext context)
        {
            this.context = context;
            
        }
        [HttpGet]
     
    public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery] ProductParams productParams)
    {
        var query = context.Products
            .Sort(productParams.orderBy)
            .Search(productParams.searchTerm)
            .Filter(productParams.brands, productParams.types)
            .AsQueryable();

        var products =
            await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

        Response.AddPaginationHeader(products.Metadata);

        return products;
    }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }   
        
        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await context.Products.Select(p => p.Type).Distinct().ToListAsync();

            return Ok(new {brands, types});
        }
    }
}