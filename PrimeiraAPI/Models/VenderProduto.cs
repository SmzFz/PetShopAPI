namespace PrimeiraAPI.Models
{
    public class VenderProduto
    {
        public int VenderProdutoId { get; set; }

        public Guid VenderId { get; set; }
        public Vender? Vender { get; set; }

        public Guid ProdutoId { get; set; }

        public Produto? Produto { get; set; }
        public float? QuantidadeVenda { get; set; }
    }
}
