using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;

namespace CosmosBenchmark
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Parser parser = new Parser(config =>
            {
                config.CaseInsensitiveEnumValues = true;
                config.HelpWriter = Console.Error;
            });
            await parser.ParseArguments<Options>(args)
                .MapResult(
                    async options => await RunLogic(options),
                    errors => Task.CompletedTask
                );
        }

        private static async Task RunLogic(Options options)
        {       
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile($"appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            string endpointUrl = configuration.GetValue<string>("COSMOS_URI");
            string primaryKey = configuration.GetValue<string>("COSMOS_KEY");

            await Console.Out.WriteLineAsync($"Location:\t{options.Location.GetDisplayName()}");  
            await Console.Out.WriteLineAsync($"Task Type:\t{options.Type.GetDisplayName()}");               

            await Console.Out.WriteLineAsync($"Endpoint Uri:\t{endpointUrl}");
            await Console.Out.WriteLineAsync($"Primary Key:\t{primaryKey}"); 

            await Console.Out.WriteLineAsync($"Database:\t{options.Database}");
            await Console.Out.WriteLineAsync($"Collection:\t{options.Collection}"); 

            ConnectionPolicy policy = new ConnectionPolicy();
            CosmosSettings settings = new CosmosSettings();

            configuration.GetSection(nameof(ConnectionPolicy)).Bind(policy);
            configuration.GetSection(nameof(CosmosSettings)).Bind(settings);

            policy.ConnectionMode = ConnectionMode.Direct;
            policy.ConnectionProtocol = Protocol.Tcp;
            policy.SetCurrentLocation(options.Location.GetDisplayShortName());

            settings.EndpointUri = new Uri(endpointUrl, UriKind.Absolute);
            settings.PrimaryKey = primaryKey;
            settings.Database = options.Database;
            settings.Collection = options.Collection;
            settings.Throughput = options.Throughput;

            using (DocumentClient client = new DocumentClient(settings.EndpointUri, settings.PrimaryKey, policy))
            {
                CollectionSettings collection = new CollectionSettings
                {
                    Id = settings.Collection,
                    PartitionKeys = new List<string> { "/DeviceId" },
                    PartitionId = Guid.Empty,
                    Throughput = settings.Throughput
                };
                if (options.Type == Options.Types.Populate)
                {
                    await new PopulateJob().StartAsync(client, policy, settings, collection);
                }
                if (options.Type == Options.Types.Write)
                {
                    await new WriteBenchmark().StartBenchmarkAsync(client, policy, settings, collection);
                }
                if (options.Type == Options.Types.Read)
                {
                    await new ReadBenchmark().StartBenchmarkAsync(client, policy, settings, collection);                    
                }
            }
        }
    }
}