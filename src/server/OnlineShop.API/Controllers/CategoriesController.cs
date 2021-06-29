using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> GetAll(int? pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Category, bool>> filter = x => x.Name.Contains("");
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = q => q.OrderBy(c => c.Name);

            var result = await _categoryService.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return Ok(result);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
