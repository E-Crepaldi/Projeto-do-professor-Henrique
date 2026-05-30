using System.ComponentModel.DataAnnotations;
namespace Cine99.Models {
public class Filme {
public int Id { get; set; }
[Required(ErrorMessage="O nome é obrigatório")]
public string Nome { get; set; } = string.Empty;
[Required(ErrorMessage="A sinopse é obrigatória")]
public string Sinopse { get; set; } = string.Empty;
[Required] public string Diretor { get; set; } = string.Empty;
[Range(1888,2100)] public int AnoLancamento { get; set; }
public string? ImagemUrl { get; set; }
public string? CriticaEditor { get; set; }
public ICollection<Avaliacao> Avaliacoes { get; set; }
= new List<Avaliacao>();
}
}