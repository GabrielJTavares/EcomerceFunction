using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace checkout2.Data
{
    public class NoSqlDataBase<T>
    {
        private static readonly string EndpointUri = "https://cosmosdbecommerce.documents.azure.com:443/";
        private static readonly string PrimaryKey = "icNRT60QS7hjkYaJwHxodztRfah8QUJ7JKHzmd6pFV0LJxGL9WI4ygDlNABNzZObVEvhCNr0hv3hACDbO2A4xw==";
        private readonly string databaseId = "ecommercecomosdb";

        public async Task<IEnumerable<T>> GetAllItens(string containerId)
        {
            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            var sqlQueryText = "select * from c";

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            List<T> list = new();

            QueryDefinition queryDefinition = new(sqlQueryText);

            var iterator = container.GetItemQueryIterator<T>(queryDefinition);

            while(iterator.HasMoreResults)
            {
                FeedResponse<T> result = await iterator.ReadNextAsync();
                foreach(var item in result)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public async Task<IEnumerable<T>> GetByPredicate(string containerId, Expression<Func<T,bool>> predicate) {

            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            var query = container.GetItemLinqQueryable<T>();
            var iterator = query.Where(predicate).ToFeedIterator();
            var resultado = await iterator.ReadNextAsync();
            return resultado.ToList();
        }

        public async Task Add(string containerId, T data, string category)
        {
            try
            {
                CosmosClient cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

                Database database = cosmosClient.GetDatabase(databaseId);
                Container container = database.GetContainer(containerId);

                await container.CreateItemAsync<T>(data, new PartitionKey(category));
            }catch(Exception ex)
            {
                var teste = "";
            }
        }

        public async Task Update(string containerId, T data, string category)
        {
            try
            {
                CosmosClient cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

                Database database = cosmosClient.GetDatabase(databaseId);
                Container container = database.GetContainer(containerId);

                await container.UpsertItemAsync<T>(data, new PartitionKey(category));
            }
            catch (Exception ex)
            {
                var teste = "";
            }
        }
    }
}
