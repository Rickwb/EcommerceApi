using System;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Entidades
{
    public class Cliente : EntidadeBase
    {
        public Cliente() { }
        public Cliente(Guid id,
                       string nome,
                       string sobrenome,
                       string documento,
                       int idade,
                       ETipoPessoa e,
                       Pedido pedido) : base(id)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Pedido = pedido;
            Idade = idade;
            TipoPessoa = e;
        }
        public Cliente(Guid id,
                       string nome,
                       string sobrenome,
                       string documento,
                       int idade,
                       ETipoPessoa e
                       ) : base(id)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Pedido ??= new Pedido();
            Idade = idade;
            TipoPessoa = e;
        }
        public string Nome { get;private  set; }
        public string Sobrenome { get;private  set; }
        public Pedido Pedido { get;private set; }
        public string Documento { get;private set; }
        public int Idade { get;private set; }
        public ETipoPessoa TipoPessoa { get;private  set; }
    }
}
