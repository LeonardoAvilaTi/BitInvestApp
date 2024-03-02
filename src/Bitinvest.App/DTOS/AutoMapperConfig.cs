using AutoMapper;
using Bitinvest.Domain.Entities;
using Bitinvest.Domain.ValueObjects;

namespace Bitinvest.App.DTOS
{
    public  class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ClienteDTO, Cliente>().ReverseMap();
            CreateMap<EnderecoDTO, Endereco>().ReverseMap();
            CreateMap<TelefoneDTO, Telefone>().ReverseMap();
            CreateMap<CpfDTO, Cpf>().ReverseMap();
        }
    }
}
