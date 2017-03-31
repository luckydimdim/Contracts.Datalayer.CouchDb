using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.Contracts;
using Cmas.DataLayers.CouchDb.Contracts.Queries;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.DataLayers.CouchDb.Contracts.Commands;
using Cmas.BusinessLayers.Contracts.CommandsContexts;

namespace ConsoleTests
{
    class Program
    {
        private static IMapper _mapper;


        static void Main(string[] args)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                _mapper = config.CreateMapper();


                //FindByIdQueryTest().Wait();
                //FindAllEntitiesTest().Wait();
                //UpdateContractCommand().Wait();
                //DeleteContractCommand().Wait();
                CreateContractCommand().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
         

            Console.ReadKey();
        }


        static async Task<bool> FindByIdQueryTest()
        {
            FindByIdQuery findByIdQuery = new FindByIdQuery(_mapper);
            FindById criterion = new FindById("26270cfa2422b2c4ebf158285e027730");
            Contract result = null;

            try
            {
                result = await findByIdQuery.Ask(criterion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine(result.Id);

            return true;
        }

        static async Task<bool> FindAllEntitiesTest()
        {
            AllEntitiesQuery query = new AllEntitiesQuery(_mapper);
            IEnumerable<Contract> result = null;

            result = await query.Ask(new AllEntities());
           
            Console.WriteLine(result.Count());

            return true;
        }

        static async Task<bool> UpdateContractCommand()
        {
            var commandContext = new UpdateContractCommandContext();
            var createContractCommand = new UpdateContractCommand(_mapper);

            commandContext.Form.Name = "new name";
            commandContext.Form.Id = "26270cfa2422b2c4ebf158285e0200a5";

            var result = await createContractCommand.Execute(commandContext);

            return true;
        }

        static async Task<bool>DeleteContractCommand()
        {
            var commandContext = new DeleteContractCommandContext();
            var command = new DeleteContractCommand();

            commandContext.Id = "26270cfa2422b2c4ebf158285e0241e5";

            var result = await command.Execute(commandContext);

            return true;
        }

        static async Task<bool> CreateContractCommand()
        {
            var commandContext = new CreateContractCommandContext();
            var command = new CreateContractCommand(_mapper);

            var contract = new Contract();

            contract.Name = "name";
            contract.TemplateSysName = "default";

            commandContext.Form = contract;

            var result = await command.Execute(commandContext);

            return true;
        }
    }
}
