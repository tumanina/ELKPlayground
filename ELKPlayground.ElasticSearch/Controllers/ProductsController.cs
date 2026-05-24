using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Mvc;

namespace ELKPlayground.ElasticSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(ElasticsearchClient client) : ControllerBase
    {
        private readonly ElasticsearchClient _client = client;

        [HttpGet(Name = "FindProducts")]
        public async Task<IEnumerable<Product>> Get(string query)
        {
            var response = await _client.SearchAsync<Product>(s => s
               .Query(q => q
                   .MultiMatch(m => m
                       .Query(query)
                       .Fields(new[]
                       {
                            Infer.Field<Product>(p => p.Name),
                            Infer.Field<Product>(p => p.Description)
                       }))));

            return response.Documents;
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task CreateProduct(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
            }

            var response = await _client.IndexAsync(product);
        }
    }
}
