using System.ComponentModel.DataAnnotations;
using MBA.Modulo2.Data.Domain.Enums;

namespace MBA.Modulo2.App.ViewModels;

public class DenunciaViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O motivo da denúncia é obrigatório")]
    [StringLength(500, ErrorMessage = "O motivo deve ter no máximo 500 caracteres")]
    [Display(Name = "Motivo da Denúncia")]
    public string Motivo { get; set; } = null!;

    [Required(ErrorMessage = "O tipo da denúncia é obrigatório")]
    [Display(Name = "Tipo de Denúncia")]
    public TipoDenuncia Tipo { get; set; }

    [Display(Name = "Data da Denúncia")]
    public DateTime DataCriacao { get; set; }

    [Display(Name = "Status")]
    public StatusDenuncia Status { get; set; }

    [Display(Name = "Data de Resolução")]
    public DateTime? DataResolucao { get; set; }

    [Display(Name = "Observação do Administrador")]
    public string? ObservacaoAdmin { get; set; }

    public Guid ProdutoId { get; set; }

    [Display(Name = "Produto")]
    public string? ProdutoNome { get; set; }

    public Guid DenuncianteId { get; set; }

    [Display(Name = "Denunciante")]
    public string? DenuncianteEmail { get; set; }

    public Guid? AdminResponsavelId { get; set; }

    [Display(Name = "Admin Responsável")]
    public string? AdminResponsavelEmail { get; set; }
}

public class CriarDenunciaViewModel
{
    [Required(ErrorMessage = "O motivo da denúncia é obrigatório")]
    [StringLength(500, ErrorMessage = "O motivo deve ter no máximo 500 caracteres")]
    [Display(Name = "Motivo da Denúncia")]
    public string Motivo { get; set; } = null!;

    [Required(ErrorMessage = "O tipo da denúncia é obrigatório")]
    [Display(Name = "Tipo de Denúncia")]
    public TipoDenuncia Tipo { get; set; }

    public Guid ProdutoId { get; set; }

    public string? ProdutoNome { get; set; }
}

public class ProcessarDenunciaViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O novo status é obrigatório")]
    [Display(Name = "Novo Status")]
    public StatusDenuncia NovoStatus { get; set; }

    [StringLength(1000, ErrorMessage = "A observação deve ter no máximo 1000 caracteres")]
    [Display(Name = "Observação")]
    public string? ObservacaoAdmin { get; set; }

    public DenunciaViewModel? Denuncia { get; set; }
}