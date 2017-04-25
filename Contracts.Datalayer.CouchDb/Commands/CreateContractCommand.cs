using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using MyCouch;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Contracts.CommandsContexts;
using Cmas.DataLayers.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class CreateContractCommand : ICommand<CreateContractCommandContext>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public CreateContractCommand(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<CreateContractCommand>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<CreateContractCommandContext> Execute(CreateContractCommandContext commandContext)
        {
            using (var store = new MyCouchStore(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                var doc = _autoMapper.Map<ContractDto>(commandContext.Form);

                var result = await _couchWrapper.GetResponseAsync(async (client) =>
                {
                    return await client.Entities.PostAsync(doc);
                });

                commandContext.Id = result.Id;

                return commandContext;
            }
        }
    }
}