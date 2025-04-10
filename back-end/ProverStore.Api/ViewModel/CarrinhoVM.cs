using System.Transactions;

namespace ProverStore.Api.ViewModel
{
    public class CarrinhoVM
    {
        public Guid ClienteId { get; set; }
        public Guid CarrinhoId { get; set; }
        public List<CarrinhoItemVM>? CarrinhoItemsVM { get; set; }   
        public double Total { get; set; }
    }

    public class CarrinhoItemVM
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public double TotalProduto { get; set; }
    }
}
