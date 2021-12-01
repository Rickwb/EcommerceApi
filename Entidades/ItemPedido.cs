using System;

namespace EcommerceApi.Entidades
{
    public class ItemPedido:EntidadeBase
    {
        public ItemPedido(Guid id,Pedido pedido,Produto produto,int qtd):base(id)
        {
            Produto = produto;
            QtdProdutos = qtd;
            Pedido = pedido;
        }
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
        public int QtdProdutos { get; set; }
    }
}
