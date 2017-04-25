using System;
using System.Threading.Tasks;
using MyCouch;
using Cmas.BusinessLayers.Contracts.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using Microsoft.Extensions.Logging;
using Cmas.DataLayers.Infrastructure;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class DeleteContractCommand : ICommand<DeleteContractCommandContext>
    {
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public DeleteContractCommand(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DeleteContractCommand>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<DeleteContractCommandContext> Execute(DeleteContractCommandContext commandContext)
        {
            var header = await _couchWrapper.GetHeaderAsync(commandContext.Id);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Documents.DeleteAsync(commandContext.Id, header.Rev);
            });
            
            return commandContext;
        }
    }
}