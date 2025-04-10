using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using System.Data;

namespace ProverStore.Api.Controllers
{
    [Authorize]
    [Route("api/favoritos")]
    public class FavoritoController : MainController
    {
        private readonly IFavoritoRepository _favoritoRepository;
        private readonly IMapper _mapper;
        private readonly IProdutoService _produtoService;
        private readonly IUnitOfWork _uow;
        public FavoritoController(INotificador notificador, IFavoritoRepository favoritoRepository, IProdutoService produtoService, IMapper mapper, IUnitOfWork uow) : base(notificador)
        {
            _produtoService = produtoService;
            _favoritoRepository = favoritoRepository;
            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet("{clienteId:guid}")]
        public async Task<ActionResult<IEnumerable<ProdutoVM>>> GetProdutosFavoritadosPorUsuario(Guid clienteId)
        {

            var favoritadosPorUsuario = await _favoritoRepository.ObterProdutoFavoritadoUsuario(clienteId);

            if (favoritadosPorUsuario == null) return CustomResponse();

            return CustomResponse(_mapper.Map<IEnumerable<ProdutoVM>>(favoritadosPorUsuario));
        }

        [HttpPost]
        public async Task<ActionResult> FavoritarProduto(FavoritoVM favorito)
        {
            var favoritadoVm = new Favorito
            {
                ClienteId = favorito.ClienteId,
                ProdutoId = favorito.ProdutoId
            };

            var favoritado = await _favoritoRepository.ObterProdutoFavoritadoUsuario(favorito.ClienteId);

            if (favoritado.Any(p => p.Id == favorito.ProdutoId)) return CustomResponse();

            await _favoritoRepository.Favoritar(favoritadoVm);

            var p = await _produtoService.ObterPorId(favorito.ProdutoId);
            if (p == null) return NotFound();

            p.Favoritados += 1;
            await _produtoService.Atualizar(p);

            await _uow.Commit();
            return Ok();
        }

        [HttpDelete("{clienteId:guid}")]
        public async Task<ActionResult> DesfavoritarProduto(FavoritoVM favorito)
        {
            var favoritado = new Favorito
            {
                ClienteId = favorito.ClienteId,
                ProdutoId = favorito.ProdutoId
            };

            try
            {
                if (favoritado == null) return NotFound();

                await _favoritoRepository.DesFavoritar(favoritado);

                var p = await _produtoService.ObterPorId(favoritado.ProdutoId);
                if (p == null || p.Favoritados < 1) return NotFound();

                p.Favoritados -= 1;

                await _produtoService.Atualizar(p);

                await _uow.Commit();
                return Ok();
            }
            catch (DBConcurrencyException ex)
            {
                await _uow.Rollback();
                return BadRequest(ex.Message);
            }
        }
        
    }
}