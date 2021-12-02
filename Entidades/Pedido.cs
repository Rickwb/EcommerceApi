using System;
using System.Collections.Generic;

namespace EcommerceApi.Entidades
{
    public class Pedido : EntidadeBase
    {
        public Pedido()
        {
            
        }
        public Pedido(Guid id,string name, Cliente cliente,FormaPagamento formaPagamento) : base(id)
        {
            Nome = name;
            Cliente = cliente;
            ItensPedido ??= new List<ItemPedido>();
            FormaPagamento = formaPagamento;
        }
        public string Nome { get; set; }
        public List<ItemPedido> ItensPedido { get; set; }
        public FormaPagamento FormaPagamento { get; set;}
        public Cliente Cliente { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
