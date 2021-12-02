using EcommerceApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApi.Services
{
    public class ClienteService
    {

        private readonly List<Cliente> _clienteService;
        private readonly PedidoService _pedidoService;
        public ClienteService(PedidoService pedidoService)
        {
            _clienteService ??= new List<Cliente>();
            _pedidoService = pedidoService;
        }

        public Cliente Get(Guid id) => _clienteService.Where(c => c.ID == id).SingleOrDefault();
        public IEnumerable<Cliente> GetAll()
        {
            return _clienteService;
        }

        public Cliente Cadastrar(Cliente cli)
        {
            _clienteService.Add(cli);
            _pedidoService.Cadastrar(cli.Pedido);
            return cli;
        }

        public bool Deletar(Guid id)
        {
            var cliente = _clienteService.SingleOrDefault(c => c.ID == id);
            if (cliente == null) return false;
            _clienteService.Remove(cliente);
            _pedidoService.Deletar(cliente.Pedido.ID);
            return true;
        }

        public Cliente Atualizar(Guid id, Cliente cli)
        {
            var cliente = _clienteService.SingleOrDefault(c => c.ID == id);
            int index = _clienteService.IndexOf(cliente);

            if (cliente.Pedido.Equals(cli.Pedido))
                _pedidoService.Atualizar(cliente.Pedido.ID, cli.Pedido);

            _clienteService.RemoveAt(index);
            _clienteService.Insert(index, cliente);

            return cliente;
        }

        public bool Pagar(Pedido pedido)
        {
            return true;
        }
        public bool Pagar(Guid idPedido)
        {
            return true;
        }
    }
}
