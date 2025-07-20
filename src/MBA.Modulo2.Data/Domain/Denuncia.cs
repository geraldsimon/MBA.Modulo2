using MBA.Modulo2.Data.Domain.Enums;
using MBA.Modulo2.Data.Domain.Extensions;
using MBA.Modulo2.Data.Models;

using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Data.Domain;

public class Denuncia
{
	public Guid Id { get; set; } = string.Empty.NewUuidV7();

	[Required][StringLength(500)] public string Motivo { get; set; } = null!;

	[Required] public TipoDenuncia Tipo { get; set; }

	public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

	public StatusDenuncia Status { get; set; } = StatusDenuncia.Pendente;

	public DateTime? DataResolucao { get; set; }

	[StringLength(1000)] public string? ObservacaoAdmin { get; set; }

	public Guid ProdutoId { get; set; }
	public virtual Produto Produto { get; set; } = null!;

	public Guid DenuncianteId { get; set; }
	public ApplicationUser Denunciante { get; set; } = null!;

	public Guid? AdminResponsavelId { get; set; }
	public virtual ApplicationUser? AdminResponsavel { get; set; }
}