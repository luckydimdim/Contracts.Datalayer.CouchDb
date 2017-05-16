using System;
using System.Threading.Tasks;
using MyCouch;
using Cmas.BusinessLayers.Contracts.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.DataLayers.Infrastructure;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class DeleteContractCommand : ICommand<DeleteContractCommandContext>
    {
        private readonly CouchWrapper _couchWrapper;

        public DeleteContractCommand(IServiceProvider serviceProvider)
        {
            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
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