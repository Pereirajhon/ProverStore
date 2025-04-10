namespace ProverStore.Api.ViewModel
{
    public class PedidoVM
    {
        public Guid ClienteId { get; set; }
        public List<PedidoItemVM> PedidoItems { get; set; }
        public DateTime DataDoPedido { get; set; }
        public double Total { get; set; }
        
    }
    
    public class PedidoItemVM
    {
        public Guid ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }

    } 
}
