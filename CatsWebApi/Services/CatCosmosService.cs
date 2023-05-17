using CatsWebApi.DTOs;
using CatsWebApi.Models;
using Microsoft.Azure.Cosmos;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CatsWebApi.Services
{
    public class CatCosmosService : ICatDBService
    {
        public readonly Container container;
        public CatCosmosService(CosmosClient client, string databaseId, string containerId) 
        {
            container = client.GetContainer(databaseId, containerId);
        }

        public async Task<Cat> AddAsync(CreateCatDTO newCat)
        {
            Cat addedCat = new Cat()
            {
                Id = Guid.NewGuid().ToString(),
                Name = newCat.Name,
                Breed = newCat.Breed
            };
            ItemResponse<Cat> response = await container.CreateItemAsync<Cat>(addedCat, new PartitionKey(newCat.Name));
            return response.Resource;
        }

        public async Task<HttpStatusCode> DeleteAsync(string id, string key)
        {
            var deletedCat = await container.DeleteItemAsync<Cat>(id, new PartitionKey(key));
            return deletedCat.StatusCode;
        }

        //public async Task<Cat> getCatAsyn(string id, string key)
        //{
        //    await container.GetIte

        //}

        public async Task<IEnumerable<Cat>> getCatsAsync(string query)
        {
            QueryDefinition queryDefinition = new (query);
            FeedIterator<Cat> feedIterator = container.GetItemQueryIterator<Cat>(queryDefinition);

            List<Cat> cats = new List<Cat>();
            while(feedIterator.HasMoreResults)
            {
                FeedResponse<Cat> resp = await feedIterator.ReadNextAsync();
                foreach(var cat in resp)
                {
                    cats.Add(cat);
                }
            }
            return cats;
        }

        
        public async Task<Cat> UpdateAsync(Cat catToUpdate)
        {
           var updatedCat =  await container.UpsertItemAsync(catToUpdate, new PartitionKey(catToUpdate.Name));
            return updatedCat.Resource;
        }
    }
}
