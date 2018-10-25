using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

namespace CosmosBenchmark
{
    public class ReadBenchmark
    {
        public async Task StartBenchmarkAsync(DocumentClient client, ConnectionPolicy policy, CosmosSettings settings, CollectionSettings collectionSetting)
        {
            await Task.Delay(100);
        }
    }
}