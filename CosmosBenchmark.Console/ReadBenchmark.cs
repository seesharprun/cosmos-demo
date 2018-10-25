using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosBenchmark
{
    public class ReadBenchmark
    {
        public async Task StartBenchmarkAsync(DocumentClient client, ConnectionPolicy policy, CosmosSettings settings, CollectionSettings collectionSetting)
        {
            await client.OpenAsync();

            Database database = await EnsureDatabaseResourceAsync(client, settings.Database);
            await Console.Out.WriteLineAsync($"Database Validated:\t{database.SelfLink}");

            DocumentCollection collection = await EnsureCollectionResourceAsync(client, database, collectionSetting);
            await Console.Out.WriteLineAsync($"Collection Validated:\t{collection.SelfLink}");


            await Console.Out.WriteLineAsync("Summary:");
            await Console.Out.WriteLineAsync("--------------------------------------------------------------------- ");
            await Console.Out.WriteLineAsync($"Endpoint:\t\t{settings.EndpointUri}");
            await Console.Out.WriteLineAsync($"Database\t\t{settings.Database}");
            await Console.Out.WriteLineAsync($"Collection\t\t{collectionSetting.Id}");
            await Console.Out.WriteLineAsync($"Partition Key:\t\t{String.Join(", ", collectionSetting.PartitionKeys)}");
            await Console.Out.WriteLineAsync($"Throughput:\t\t{collectionSetting.Throughput} Request Units per Second (RU/s)");
            await Console.Out.WriteLineAsync($"Read Operation:\tEnd-to-end performance for a document read operation");
            await Console.Out.WriteLineAsync("--------------------------------------------------------------------- ");
            await Console.Out.WriteLineAsync();

            Document randomDocument = GetRandomDocument(client, settings, collectionSetting, collection);
            RequestOptions options = new RequestOptions
            {
                PopulateQuotaInfo = true
            };

            Stopwatch watch = new Stopwatch();
            watch.Start();

            ResourceResponse<Document> response = await client.ReadDocumentAsync(randomDocument.SelfLink);

            watch.Stop();
            
            await Console.Out.WriteLineAsync();
            await Console.Out.WriteLineAsync("Summary:");
            await Console.Out.WriteLineAsync("--------------------------------------------------------------------- ");
            await Console.Out.WriteLineAsync($"Total Time Elapsed:\t{watch.Elapsed}");
            await Console.Out.WriteLineAsync($"Request Units Used:\t{response.RequestCharge} RUs");
            await Console.Out.WriteLineAsync("--------------------------------------------------------------------- ");
            await Console.Out.WriteLineAsync();
            await Console.Out.WriteLineAsync();
            await Console.Out.WriteLineAsync();

        }

        private Document GetRandomDocument(DocumentClient client, CosmosSettings settings, CollectionSettings collectionSetting, DocumentCollection collection)
        {
            SqlQuerySpec query = new SqlQuerySpec
            {
                QueryText = "SELECT TOP 1 * FROM docs"
            };
            FeedOptions options = new FeedOptions
            {
                MaxDegreeOfParallelism = 0,
                MaxItemCount = 1,
                PartitionKey = new PartitionKey(collectionSetting.PartitionId)
            };
            return client.CreateDocumentQuery<Document>(collection.SelfLink, query, options).Single<Document>();
        }

        private async Task<Database> EnsureDatabaseResourceAsync(DocumentClient client, string databaseId)
        {
            Database database = new Database { Id = databaseId };
            database = await client.CreateDatabaseIfNotExistsAsync(database);

            return database;
        }

        private async Task<DocumentCollection> EnsureCollectionResourceAsync(DocumentClient client, Database database, CollectionSettings collectionSetting)
        {
            DocumentCollection collection = new DocumentCollection
            {
                Id = collectionSetting.Id,
                PartitionKey = new PartitionKeyDefinition
                {
                    Paths = new Collection<string>(collectionSetting.PartitionKeys)
                }
            };
            RequestOptions options = new RequestOptions
            {
                OfferThroughput = collectionSetting.Throughput
            };
            collection = await client.CreateDocumentCollectionIfNotExistsAsync(database.SelfLink, collection, options);

            return collection;
        }
    }
}