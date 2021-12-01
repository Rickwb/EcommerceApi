using EcommerceApi.Entidades;

namespace EcommerceApi.DTOs
{
    public class ItemPedidoDTO : Validator
    {
        public Pedido Pedido { get; set; }  
        public Produto Produto { get; set; }
        public int QtdProdutos { get; set; }
        public override void Validar()
        {
            Valido = true;
            if (Produto is null) Valido = false;
            if (Pedido is null) Valido = false;
            if (QtdProdutos <= 0) Valido = false;

        }
    }
}
