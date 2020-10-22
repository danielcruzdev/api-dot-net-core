using AutoMapper;
using Entity.Pessoa;
using ViewModel.Pessoa;

namespace api_dot_net_core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<PessoaViewModel, Pessoa>();
        }
    }
}
