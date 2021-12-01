using System;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Entidades
{
    public class CartaoDebito:FormaPagamento
    {
        public CartaoDebito(decimal valor) : base(valor)
        {
            TipoPagamento = Epagamento.CartaoDebito;
        }

        public string NomeCartao { get; set; }
        public string NumCartao { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public int CVV { get; set; }
        public decimal Limite { get; set; }
        public DateTime DataValidade { get; set; }
        public string NumBanco { get; set; }
        public string MarcaCartao { get; set; }
        public decimal Saldo { get; set; }
    }
}
