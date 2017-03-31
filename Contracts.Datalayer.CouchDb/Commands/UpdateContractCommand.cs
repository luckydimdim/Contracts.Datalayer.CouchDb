using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using MyCouch;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Contracts.CommandsContexts;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class UpdateContractCommand : ICommand<UpdateContractCommandContext>
    {
        private IMapper _autoMapper;

        public UpdateContractCommand(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<UpdateContractCommandContext> Execute(UpdateContractCommandContext commandContext)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                // FIXME: нельзя так делать, надо от frontend получать
                var existingDoc = (await client.Entities.GetAsync<ContractDto>(commandContext.Form.Id)).Content;

                var newDto = _autoMapper.Map<ContractDto>(commandContext.Form);

                newDto._id = existingDoc._id;
                newDto.Status = existingDoc.Status;
                newDto._rev = existingDoc._rev;
                newDto.UpdatedAt = DateTime.Now;

                var result = await client.Entities.PutAsync(newDto._id, newDto);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Error);
                }

                // TODO: возвращать _revid

                return commandContext;
            }

        }
    }
}
