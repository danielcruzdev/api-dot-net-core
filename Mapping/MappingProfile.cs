using api_dot_net_core.Entities.BaseExcel;
using api_dot_net_core.Entities.RCompras;
using api_dot_net_core.ViewModels.RCompras;
using api_dot_net_core.ViewModels.Shared;
using AutoMapper;
using Entity;
using ViewModel;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Base Excel Cabeçalho
            CreateMap<BaseCabecalho, BaseExcelHeader>();

            //Pessoa
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<PessoaViewModel, Pessoa>();

            //Relatório Compras
            CreateMap<RCompraCabecalho, RCompraCabecalhoViewModel>();
            CreateMap<RCompra, RCompraViewModel>();
            CreateMap<RCompraCliente, RCompraClienteViewModel>();
            CreateMap<RCompraEmpresa, RCompraEmpresaViewModel>();
            CreateMap<RCompraCategoria, RCompraCategoriaViewModel>();
            CreateMap<RCompraProduto, RCompraProdutoViewModel>();
        }
    }
}
