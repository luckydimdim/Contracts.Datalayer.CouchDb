using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using MyCouch;
using MyCouch.Requests;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.Infrastructure.Domain.Criteria;

namespace Cmas.DataLayers.CouchDb.Contracts.Queries
{
    public class AllEntitiesQuery : IQuery<AllEntities, Task<IEnumerable<Contract>>>
    {
        private IMapper _autoMapper;

        public AllEntitiesQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<Contract>> Ask(AllEntities criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                var result = new List<Contract>();

                var query = new QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.AllDocsViewName);

                var viewResult = await client.Views.QueryAsync<ContractDto>(query);

                foreach (var row in viewResult.Rows.OrderByDescending(s=>s.Value.CreatedAt))
                { 
                    var contract = _autoMapper.Map<Contract>(row.Value);
                    contract.Id = row.Value._id;
                    result.Add(contract);
                }

                return result;
            }
        }
    }


}
