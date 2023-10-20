using AutoMapper;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Entidades;

namespace LocadoraCarros.Application.MappingProfiles
{
    public class VeiculoMappingProfile : Profile
    {
        public VeiculoMappingProfile()
        {
            CreateMap<CriarVeiculoComando, Veiculo>().ReverseMap();
            CreateMap<Veiculo, VeiculoViewModel>().ReverseMap();
        }
    }
}
