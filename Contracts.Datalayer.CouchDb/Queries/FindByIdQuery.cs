using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using MyCouch;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.ErrorHandler;
using System;
using System.Net;
using MyCouch.Responses;

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
                GetEntityResponse<ContractDto> result = await client.Entities.GetAsync<ContractDto>(criterion.Id);

                if (!result.IsSuccess)
                {
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundErrorException(result.ToStringDebugVersion());
                    }

                    throw new Exception(result.ToStringDebugVersion());
                }

                return _autoMapper.Map<Contract>(result.Content);
            }
        }
    }
}