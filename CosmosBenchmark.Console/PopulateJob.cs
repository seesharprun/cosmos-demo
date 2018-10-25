using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosBenchmark
{
    public class PopulateJob
    {
        public async Task StartAsync(DocumentClient client, ConnectionPolicy policy, CosmosSettings settings, CollectionSettings collectionSetting)
        {
            await client.OpenAsync();
            WriteBenchmark supplementalBenchmark = new WriteBenchmark();

            Database database = await supplementalBenchmark.EnsureDatabaseResourceAsync(client, settings.Database);
            await Console.Out.WriteLineAsync($"Database Validated:\t{database.SelfLink}");

            DocumentCollection collection = await supplementalBenchmark.EnsureCollectionResourceAsync(client, database, collectionSetting);
            await Console.Out.WriteLineAsync($"Collection Validated:\t{collection.SelfLink}");

            int taskCount = settings.DegreeOfParallelism;
            if (settings.DegreeOfParallelism == -1)
            {
                // set TaskCount = 1 for each 1k RUs, minimum 1, maximum 250
                taskCount = Math.Max(collectionSetting.Throughput / 100, 1);
                taskCount = Math.Min(taskCount, 250);
            }

            await Console.Out.WriteLineAsync("Summary:");
            await Console.Out.WriteLineAsync("--------------------------------------------------------------------- ");
            await Console.Out.WriteLineAsync($"Endpoint:\t\t{settings.EndpointUri}");
            await Console.Out.WriteLineAsync($"Database\t\t{settings.Database}");
            await Console.Out.WriteLineAsync($"Collection\t\t{collectionSetting.Id}");
            await Console.Out.WriteLineAsync($"Partition Key:\t\t{String.Join(", ", collectionSetting.PartitionKeys)}");
            await Console.Out.WriteLineAsync($"Populate Operation:\tInserting {settings.NumberOfDocumentsToInsert} Documents Total");
            await Console.Out.WriteLineAsync("--------------------------------------------------------------------- ");
            await Console.Out.WriteLineAsync();

            await supplementalBenchmark.BenchmarkCollectionAsync(client, collection, settings.NumberOfDocumentsToPopulate, taskCount, false);
        
            await WaitAsync();
        }

        public async Task WaitAsync()
        {            
            Timer callbackTimer = null;
            callbackTimer = new Timer(Iteration, null, 1500, Timeout.Infinite);
            await Console.Out.WriteLineAsync("Press any key to exit...");
            await Console.Out.WriteLineAsync("Waiting...");
            Console.ReadKey();

            async void Iteration(object state)
            {
                await Task.Delay(1500);
                callbackTimer.Change(1500, Timeout.Infinite);
            }
        }
    }
}