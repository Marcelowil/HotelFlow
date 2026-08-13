using System.Text.RegularExpressions;

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
            ValidarTelefone(telefone);

            Id = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            Email = email;
            Telefone = telefone;
        }

        public static Hospede Registrar(string nome, string documento, string email, string telefone)
        {
            telefone = Regex.Replace(telefone, @"\D", "");

            return new Hospede(nome, documento, email, telefone);
        }

        public void AtualizarDados(string nome, string email, string telefone)
        {
            ValidarNome(nome);
            ValidarEmail(email);
            ValidarTelefone(telefone);

            Nome = nome;
            Telefone = Regex.Replace(telefone, @"\D", "");
            Email = email;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Digite um valor válido para nome");
        }

        private void ValidarDocumento(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                throw new ArgumentException("Digite um valor válido para documento");
        }

        private void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Digite um valor válido para email");
        }

        private void ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone) || (telefone.Length < 10 && telefone.Length > 11))
                throw new ArgumentException("Digite um valor válido para telefone");
        }
    }
}
