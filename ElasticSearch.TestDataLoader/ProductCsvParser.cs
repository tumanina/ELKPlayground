using System.Globalization;

namespace ElasticSearch.TestDataLoader
{
    public static class ProductCsvParser
    {
        public static IEnumerable<Product> Parse(string csv)
        {
            var lines = csv.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',', StringSplitOptions.None);

                yield return new Product
                {
                    Id = Guid.NewGuid(),
                    Name = parts[1],
                    Description = parts[2],
                    Brand = parts[3],
                    Category = parts[4],
                    Price = decimal.Parse(parts[5], CultureInfo.InvariantCulture),
                    InStock = bool.Parse(parts[6]),
                    Rating = double.Parse(parts[7], CultureInfo.InvariantCulture),
                    Tags = parts[8]
                        .Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .ToList(),
                    CreatedAtUtc = DateTime.UtcNow
                };
            }
        }
    }

}
