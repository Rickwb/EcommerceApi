using EcommerceApi.Entidades;
using System;

namespace EcommerceApi.DTOs
{
    public class ItemPedidoDTO : Validator
    {
        public Guid? Id { get; set; }
        public PedidoDTO Pedido { get; set; }
        public ProdutoDTO Produto { get; set; }
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
