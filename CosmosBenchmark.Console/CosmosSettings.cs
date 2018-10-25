using System;

namespace CosmosBenchmark
{
    public class CosmosSettings
    {
        public Uri EndpointUri { get; set; }

        public string PrimaryKey { get; set; }

        public string Database { get; set; }

        public string Collection { get; set; }

        public int DegreeOfParallelism { get; set; }

        public int NumberOfDocumentsToInsert { get; set; }

        public int NumberOfDocumentsToPopulate { get; set; }

        public int Throughput { get; set; }
    }
}