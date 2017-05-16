using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Contracts.CommandsContexts;
using Cmas.DataLayers.Infrastructure;
using System;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class UpdateContractCommand : ICommand<UpdateContractCommandContext>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public UpdateContractCommand(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<UpdateContractCommandContext> Execute(UpdateContractCommandContext commandContext)
        {
            // FIXME: нельзя так делать, надо от frontend получать Rev
            var header = await _couchWrapper.GetHeaderAsync(commandContext.Form.Id);

            var entity = _autoMapper.Map<ContractDto>(commandContext.Form);
            entity._rev = header.Rev;

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PutAsync(entity._id, entity);
            });

            return commandContext; // TODO: возвращать _revid
        }
    }
}