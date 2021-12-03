using System;

namespace EcommerceApi.Entidades
{
    public class ItemPedido:EntidadeBase
    {
        public ItemPedido(Guid id,Produto produto,int qtd):base(id)
        {
            Produto = produto;
            QtdProdutos = qtd;
            
        }
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
        public int QtdProdutos { get; set; }
    }
}
