namespace catalogo_api.DTOs
{
    //retorna quando autentica o token
    public class UsuarioToken
    {
        public bool Authenticated { get; set; }

        public DateTime Expiration { get; set; }

        public string? Token { get; set; }

        public string? Message { get; set; }

    }
}
