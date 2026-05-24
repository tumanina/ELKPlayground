namespace ElasticSearch.TestDataLoader
{
    public sealed class Product
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Brand { get; init; }

        public string Category { get; init; }

        public decimal Price { get; init; }

        public bool InStock { get; init; }

        public double Rating { get; init; }

        public List<string> Tags { get; init; } = [];

        public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    }
}
