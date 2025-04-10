using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ProverStore.Api.ViewModel
{
    public class JwtSettings
    {
        public string? Emissor { get; set; }
        public string? Segredo { get; set; }
        public string? Audiencia { get; set; }
        public int ExpiracaoHoras { get; set; }
    }
}
