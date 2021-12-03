using System;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.DTOs
{
    public class FinalizarPagamentoDTO
    {
        public Epagamento FormaPagamento { get; set; }
        public PixDTO? Pix { get; set; }
        public CartaoCreditoDTO? CartaoCredito { get; set; }
        public CartaoDebitoDTO? CartaoDebito { get; set; }
        public BoletoDTO? Boleto { get; set; }

        public decimal Valor { get; set; }
    }

    public class PixDTO
    {

        public Epagamento FormaPagamento { get; set; }
        public PixType TipoChave { get; set; }
        public string Chave { get; set; }
        public string Comentario { get; set; }
    }
    public class CartaoCreditoDTO
    {
        public Epagamento FormaPagamento { get; set; }
        public string NomeCartao { get; set; }
        public string NumCartao { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string CVV { get; set; }
        public string NumBanco { get; set; }
        public DateTime DataValidade { get; set; }
        public string MarcaCartao { get; set; }
        public decimal Limite { get; set; }

    }
    public class BoletoDTO
    {
        public Epagamento FormaPagamento { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Multa { get; set; }
        public string Benefinciario { get; set; }
        public string NumDocumento { get; set; }
        public string Agencia { get; set; }
        public string NumBanco { get; set; }
    }

    public class CartaoDebitoDTO
    {
        public Epagamento FormaPagamento { get; set; }
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
