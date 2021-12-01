using EcommerceApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApi.Services
{
    public class ProdutoService
    {
        private readonly List<Produto> _produtoService;

        public ProdutoService() 
        {
            _produtoService ??= new List<Produto>();
        }

        public Produto Cadastrar(Produto prop)
        {
            _produtoService.Add(prop);
            return prop;
        }

        public List<Produto> GetAll() => _produtoService;

        public Produto GetProduto(Guid id) => _produtoService.Where(p => p.ID == id).SingleOrDefault();

        public void Deletar(Guid id)
        {
            var prod=_produtoService.Where(p => p.ID == id).SingleOrDefault();
            _produtoService.Remove(prod);
        }
        public Produto Atualizar(Guid id,Produto prop)
        {
            var prod=_produtoService.Where(p => p.ID == id).SingleOrDefault();
            int index = _produtoService.IndexOf(prop);
            _produtoService.Remove(prod);
            _produtoService.Insert(index, prop);
            return prop;
        }

    }
}
