using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using MyCouch;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Contracts.CommandsContexts;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class CreateContractCommand : ICommand<CreateContractCommandContext>
    {
        private IMapper _autoMapper;

        public CreateContractCommand(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<CreateContractCommandContext> Execute(CreateContractCommandContext commandContext)
        {
            using (var store = new MyCouchStore("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "cmas"))
            {

                var doc = _autoMapper.Map<ContractDto>(commandContext.Form);

                doc._id = null;
                doc._rev = null;
                doc.UpdatedAt = DateTime.Now;
                doc.CreatedAt = DateTime.Now;

                var result = await store.Client.Entities.PostAsync(doc);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Error);
                }

                commandContext.Id = result.Id;

                return commandContext;
            }

        }
    }
}
