using System;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;

namespace DataSink
{
    public static class DataSinkFunc
    {
        private static PruuLog pruuLogger;
        //Change this to something unique to you
        private static readonly string pruuLogKey = "minicursoiot";
        //Change this to your table name
        private static readonly string tableName = "jl_metrics";
        [FunctionName("datasink-func")]
        public static async Task Run([ServiceBusTrigger("datasink-q", Connection = "ServiceBusConnectionString")]string message, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
            
            var config = new ConfigurationBuilder()
                        .SetBasePath(context.FunctionAppDirectory)
                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

            if (pruuLogger == null)
            {
                pruuLogger = new PruuLog();
            }
            
            pruuLogger.LogInfo(pruuLogKey, message);

            var receivedMessage = JsonConvert.DeserializeObject<OutboundMessage>(message);
            using var connection = new NpgsqlConnection(config["SqlConnectionString"]);
            string sql = $"INSERT INTO {tableName} (id, device_id, timestamp, metric_name, metric_value) VALUES (@Id, @DeviceId, @Timestamp, @MetricName, @MetricValue)";
            await connection.ExecuteAsync(sql, receivedMessage.Metrics);

        }
    }
}
