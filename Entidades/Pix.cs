using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Entidades
{
    public class Pix:FormaPagamento
    {
        public Pix(decimal valor,string chave) : base(valor)
        {
            TipoPagamento = Epagamento.Pix;
        }

        public PixType TipoChave { get; set; }
        public string Chave { get; set; }
        public string Comentario { get; set; }

    }
}
