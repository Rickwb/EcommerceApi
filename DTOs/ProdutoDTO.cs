using System;

namespace EcommerceApi.DTOs
{
    public class ProdutoDTO : Validator
    {
        public ProdutoDTO()
        {
        }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public override void Validar()
        {
            Valido = true;
            if (String.IsNullOrEmpty(Nome) || Nome.Length > 150)
                Valido = false;
            if (String.IsNullOrEmpty(Descricao) || Descricao.Length > 150)
                Valido = false;
            if (Preco <= 0)
                Valido = false;

        }
    }
}
