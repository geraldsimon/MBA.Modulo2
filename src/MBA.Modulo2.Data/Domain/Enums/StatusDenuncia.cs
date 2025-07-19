using System.ComponentModel;

namespace MBA.Modulo2.Data.Domain.Enums;

public enum StatusDenuncia
{
	[Description("Pendente")] Pendente = 1,
	[Description("Em Análise")] EmAnalise = 2,
	[Description("Aprovada")] Aprovada = 3,
	[Description("Rejeitada")] Rejeitada = 4
}