using System.Text.RegularExpressions;
using HotelFlow.Domain.Exceptions;

namespace HotelFlow.Domain.Entities
{
    public class Hospede
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Documento { get; private set; }

        public string Email { get; private set; }
        public string Telefone { get; private set; }

        private Hospede(string nome, string documento, string email, string telefone)
        {
            ValidarNome(nome);
            ValidarDocumento(documento);
            ValidarEmail(email);

            Id = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            Email = email;
            Telefone = NormalizarEValidarTelefone(telefone);
        }

        public static Hospede Registrar(string nome, string documento, string email, string telefone)
        {
            return new Hospede(nome, documento, email, telefone);
        }

        public void AtualizarDados(string nome, string email, string telefone)
        {
            ValidarNome(nome);
            ValidarEmail(email);
            telefone = NormalizarEValidarTelefone(telefone);

            Nome = nome;
            Telefone = telefone;
            Email = email;
        }

        private string NormalizarEValidarTelefone(string telefone)
        {
            telefone = telefone != null ? RemoverMascaraTelefone(telefone) : "";
            ValidarTelefone(telefone);

            return telefone;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new HospedeException("Nome inválido");
        }

        private void ValidarDocumento(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                throw new HospedeException("Documento inválido");
        }

        private void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new HospedeException("Email inválido");
        }

        private void ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone) || (telefone.Length < 10 || telefone.Length > 11))
                throw new HospedeException("Telefone inválido");
        }

        private string RemoverMascaraTelefone(string telefone)
        {
            return Regex.Replace(telefone, @"\D", "");
        }
    }
}
