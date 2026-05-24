
using Elastic.Clients.Elasticsearch;
using Scalar.AspNetCore;

namespace ELKPlayground.ElasticSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton(sp =>
            {
                var settings = new ElasticsearchClientSettings(
                    new Uri("http://elasticsearch:9200"))
                    .DefaultIndex("products");

                return new ElasticsearchClient(settings);
            });

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
