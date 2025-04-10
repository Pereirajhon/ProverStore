using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Business.Services;
using ProverStore.Data.Repository;

namespace ProverStore.Api.Controllers
{     
    [Route("api/produtos")]
    public class ProdutoController: MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoController(INotificador notificador,

            IProdutoRepository produtoRepository,
            IProdutoService produtoService,
            IMapper mapper) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GetProdutoVM>> GetProdutos()
        {
            var listaProduto = await _produtoRepository.ObterTodos();

            return _mapper.Map<IEnumerable<Produto>,IEnumerable<GetProdutoVM>>(listaProduto);

        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetProdutoVM>> GetProdutoPorId(Guid id)
        {
            var produtoRepo = await _produtoRepository.ObterPorId(id);

            if (produtoRepo == null) NotFound();

            var produtoViewModel = _mapper.Map<GetProdutoVM>(produtoRepo);

            return produtoViewModel;

        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProdutoVM>>> SearchProdutos([FromQuery] string query)
        {
            if(String.IsNullOrEmpty(query)) return CustomResponse();

            var produtoQuery = await _produtoRepository.BuscarPorFiltro(query);
            if (produtoQuery == null) return CustomResponse();

            return CustomResponse(_mapper.Map<IEnumerable<GetProdutoVM>>(produtoQuery));
        }

        [Authorize(Roles ="ADMIN")]
        [HttpPost]
        public async Task<IActionResult> PostProduto(ProdutoVM produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var produtoMappeado = _mapper.Map<Produto>(produtoViewModel);
            await _produtoService.Adicionar(produtoMappeado);
            return CustomResponse(produtoViewModel);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateProdutoVM>> UpdateProduto(Guid id, UpdateProdutoVM produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(produtoViewModel);
            }

            if (!ModelState.IsValid) CustomResponse(ModelState);


            var produtoMapeado = _mapper.Map<Produto>(produtoViewModel);

            await _produtoService.Atualizar(produtoMapeado);
            return CustomResponse(produtoViewModel);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoVM>> DeleteProduto(Guid id)
        {
            var produtoMapeado = _mapper.Map<ProdutoVM>(await _produtoRepository.ObterPorId(id));

            if (produtoMapeado == null)
            {
                return NotFound();
            }

            await _produtoService.Deletar(id);

            return CustomResponse(produtoMapeado);
        }

        private async Task<bool> UploadArquivo(FormFile arquivo, string imgNome)
        {
            if (arquivo == null || arquivo.Length <= 0)
            {
                NotificarErro("Forneça uma imagem para este produto!");
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgNome + arquivo.FileName);

            if (System.IO.File.Exists(arquivo.FileName))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            //System.IO.File.WriteAllBytes(arquivo, imageDataByteArray);

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
