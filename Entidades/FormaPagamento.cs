using System;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Entidades
{
    public abstract class FormaPagamento
    {
        public FormaPagamento(decimal valor)
        {
            Valor = valor;
        }
        protected decimal Valor { get; private set; }
        protected DateTime DataPagamento { get; private set; }

        protected bool PagamentoConcluido { get; private set; }
        public bool Valido { get; protected set; }

        public Epagamento TipoPagamento { get; init; }

       
    }
}
