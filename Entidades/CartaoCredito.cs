using System;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Entidades
{
    public class CartaoCredito : FormaPagamento
    {
        public CartaoCredito(decimal valor, DateTime date, string numCartao, string Conta, string Agencia, string Marca, string CVV) : base(valor)
        {
            TipoPagamento = Epagamento.CartaoCredito;
        }

        public string NomeCartao { get; set; }
        public string NumCartao { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public int CVV { get; set; }
        public string NumBanco { get; set; }
        public DateTime DataValidade { get; set; }
        public string MarcaCartao { get; set; }
        public decimal Limite { get; set; }
        
    }
}
