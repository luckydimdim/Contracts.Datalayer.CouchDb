﻿using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;
using Cmas.Infrastructure.Domain.Commands;
using Cmas.BusinessLayers.Contracts.CommandsContexts;
using Cmas.DataLayers.Infrastructure;
using System;

namespace Cmas.DataLayers.CouchDb.Contracts.Commands
{
    public class CreateContractCommand : ICommand<CreateContractCommandContext>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public CreateContractCommand(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<CreateContractCommandContext> Execute(CreateContractCommandContext commandContext)
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