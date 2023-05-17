using CatsWebApi.DTOs;
using CatsWebApi.Models;
using CatsWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ICatDBService cosmosService;

        public CatsController(ICatDBService cosmosService)
        {
            this.cosmosService = cosmosService;
        }


        [HttpGet]
        public async Task<IEnumerable<Cat>> Get()
        {
            string query = "SELECT * FROM c\r\nORDER BY c._ts DESC";
            return await cosmosService.getCatsAsync(query);
        }

        [HttpPost]
        public async Task<Cat> Create(CreateCatDTO newCat)
        {
            Cat addedCat = await cosmosService.AddAsync(newCat);
            return addedCat;
        }

        [HttpPut]
        public async Task<Cat> Update(Cat catToUpdate)
        {
            Cat updatedCat = await cosmosService.UpdateAsync(catToUpdate);
            return updatedCat;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id, string name)
        {
            var statusCode = await cosmosService.DeleteAsync(id, name);
            if (statusCode == System.Net.HttpStatusCode.NoContent)
                return Ok(new { message = $"Cat with id: {id} deleted successfully" });
            else return BadRequest(statusCode);
        }
    }
}
