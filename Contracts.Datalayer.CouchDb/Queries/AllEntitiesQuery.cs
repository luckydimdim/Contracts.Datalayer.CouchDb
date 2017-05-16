using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using MyCouch.Requests;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.DataLayers.Infrastructure;
using Cmas.Infrastructure.Domain.Criteria;
using System;

namespace Cmas.DataLayers.CouchDb.Contracts.Queries
{
    public class AllEntitiesQuery : IQuery<AllEntities, Task<IEnumerable<Contract>>>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public AllEntitiesQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<IEnumerable<Contract>> Ask(AllEntities criterion)
        {
            var result = new List<Contract>();
            var query = new QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.AllDocsViewName);

            var viewResult = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Views.QueryAsync<ContractDto>(query);
            });

            foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.CreatedAt))
            {
                result.Add(_autoMapper.Map<Contract>(row.Value));
            }

            return result;
        }
    }
}