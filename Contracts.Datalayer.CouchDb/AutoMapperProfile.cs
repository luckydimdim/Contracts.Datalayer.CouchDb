using AutoMapper;
using Cmas.BusinessLayers.Contracts.Entities;
using Cmas.DataLayers.CouchDb.Contracts.Dtos;

namespace Cmas.DataLayers.CouchDb.Contracts
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contract, ContractDto>();

            CreateMap<ContractDto, Contract>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src._id));
        }
    }
}