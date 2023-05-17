using CatsWebApi.DTOs;
using CatsWebApi.Models;
using System.Net;

namespace CatsWebApi.Services
{
    public interface ICatDBService
    {
        Task<IEnumerable<Cat>> getCatsAsync(string query);
        Task<Cat> AddAsync(CreateCatDTO newCat);
        Task<Cat> UpdateAsync(Cat catToUpdate);
        Task<HttpStatusCode> DeleteAsync(string id, string key);
        //Task<Cat> getCatAsyn(string id);
    }
}
