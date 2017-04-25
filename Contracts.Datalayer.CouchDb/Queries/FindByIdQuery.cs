using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.DataLayers.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.Contracts.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<Contract>>
    {
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public FindByIdQuery(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<FindByIdQuery>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<Contract> Ask(FindById criterion)
        {
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.GetAsync<ContractDto>(criterion.Id);
            });

            return _autoMapper.Map<Contract>(result.Content);
        }
    }
}