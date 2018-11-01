> ![Azure Cosmos DB Icon](https://github.com/azure-immersion/cosmos-jekyll-theme/raw/master/assets/img/logo.png) 
> [Azure Cosmos DB](https://docs.microsoft.com/azure/cosmos-db/)

# Scaling, performance, and flexibility in Azure Cosmos DB

This demo script will help you demonstrate Azure Cosmos DB to an audience. The script comprises of a single Setup section that should be followed prior to starting the demo and three demo sections for each of the categories that will be demoed: "Scalability", "Performance", and "Flexibility".

===

# Demo notes

- The resource names may change in the screenshots. This occurs because the ARM template dynamically generates a unique name each time a demo is performed. You can safely disregard the resource names.

===

# Demo setup

This section should be completed prior to starting the demo. In this section, you will deploy the following resources:

- Azure Cosmos DB account in West US region
- Azure Container Instance in West US region
    - This instance will be used to initially seed and populate the database
- Azure Container Instances in Southeast Asia, North Europe, and West US regions
    - These instances will be used to test global performance of the account

Throughout this setup section, we will share animated images that will help you navigate the Azure portal and learn where specific features are located. The instructions in the remaining sections will use the same features in the portal so it is recommended to repeat the setup multiple times if you are unfamiliar with the Azure Cosmos DB experience in the portal.

## Step-by-step instructions

1. To get started, you must first deploy an [ARM template](azuredeploy.json) that will deploy all of the Azure Cosmos DB and Azure Container Instances resources and correctly configure them.

    > If you are using LODS as your demo environment, this ARM template will already have been deployed for you. There is no need to deploy the template a second time.

    For ease, you can click the link below to deploy your ARM template:

    <a href="https://portal.azure.com/#create/Microsoft.Template.2.0.0/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fseesharprun%2Fcosmos-demo%2Fmaster%2Fazuredeploy.json" target="_blank">
        <img src="media/deploytoazure.png" alt="Deploy to Azure" />
    </a>

    > A typicaly deployment takes approximately ten minutes.

1. Once your environment is deployed, you can observe the Azure Cosmos DB account in your Azure subscription.

    ![Deployed Resource Group](media/go_to_cosmos.gif)

    > The name of your resource group may differ from the screenshots.

1. Go to your resource group, locate and select the **Azure Container Instance** with **-populate-** in the name.

    ![Populate Container](media/go_to_aci_populate.gif)

1. In the **Settings** section of the container instance, select the **Containers** option to view your current container.

1. Click the **Connect** tab, select the **/bin/bash** option and then click the **Connect** button to connect to the running container.

    ![Running Container](media/aci_connect.gif)

1. Within the container, run the following command to populate your Azure Cosmos DB instance:

    ```
    ./cosmosbenchmark --location westus --type populate --database IoTDatabase --collection DeviceDataCollection --throughput 25000
    ```

1. Observe the output from the container application as it populates your Azure Cosmos DB instance with data.

    ![Container Ouput](media/benchmark_output.gif)

1. Wait for the benchmark application to finish processing.

1. Navigate back to the Azure Cosmos DB instance.

1. Select the **Data Explorer** option to view your newly created database and collection.

    ![View Data Explorer](media/data_explorer.gif)

1. In the Data Explorer, select the **DeviceDataCollection** collection and then click the **New SQL Query** button.

    ![New SQL Query](media/new_query.gif)

1. In the **New SQL Query** window, enter the following SQL command:

    ```
    SELECT * FROM data
    ```

1. Press the **Execute** button to issue the query and observe the query results.

    ![Execute Query](media/execute_query.png)

1. Enter and execute the following command:

    ```
    SELECT COUNT(1) FROM data
    ```

1. Click the **Documents** link under the **DeviceDataCollection** node and then click the **New Document** button.

    ![New Document](media/new_document.gif)

1. In the document editor, enter the following JSON content:

    ```
    {
        "Version": 1,
        "DeviceId": "59eec13e-160b-4e63-a20c-bb9ea57208e4",
        "LocationId": "2f4a4707-ca74-447f-aa90-d03bbbc27d2d",
        "DeviceLocationComposite": "59eec13e-160b-4e63-a20c-bb9ea57208e4_2f4a4707-ca74-447f-aa90-d03bbbc27d2d",
        "SubmitDay": "2018-10-25",
        "SubmitHour": "2018-10-25-18",
        "SubmitMinute": "2018-10-25-18-24",
        "SubmitSecond": "2018-10-25-18-24-35",
        "TemperatureCelsius": 21.216567948561426,
        "Humidity": 0.2047588327456074
    }
    ```

1. Click the **Save** button to persist the document and then observe the newly created document.

    ![Save New Document](media/save_document.gif)

1. Finally in the Data Explorer, select the **DeviceDataCollection** collection and then click the **Settings** option.

    ![Collection Settings]()

1. In the settings pane, change the throughput of the collection to ``10000`` RUs (request units).

    > It is typical to use more RUs for a data import task and then to scale back down to your "normal" thoughput allocation.

1. Click the **Save** button at the top of the settings pane.

    ![Save Settings]()

===

# Scalability and elasticity with Azure Cosmos DB

For the first demo, we will compare write performance at two different throughput levels. We will start with a low throughput level (10K-50K range) and then test writes using a higher throughput level (100K+ range). The goal in this demo is to show how performance and throughput is a "knob" that can be adjusted dynamically to meet an application's changing needs.

## Step-by-step instructions

1. To start, we will go into the Azure Cosmos DB account. This can be accomplished by clicking on the **Resource Groups** link in the portal, selecting your previously created group and then selecting the sole **Azure Cosmos DB** resource.

    ![Selecting Azure Cosmos DB account](media/cosmos_account.png)

1. In the Azure Cosmos DB resource's blade, select the **Data Explorer** option.

    ![Data Explorer](media/data_explorer.png)

1. In the **Data Explorer**, click the **New Collection** button.

    ![New Collection](media/new_collection.png)

    > You can call out to attendees that you will create a new collection where you intend to perform the demo.

1. In the **New Collection** popup, perform the following actions:

    1. In the **Database id** section, select the **Create new** option, and enter the value ``TestDatabase`` in *Type a new database id* field.

    1. Ensure that the **Provision database throughput** checkbox is not selected.

    1. In the **Collection id** field, enter the value ``ThroughputDemo``.

    1. In the **Storage capacity** section, select the **Unlimited** option.

    1. In the **Partition key** field, enter the value ``/DeviceId``.

    1. In the **Throughput** section, enter the value ``2000``.

    1. Click the **OK** button.

    1. Wait for the new database and collection to be created.

    ![New Collection Options](media/new_collection_settings.png)

1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the **Container instances** resource with **-westus-** in the name.

    ![West US Container Instance](media/container_instance_westus.png)

    > Point out to attendees that you are selecting a container that is running in the same region as your Azure Cosmos DB account. This container has a benchmarking application built-in that is open-source and written using .NET Core.

1. In the **Settings** section of the container instance, select the **Containers** option to view your current container.

    ![Container Settings](media/container_settings.png)

1. In the **Containers** section, locate and click the **Connect** tab.

    ![Connect Tab](media/container_tabs.png)

1. In the **Connect** tab, select the **/bin/bash** option and then click the **Connect** button to connect to the running container.

    ![Connect to Container Using Bash Shell](media/connect_container.png)

1. Within the container, run the following command to benchmark the write performance of your Azure Cosmos DB instance:

    ```
    ./cosmosbenchmark --location westus --type write --database TestDatabase --collection ThroughputDemo --throughput 2000
    ```

    ![Run Script in Container](media/container_job.png)

    > This benchmark application will blast the account with writes. Explain to the attendees that this benchmark application will usee 5000 RUs to test how long it takes to upload 10,000 documents. In our testing, this can take anywhere from 45-60 seconds.
    
1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the sole **Azure Cosmos DB** resource.

1. In the Azure Cosmos DB resource's blade, select the **Data Explorer**.

1. In the Data Explorer, expand the **TestDatabase** database, select the **ThroughputDemo** collection, and then click the **Scale & Settings** option.

    ![Collection Settings](media/collection_settings.png)
  
1. In the settings pane, change the throughput of the collection from ``2000`` to ``10000`` RUs (request units).

    > Let the attendees know that we are exponentially increasing the RUs by five times. Point out that we have now elastically scaled our database by only changing a simple field and clicking a save button.

1. Click the **Save** button at the top of the settings pane.

    ![Collection Throughput](media/collection_throughput.png)

1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the **Container instances** resource with **-westus-** in the name.

1. In the **Settings** section of the container instance, select the **Containers** option to view your current container.

1. Click the **Connect** tab, select the **/bin/bash** option and then click the **Connect** button to connect to the running container.

1. Within the container, run the following command to benchmark the write performance of your Azure Cosmos DB instance again with the new throughput level:

    ```
    ./cosmosbenchmark --location westus --type write --database TestDatabase --collection ThroughputDemo --throughput 10000
    ```

    > Point out to attendees the difference in performance between the two benchmarks. In our testing, the second benchmark completed in 5-10 seconds.

    > If an attendee ask why we are "telling" the benchmark application about our throughput, this is to avoid rate limiting. Our benchmark application is using a simple math formula to determine how much data it can send in parallel while avoiding rate limiting for the entire collection. If we didn't tell the benchmark application about our throughput limit, it could potentially send too much data at once and get a HTTP 429 error code.

## More Reading

If attendees want to learn more about scaling throughput in Azure Cosmos DB, it is recommended that they read the following article: <https://docs.microsoft.com/azure/cosmos-db/request-units>

===

# Azure Cosmos DB Performance

For the next demo, we will run a script that uses a high-resolution timer to measure the read performance between different Azure Container Instances across various Azure regions and a multi-region Azure Cosmos DB instance. Our intention is to show how having multiple read regions for an Azure Cosmos DB account will make it functionally behave like a CDN for dynamic data.

## Step-by-step instructions

1. To start, we will go into the Azure Cosmos DB account. This can be accomplished by clicking on the **Resource Groups** link in the portal, selecting your previously created group, and then selecting the sole **Azure Cosmos DB** resource.

    ![Selecting Azure Cosmos DB account](media/cosmos_account_2.png)

1. In the Azure Cosmos DB resource's blade, select the **Data Explorer** option.

    ![Data Explorer](media/data_explorer_2.png)

1. In the **Data Explorer**, locate and expand the existing **IoTDatabase** database. Then select the **DeviceDataCollection** collection.

    ![Pre-Created Collection](media/data_explorer_collection.png)

    > Call out to attendees that you have pre-created a collection with 50,000 documents. We will use this collection in our read benchmark.

1. Click the **New SQL Query** button.

    ![New SQL Query](media/new_sql_query.png)

1. In the **New SQL Query** window, enter the following SQL command:

    ```
    SELECT TOP 1 * FROM data WHERE data.DeviceId = "00000000-0000-0000-0000-000000000000"
    ```

    > We know that there is a device out there with a Device Id of "00000000-0000-0000-0000-000000000000". This query will return the first record associated with that partition key. 

1. Press the **Execute** button to issue the query and observe the query results.

    ![Execute Query](media/execute_query_2.png)

    ![Execute Query](media/execute_query_results.png)

    > This is the record we will use when we test read performance.

1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the **Container instances** resource with **-westus-** in the name.

    ![West US Container Instance](media/container_instance_westus_2.png)

    > Point out to attendees that you are selecting a container that is running in the same region as your Azure Cosmos DB account. In the absence of global distribution, we would have to send requests around the world. This can create a lot of latency. To reduce latency, we deployed this Azure Container Instance to the same region as our Azure Cosmos DB account.

1. In the **Settings** section of the container instance, select the **Containers** option to view your current container.

    ![Container Settings](media/container_settings_2.png)

1. In the **Containers** section, locate and click the **Connect** tab.

    ![Connect Tab](media/container_tabs_2.png)

1. In the **Connect** tab, select the **/bin/bash** option and then click the **Connect** button to connect to the running container.

    ![Connect to Container Using Bash Shell](media/connect_container_2.png)

1. Within the container, run the following command to benchmark the read performance of your Azure Cosmos DB instance:

    ```
    ./cosmosbenchmark --location westus --type read --database IoTDatabase --collection DeviceDataCollection
    ```

    > This read operation was very fast. We can attribute this to the fact that we have both our Azure Cosmos DB account and our container instance in the same region.

    ![Read Benchmark - West US to West US](media/read_west_results.png)

1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the sole **Azure Cosmos DB** resource again.

1. In the Azure Cosmos DB resource's blade, select the **Replicate data globally** option.

    ![Replicate Globally Option](media/replicate_data.png)

1. Observe that the database is already replicated to the **West US**, **Southeast Asia**, and **North Europe** regions.

    ![Replication Destinations](media/replication_destinations.png)
    
1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the **Container instances** resource with **-westus-** in the name.

1. In the **Settings** section of the container instance, select the **Containers** option to view your current container.

1. In the **Containers** section, locate and click the **Connect** tab.

1. In the **Connect** tab, select the **/bin/bash** option and then click the **Connect** button to connect to the running container.

1. Within the container, run the following command to benchmark the read performance of your Azure Cosmos DB instance using a different region:

    ```
    ./cosmosbenchmark --location southeastasia --type read --database IoTDatabase --collection DeviceDataCollection
    ```

    > We are using our **West US** container instance to try and read data from the Azure Cosmos DB **Southeast Asia** read region. Unfortunately, our request has to be sent around the world and this causes a lot of latency. No matter what, you can't beat the speed of light (physics).

    ![Read Benchmark - West US to Southeast Asia](media/read_west_southeast_results.png)

1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the **Container instances** resource with **-southeastasia-** in the name.

    ![Southeast Asia Container Instance](media/container_instance_southeastasia.png)

1. In the **Settings** section of the container instance, select the **Containers** option to view your current container.

1. In the **Containers** section, locate and click the **Connect** tab.

1. In the **Connect** tab, select the **/bin/bash** option and then click the **Connect** button to connect to the running container.

1. Within the container, run the following command to benchmark the read performance of your Azure Cosmos DB instance using the same region as the container:

    ```
    ./cosmosbenchmark --location southeastasia --type read --database IoTDatabase --collection DeviceDataCollection
    ```

    > We are now using the **Southeast Asia** container instance to try and read data from the Azure Cosmos DB **Southeast Asia** read region. You can see a  performance difference here. We have "cheated" the speed of light by bringing the data closer to the application/client.

    ![Read Benchmark - Southeast Asia to Southeast Asia](media/read_southeast_results.png)

1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the **Container instances** resource with **-northeurope-** in the name.

    ![North Europe Container Instance](media/container_instance_northeurope.png)

1. In the **Settings** section of the container instance, select the **Containers** option to view your current container.

1. In the **Containers** section, locate and click the **Connect** tab.

1. In the **Connect** tab, select the **/bin/bash** option and then click the **Connect** button to connect to the running container.

1. Within the container, run the following command to benchmark the read performance of your Azure Cosmos DB instance using the same region as the container:

    ```
    ./cosmosbenchmark --location northeurope --type read --database IoTDatabase --collection DeviceDataCollection
    ```

    > Since we have multiple clients and multiple Azure Cosmos DB read locations. We essentially have a CDN for dynamic data.

    ![Read Benchmark - North Europe to North Europe](media/read_north_results.png)

1. Click on the **Resource Groups** link in the portal, selecting your previously created group, and then select the sole **Azure Cosmos DB** resource again.

1. In the Azure Cosmos DB resource's blade, select the **Default consistency** option.

    ![Consistency Level Option](media/consistency_level_option.png)

    > You can configure the default consistency level on your Cosmos DB account at any time. The default consistency level configured on your account applies to all Cosmos databases (and containers) under that account. All reads and queries issued against a container or a database will use that consistency level by default.

1. Observe that the default consistency level is set to **Session**.

    ![Session Consistency](media/session_consistency.png)

    > Session consistency is a "middle of the road" consistency option that strikes a good balance between the polarized Strong and Eventual consistency options by enforcing strong consistency within a client session and eventual consistency for all other clients.

1. Change the consistency level to **Eventual** and then click the **Save** button.

    ![Eventual Consistency](media/eventual_consistency.png)

    > You can change the default consistency level that is applied to all future requests. Eventual consistency improves read scalability, offers higher availability and often results in lower latency. The tradeoff is that a client may potentially see "stale" data.

## More Reading

If attendees want to learn more about Azure Cosmos DB and global distribution, it is recommended that they read the following article: <https://docs.microsoft.com/azure/cosmos-db/distribute-data-globally>

===

# Document Flexibility using Azure Cosmos DB

For the last demo, we will populate our Azure Cosmos DB instance with various documents that do not conform to a standardized schema or structure. We are going to take advantage of the automatic indexing and flexible nature of Azure Cosmos DB to query for documents based on fields that may or may not be in every document.

## Step-by-step instructions

1. To start, we will go into the Azure Cosmos DB account. This can be accomplished by clicking on the **Resource Groups** link in the portal, selecting your previously created group, and then selecting the sole **Azure Cosmos DB** resource.

    ![Selecting Azure Cosmos DB account](media/)

1. In the Azure Cosmos DB resource's blade, select the **Data Explorer** option.

    ![Data Explorer](media/)

1. In the **Data Explorer**, locate and expand the existing **IoTDatabase** database. Then select the **DeviceDataCollection** collection.

    ![Pre-Created Collection](media/)

    > We are creating a new collection

1. Click the **New SQL Query** button.

## More Reading

If attendees want to learn more about modeling flexibly structed data in Azure Cosmos DB, it is recommended that they read the following article: <https://docs.microsoft.com/azure/cosmos-db/modeling-data>

===