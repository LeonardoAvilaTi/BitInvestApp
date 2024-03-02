using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Bitinvest.Workers
{
    public static class AprovadorCompraCripto
    {
        [Function(nameof(AprovadorCompraCripto))]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context, Guid idCompra)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(AprovadorCompraCripto));
            logger.LogInformation("Iniciando Processo de Aprovação de Compra Cripto");
            bool debitouConta = false;

            var aceitouOferta = await context.CallActivityAsync<bool>(nameof(EnviandoOfertaCorretora), idCompra);
            if(aceitouOferta)
                debitouConta = await context.CallActivityAsync<bool>(nameof(DebitandoBanco), idCompra);
            
            if(debitouConta)
                await context.CallActivityAsync<string>(nameof(AprovandoCompra), idCompra);

        }

        [Function(nameof(EnviandoOfertaCorretora))]
        public static bool EnviandoOfertaCorretora([ActivityTrigger] Guid idCompra, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(EnviandoOfertaCorretora));
            logger.LogInformation("Enviando Oferta para corretora!");
            return true;
        }

        [Function(nameof(DebitandoBanco))]
        public static bool DebitandoBanco([ActivityTrigger] Guid idCompra, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(DebitandoBanco));
            logger.LogInformation("Debitando valor na conta bancária!");
            return true;
        }

        [Function(nameof(AprovandoCompra))]
        public static void AprovandoCompra([ActivityTrigger] Guid idCompra, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(AprovandoCompra));
            logger.LogInformation("Aprovando movimento de compra!");
            var sql = $"UPDATE MovimentoContaCripto SET Ativo = 1 WHERE id = '{idCompra}'";
            var stringConexao = "Server=localhost;Database=sqldb-bitinvest;User Id=sa;Password=intedata@2023;TrustServerCertificate=True;";
            using var sqlConnection = new SqlConnection(stringConexao);
            sqlConnection.Open();
            var sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        [Function(nameof(QueueStart))]
        public static async Task QueueStart(
            [QueueTrigger("compras-cripto", Connection = "")] QueueMessage message,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(QueueStart));

            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(AprovadorCompraCripto), Guid.Parse(message.Body.ToString()));

            logger.LogInformation("Iniciado processo de conclusão da compra nº '{instanceId}'.", instanceId);

        }
    }
}
