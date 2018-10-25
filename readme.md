> ![Azure Cosmos DB Icon](https://github.com/azure-immersion/cosmos-jekyll-theme/raw/master/assets/img/logo.png) 
> [Azure Cosmos DB](https://docs.microsoft.com/azure/cosmos-db/)

# Scaling, Performance, and Flexibility in Azure Cosmos DB

This demo script will help you demonstrate Azure Cosmos DB to an audience. The script comprises of a single Setup section that should be followed prior to starting the demo and three demo sections for each of the categories that will be demoed: "Scalability", "Performance", and "Flexibility".

===

# Demo Setup

This section should be completed prior to starting the demo. In this section, you will deploy the following resources:

- Azure Cosmos DB account in West US region
- Azure Container Instance in West US region
    - This instance will be used to initially seed and populate the database
- Azure Container Instances in Brazil South, North Europe, and West US regions
    - These instances will be used to test global performance of the account

To get started, you must first deploy an [ARM template](azuredeploy.json) that will deploy all of those resources and correctly configure them.

> If you are using LODS as your demo environment, this ARM template will already have been deployed for you. There is no need to deploy the template a second time.

For ease, you can click the link below to deploy your ARM template:

[![Deploy to Azure](media/deploytoazure.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fseesharprun%2Fcosmos-demo%2Fmaster%2Fazuredeploy.json)

Once your environment is deployed, you can observe the resource group in your Azure subscription:

![Deployed Resource Group]()

> The name of your resource group may differ from the screenshots.



===

# Scalability and Elasticity with Azure Cosmos DB



===

# Azure Cosmos DB Performance



===

# Document Flexibility using Azure Cosmos DB



===
