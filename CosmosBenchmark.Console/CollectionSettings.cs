using System;
using System.Collections.Generic;

namespace CosmosBenchmark
{
    public class CollectionSettings
    {
        public string Id { get; set; }    

        public List<string> PartitionKeys { get; set; }

        public Guid PartitionId { get; set; }

        public int Throughput { get; set; }
    }
}