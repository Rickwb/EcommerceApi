using EcommerceApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApi.Services
{
    public class PedidoService
    {
        private readonly List<Pedido> _pedidoService;

        public Entidades.Pedido Get(Guid id) => _pedidoService.Where(c => c.ID == id).SingleOrDefault();
        public IEnumerable<Pedido> GetAll() => _pedidoService;

        public Pedido Cadastrar(Pedido cli)
        {
            _pedidoService.Add(cli);
            return cli;
        }

        public bool Deletar(Guid id)
        {
            var cliente = _pedidoService.SingleOrDefault(c => c.ID == id);
            if (cliente == null) return false;
            _pedidoService.Remove(cliente);
            return true;
        }

        public Pedido Atualizar(Guid id, Pedido cli)
        {
            var cliente = _pedidoService.SingleOrDefault(c => c.ID == id);
            int index = _pedidoService.IndexOf(cliente);

            _pedidoService.RemoveAt(index);
            _pedidoService.Insert(index, cliente);

            return cliente;
        }
        public ItemPedido AdicionarItem(ItemPedido item)
        {
            item.Pedido.ItensPedido.Add(item);
            return item;
        }
        public List<ItemPedido> BuscarItensPedidos(Guid idPedido)
        {
            var pe = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            if (pe is not null) return pe.ItensPedido;
            return null;
        }
        //public void RemoverItemPedido(ItemPedido item, Pedido pe) => pe.ItensPedido.Remove(item);
        public bool RemoverItemPedido(ItemPedido item, Guid idPedido)
        {
            var pe = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            int qtd= pe.ItensPedido.Count();
            if (pe is not null)
            {
                pe.ItensPedido.Remove(item);
                return qtd>pe.ItensPedido.Count()?true:false;
            }
            return false;
        }
        public bool EsvaziarCarrinho(Guid idPedido)
        {
            var pedido = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            int index = _pedidoService.IndexOf(pedido);
            pedido.ItensPedido.Clear();
            if (pedido.ItensPedido.Count == 0)
            {
                _pedidoService[index] = pedido;
                return true;
            }
            else
            {
                return false;
            }
        }

        public ItemPedido AtualizarItemPedido(Guid id, ItemPedido itemPedido)
        {
            var item = itemPedido.Pedido.ItensPedido.SingleOrDefault(p => p.ID == id);
            int index = itemPedido.Pedido.ItensPedido.IndexOf(item);
            itemPedido.Pedido.ItensPedido[index] = itemPedido;
            return itemPedido;
        }
    }
}
