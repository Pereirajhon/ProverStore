/*
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;

namespace ProverStore.Api.Controllers
{
    [Route("api/carrinho")]
    public class CarrinhoController : MainController
    {
        private readonly IShoppingCartRepository _carrinhoRepository;
        private readonly IMapper _mapper;
        public CarrinhoController(IShoppingCartRepository carrinhoRepository, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _carrinhoRepository = carrinhoRepository;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoItemVM>> AdicionarAoCarrinho(CarrinhoItemVM carrinhoVM)
        {
            if (carrinhoVM == null) CustomResponse();

            await _carrinhoRepository.AddAoCarrinho(carrinhoVM.CarrinhoId);

            return CustomResponse(carrinhoVM);
        }
        [HttpPut]
        public async Task<ActionResult<CarrinhoItemVM>> Decrementar(CarrinhoItemVM carrinhoVM)
        {
            if (carrinhoVM == null) CustomResponse();

            await _carrinhoRepository.Decrementar(carrinhoVM.CarrinhoId);

            return CustomResponse(carrinhoVM);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrinhoItemVM>>> GetCarrinhoLista()
        {
            var listaCarrinho = await _carrinhoRepository.ObterListaCarrinho();

            if (listaCarrinho == null) return CustomResponse();
           
            var listaMapeada = _mapper.Map<IEnumerable<CarrinhoItemVM>>(listaCarrinho);

            return CustomResponse(listaMapeada);
        }
    }
}

*/