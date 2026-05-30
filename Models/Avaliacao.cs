using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cine99.Models {
public class Avaliacao {
public int Id { get; set; }
[Required][Range(1,10,ErrorMessage="Nota entre 1 e 10")]
public int Nota { get; set; }
public string? Comentario { get; set; }
public DateTime DataAvaliacao { get; set; } = DateTime.Now;
[Required] public int FilmeId { get; set; }
[ForeignKey("FilmeId")] public Filme? Filme { get; set; }
public string? UserId { get; set; }
}
}