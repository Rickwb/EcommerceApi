namespace EcommerceApi.Enums
{
    public class Enums
    {
        public enum ETipoPessoa
        {
            PessoaFisica = 1,
            PessoaJuridica = 2
        }
        public enum Epagamento
        {
            Pix,
            Boleto,
            CartaoDebito,
            CartaoCredito,
        }
        public enum PixType
        {
            Email,
            Telefone,
            Cpf,
            ChaveAleatoria,
            QrCode
        }
    }
}