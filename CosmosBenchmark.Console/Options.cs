using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CommandLine;

namespace CosmosBenchmark
{
    public class Options
    {
        [Option('t', "type", Required = false, Default = Types.Populate, HelpText = "Set type of benchmark to run")]
        public Types Type { get; set; }

        [Option('l', "location", Required = true, HelpText = "Set location to use when benchmarking")]
        public Locations Location { get; set; }

        public enum Types : int
        {
            [Display(Name = "Populate Database")]
            Populate = 0,

            [Display(Name = "Read Data")]
            Read,

            [Display(Name = "Write Data")]
            Write
        }

        public enum Locations : int
        {
            [Display(Name = "West US", ShortName = "westus")]
            WestUS = 0,

            [Display(Name = "East Asia", ShortName = "eastasia")]
            EastAsia,

            [Display(Name = "Southeast Asia", ShortName = "southeastasia")]
            SoutheastAsia,

            [Display(Name = "Central US", ShortName = "centralus")]
            CentralUS,

            [Display(Name = "East US", ShortName = "eastus")]
            EastUS,

            [Display(Name = "East US 2", ShortName = "eastus2")]
            EastUS2,

            [Display(Name = "North Central US", ShortName = "northcentralus")]
            NorthCentralUS,

            [Display(Name = "South Central US", ShortName = "southcentralus")]
            SouthCentralUS,

            [Display(Name = "North Europe", ShortName = "northeurope")]
            NorthEurope,

            [Display(Name = "West Europe", ShortName = "westeurope")]
            WestEurope,

            [Display(Name = "Japan West", ShortName = "japanwest")]
            JapanWest,

            [Display(Name = "Japan East", ShortName = "japaneast")]
            JapanEast,

            [Display(Name = "Brazil South", ShortName = "brazilsouth")]
            BrazilSouth,

            [Display(Name = "Australia East", ShortName = "australiaeast")]
            AustraliaEast,

            [Display(Name = "Australia Southeast", ShortName = "australiasoutheast")]
            AustraliaSoutheast,

            [Display(Name = "South India", ShortName = "southindia")]
            SouthIndia,

            [Display(Name = "Central India", ShortName = "centralindia")]
            CentralIndia,

            [Display(Name = "West India", ShortName = "westindia")]
            WestIndia,

            [Display(Name = "Canada Central", ShortName = "canadacentral")]
            CanadaCentral,

            [Display(Name = "Canada East", ShortName = "canadaeast")]
            CanadaEast,

            [Display(Name = "UK South", ShortName = "uksouth")]
            UKSouth,

            [Display(Name = "UK West", ShortName = "ukwest")]
            UKWest,

            [Display(Name = "West Central US", ShortName = "westcentralus")]
            WestCentralUS,

            [Display(Name = "West US 2", ShortName = "westus2")]
            WestUS2,

            [Display(Name = "Korea Central", ShortName = "koreacentral")]
            KoreaCentral,

            [Display(Name = "Korea South", ShortName = "koreasouth")]
            KoreaSouth,

            [Display(Name = "France Central", ShortName = "francecentral")]
            FranceCentral,

            [Display(Name = "France South", ShortName = "francesouth")]
            FranceSouth,

            [Display(Name = "Australia Central", ShortName = "australiacentral")]
            AustraliaCentral,

            [Display(Name = "Australia Central 2", ShortName = "australiacentral2")]
            AustraliaCentral2
        }
    }
}
