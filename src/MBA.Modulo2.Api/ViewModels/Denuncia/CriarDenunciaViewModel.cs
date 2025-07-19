using MBA.Modulo2.Data.Domain.Enums;
using MBA.Modulo2.Data.Domain.Extensions;

using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Api.ViewModels.Denuncia;

public class CriarDenunciaViewModel
{
	private Guid _id;
	private Guid _denuncianteId;

	[Required(ErrorMessage = "O motivo da denúncia é obrigatório")]
	[StringLength(500, ErrorMessage = "O motivo deve ter no máximo 500 caracteres")]
	public string Motivo { get; set; } = null!;

	[Required(ErrorMessage = "O tipo da denúncia é obrigatório")]
	public TipoDenuncia Tipo { get; set; }

	[Required(ErrorMessage = "O ID do produto é obrigatório")]
	public Guid ProdutoId { get; set; }

	public CriarDenunciaViewModel SetDenuncianteId(Guid id)
	{
		_denuncianteId = id;
		return this;
	}

	public CriarDenunciaViewModel SetId(Guid id)
	{
		_id = id;
		return this;
	}

	public static implicit operator Modulo2.Data.Domain.Denuncia(CriarDenunciaViewModel viewModel)
	{
		return new Modulo2.Data.Domain.Denuncia
		{
			Motivo = viewModel.Motivo,
			Tipo = viewModel.Tipo,
			ProdutoId = viewModel.ProdutoId,
			DenuncianteId = viewModel._denuncianteId
		};
	}

	public static implicit operator DenunciaViewModel(CriarDenunciaViewModel viewModel)
	{
		return new DenunciaViewModel
		{
			Id = viewModel._id,
			Motivo = viewModel.Motivo,
			Tipo = new TipoDenunciaViewModel((short)viewModel.Tipo, viewModel.Tipo.ToDescription()),
			Status = new StatusDenunciaViewModel(
				(short)StatusDenuncia.EmAnalise,
				StatusDenuncia.EmAnalise.ToDescription()),
			ProdutoId = viewModel.ProdutoId,
			DenuncianteId = viewModel._denuncianteId
		};
	}
}