using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using MyCouch;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.Infrastructure.Domain.Criteria;

namespace Cmas.DataLayers.CouchDb.Contracts.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<Contract>>
    {

        private IMapper _autoMapper;

        public FindByIdQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<Contract> Ask(FindById criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            { 
                var dto = await client.Entities.GetAsync<ContractDto>(criterion.Id);

                var contract = _autoMapper.Map<Contract>(dto.Content);
                contract.Id = dto.Content._id;

                return contract;
            }

        }
    }
}
