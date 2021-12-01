using System;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Entidades
{
    public class Cliente : EntidadeBase
    {
        public Cliente(Guid id,
                       string nome,
                       string sobrenome,
                       string documento,
                       int idade,
                       ETipoPessoa e) : base(id)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Idade = idade;
            TipoPessoa = e;
        }
        public string Nome { get;private  set; }
        public string Sobrenome { get;private  set; }
        public string Documento { get;private set; }
        public int Idade { get;private set; }
        public ETipoPessoa TipoPessoa { get;private  set; }
    }
}
