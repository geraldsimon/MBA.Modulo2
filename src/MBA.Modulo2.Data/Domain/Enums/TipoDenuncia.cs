using System.ComponentModel;

namespace MBA.Modulo2.Data.Domain.Enums;

public enum TipoDenuncia
{
	[Description("Conteúdo Inadequado")] ConteudoInadequado = 1,
	[Description("Spam")] Spam = 2,
	[Description("Preço Abusivo")] PrecoAbusivo = 3,
	[Description("Produto Falso")] ProdutoFalso = 4,

	[Description("Violação dos Termos de Uso")]
	ViolacaoTermos = 5,
	[Description("Outro")] Outro = 6
}