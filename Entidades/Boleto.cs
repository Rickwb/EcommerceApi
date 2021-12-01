using System;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Entidades
{
    public class Boleto:FormaPagamento
    {
        public Boleto(decimal valor) : base(valor)
        {
            TipoPagamento = Epagamento.Boleto;
        }

        public DateTime Vencimento { get; set; }
        public decimal Multa { get; set; }
        public string Benefinciario { get; set; }
        public string NumDocumento { get; set; }
        public string Agencia { get; set; }
        public string NumBanco { get; set; }

    }
}
