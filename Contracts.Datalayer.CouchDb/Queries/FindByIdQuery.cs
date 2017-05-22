using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.DataLayers.Infrastructure;
using System;

namespace Cmas.DataLayers.CouchDb.Contracts.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<Contract>>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public FindByIdQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<Contract> Ask(FindById criterion)
        {
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.GetAsync<ContractDto>(criterion.Id);
            });

            if (result == null)
                return null;

            return _autoMapper.Map<Contract>(result.Content);
        }
    }
}