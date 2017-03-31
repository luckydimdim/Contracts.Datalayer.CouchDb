using System;
using System.Threading.Tasks;
using MyCouch;
using Cmas.BusinessLayers.Contracts.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class DeleteContractCommand : ICommand<DeleteContractCommandContext>
    {
        public async Task<DeleteContractCommandContext> Execute(DeleteContractCommandContext commandContext)
        {
            using (var store = new MyCouchStore("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "cmas"))
            {
                bool success =  await store.DeleteAsync(commandContext.Id);

                if (!success)
                {
                    throw new Exception("error while deleting");
                }

                return commandContext;
            }

        }
    }
}
