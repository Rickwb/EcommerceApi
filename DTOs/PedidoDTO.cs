using EcommerceApi.Entidades;
using System;
using System.Collections.Generic;
using EcommerceApi.Entidades;

namespace EcommerceApi.DTOs
{
    public class PedidoDTO : Validator
    {
        public string Nome { get; set; }
        public List<ItemPedido> ItensPedido { get; set; }
        public Cliente Cliente { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public override void Validar()
        {
            Valido = true;
            if (String.IsNullOrEmpty(Nome) || Nome.Length > 150)
                Valido = false;
            if (Cliente is null)
                Valido = false;
            if (ItensPedido.Count==0)
                Valido=false;


        }
    }
}
