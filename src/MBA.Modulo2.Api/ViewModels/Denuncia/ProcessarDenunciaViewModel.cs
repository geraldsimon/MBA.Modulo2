using System.ComponentModel.DataAnnotations;
using MBA.Modulo2.Data.Domain.Enums;

namespace MBA.Modulo2.Api.ViewModels.Denuncia;

public class ProcessarDenunciaViewModel
{
	[Required(ErrorMessage = "O novo status é obrigatório")]
	public StatusDenuncia NovoStatus { get; set; }

	[StringLength(1000, ErrorMessage = "A observação deve ter no máximo 1000 caracteres")]
	public string ObservacaoAdmin { get; set; }
}