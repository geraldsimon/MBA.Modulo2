using MBA.Modulo2.Data.Domain.Extensions;

using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Api.ViewModels.Denuncia;

public class DenunciaViewModel
{
	public Guid Id { get; set; }

	[Required(ErrorMessage = "O motivo da denúncia é obrigatório")]
	[StringLength(500, ErrorMessage = "O motivo deve ter no máximo 500 caracteres")]
	public string Motivo { get; set; } = null!;

	[Required(ErrorMessage = "O tipo da denúncia é obrigatório")]
	public TipoDenunciaViewModel Tipo { get; set; }

	public DateTime DataCriacao { get; set; }

	public StatusDenunciaViewModel Status { get; set; }

	public DateTime? DataResolucao { get; set; }

	public string ObservacaoAdmin { get; set; }

	[Required(ErrorMessage = "O ID do produto é obrigatório")]
	public Guid ProdutoId { get; set; }

	public string ProdutoNome { get; set; }

	public Guid DenuncianteId { get; set; }

	public string DenuncianteEmail { get; set; }

	public Guid? AdminResponsavelId { get; set; }

	public string AdminResponsavelEmail { get; set; }

	public static implicit operator DenunciaViewModel(Modulo2.Data.Domain.Denuncia denuncia)
	{
		return new DenunciaViewModel
		{
			Id = denuncia.Id,
			Motivo = denuncia.Motivo,
			Tipo = new TipoDenunciaViewModel((short)denuncia.Tipo, denuncia.Tipo.ToDescription()),
			DataCriacao = denuncia.DataCriacao,
			Status = new StatusDenunciaViewModel((short)denuncia.Status, denuncia.Status.ToDescription()),
			DataResolucao = denuncia.DataResolucao,
			ObservacaoAdmin = denuncia.ObservacaoAdmin ?? string.Empty,
			ProdutoId = denuncia.ProdutoId,
			ProdutoNome = denuncia.Produto?.Nome ?? string.Empty,
			DenuncianteId = denuncia.DenuncianteId,
			DenuncianteEmail = denuncia.Denunciante?.Email ?? string.Empty,
			AdminResponsavelId = denuncia.AdminResponsavelId,
			AdminResponsavelEmail = denuncia.AdminResponsavel?.Email ?? string.Empty
		};
	}
}