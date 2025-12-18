using TechFutureCRMAPI.Models; // ⬅️ DEVE ESTAR AQUI
namespace TechFutureCRMAPI.Models // Deve ser este namespace
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string Status { get; set; } = "Ativo";
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}