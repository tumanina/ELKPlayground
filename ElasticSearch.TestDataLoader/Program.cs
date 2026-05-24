using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ElasticSearch.TestDataLoader;

var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
    .Authentication(new BasicAuthentication("elastic", "123456test"))
    .DefaultIndex("products");

var client = new ElasticsearchClient(settings);

var csv = File.ReadAllText("Data/products.csv");
var products = ProductCsvParser.Parse(csv).ToList();

var response = await client.BulkAsync(b => b
        .Index("products")
        .IndexMany(products));

if (response.Errors)
{
    foreach (var item in response.ItemsWithErrors)
    {
        Console.WriteLine($"Error: {item.Error?.Reason}");
    }
}
else
{
    Console.WriteLine("Done");
}