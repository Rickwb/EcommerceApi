
using System;

namespace EcommerceApi.Entidades
{
    public class Produto:EntidadeBase
    {
        public Produto( Guid id,string nome, string descricao, decimal preco):base(id)
        {
            
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            DataCriacao=  DateTime.Now;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
