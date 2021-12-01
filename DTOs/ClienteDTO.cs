using EcommerceApi.Entidades;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.DTOs
{
    public class ClienteDTO : Validator
    {
        public ClienteDTO() { }


        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Documento { get; set; }
        public int Idade { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }
        public override void Validar()
        {
            Valido=true;
            if (string.IsNullOrEmpty(Nome) || Nome.Length > 150) Valido = false;
            if (string.IsNullOrEmpty(Sobrenome) || Sobrenome.Length > 150) Valido = false;

            if (TipoPessoa==ETipoPessoa.PessoaJuridica)
                Valido=ValidarCnpj(Documento);
            if (TipoPessoa==ETipoPessoa.PessoaFisica)
                Valido=ValidarCpf(Documento);
            if (Idade < 18) Valido = false;

        }
        public bool ValidarCpf(string documento)
        {
            return true;
        }
        public bool ValidarCnpj(string documento)
        {
            return true;
        }

    }
}
