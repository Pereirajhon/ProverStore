using AutoMapper;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Model;

namespace ProverStore.Api.Configuration
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Produto, GetProdutoVM>()
                .ForMember(
                    p => p.ClienteIdsFavorito, 
                    src => src.MapFrom(f => f.Favoritos.Select(f => f.ClienteId))
                );
            CreateMap<Produto, UpdateProdutoVM>().ReverseMap();
            CreateMap<Produto, ProdutoVM>().ReverseMap();
            CreateMap<ShoppingCartItem,CarrinhoItemVM>().ReverseMap();
            CreateMap<Pedido, PedidoVM>().ReverseMap();
            CreateMap<Favorito, FavoritoVM>().ReverseMap();
            CreateMap<EnderecoCliente, EnderecoClienteVM>();
        }

    }
}
