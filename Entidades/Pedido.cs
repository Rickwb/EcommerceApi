using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApi.Entidades
{
    public class Pedido : EntidadeBase
    {
        private List<ItemPedido> _itensPedidos;
        public Pedido()
        {

        }
        public Pedido(Guid id, string name, Cliente cliente) : base(id)
        {
            Nome = name;
            Cliente = cliente;
            _itensPedidos = new List<ItemPedido>();
        }
        public string Nome { get; set; }
        public IReadOnlyList<ItemPedido> ItensPedido => _itensPedidos;
        public FormaPagamento FormaPagamento { get; set; }
        public Cliente Cliente { get; set; }
        public decimal ValorTotal { get; set; }

        private bool Pago { get; set; }

        public void DefinirPago()
        {
            Pago = true;
        }
        public void AdicionarItemPedido(ItemPedido itemPedido)
        {
            _itensPedidos.Add(itemPedido);
        }
        public void Removerpedido(ItemPedido itemPedido)
        {
            _itensPedidos.Remove(itemPedido);
        }
        public void AtualizarItemPedido(Guid idItem,ItemPedido itemPedido)
        {
            var itemAntigo= _itensPedidos.Where(i=>i.ID==idItem).SingleOrDefault();
            int index = _itensPedidos.IndexOf(itemAntigo);
            _itensPedidos[index] = itemPedido;

        }
        public void RemoverTodosItens()
        {
            _itensPedidos.Clear();
        }
    }
}
