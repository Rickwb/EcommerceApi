using EcommerceApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApi.Services
{
    public class ClienteService
    {

        private readonly List<Cliente> _clienteService;

        public ClienteService()
        {
            _clienteService ??= new List<Cliente>();
        }

        public Cliente Get(Guid id) => _clienteService.Where(c => c.ID == id).SingleOrDefault();
        public IEnumerable<Cliente> GetAll() => _clienteService;

        public Cliente Cadastrar(Cliente cli)
        {
            _clienteService.Add(cli);
            return cli;
        }

        public bool Deletar(Guid id)
        {
            var cliente = _clienteService.SingleOrDefault(c => c.ID == id);
            if (cliente == null) return false;
            _clienteService.Remove(cliente);
            return true;
        }

        public Cliente Atualizar(Guid id, Cliente cli)
        {
            var cliente = _clienteService.SingleOrDefault(c => c.ID == id);
            int index = _clienteService.IndexOf(cliente);

            _clienteService.RemoveAt(index);
            _clienteService.Insert(index, cliente);

            return cliente;
        }



    }
}
