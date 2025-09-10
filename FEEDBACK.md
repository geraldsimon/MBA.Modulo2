# Feedback - Avaliação Geral

## Funcionalidade 30%

Avalie se o projeto atende a todos os requisitos funcionais definidos.

* Consistência no idioma da interface, ou faz-se tudo em inglês ou tudo em português.
* A applicação deu erro ao clicar em "Denunciar Produto" na detalhe de produtos.
* Após cadastrar e ativar um produto, o mesmo não aparece em "Vitrine de Produtos".
* Nenhum produto pode ser listado no Admin ou via Loja, mesmo após cadastrar um novo produto.
* Apenas o caso de use 4 pôde ser testado, os demais não funcionaram.

## Qualidade do Código 20%

Considere clareza, organização, uso de padrões de codificação.

### Data
* `UuidExtensions.NewUuidV7()` aparemente faz o mesmo que `Guid.Parse()`.
* Em `ProdutoRepository`, fazer melhor uso de sobrecarga de métodos. Ex:
```csharp
Task<Produtop[]> Listar(Guid? vendedorId = null, Guid categoriaId = null);
Task<Produto> Obter(Guid produtoId, Guid? sellerId = null);
Task<bool> Existe(Guid produtoId);
```
* Em `ProdutoRepository.PegarPorIsAsync()` não faz o que o nome do método sugere.
* O método `ProdutoRepository.DetalheProduto()`:
  * não possui um nome que segue convenção. Sugestão `ObterDetalhesProduto()`.
  * retorna resultados diferences dependendo do estado persistido do produto. Isso não é esperado de um método de repositório.
* A abstração `Repository<T>` possui métodos públicos que permitem modificar o estado da entidade, como `Add`, `Update` e `Remove` sem ser pela classe especializada.
* Em `DenunciaRepository`:
  - o método `UsuarioJaDenunciouProduto()` sugere que o retorno é um booleano, mas na verdade retorna uma ID de entidade.
  - A linha `var produto = await _produtoRepository.PegaPorIdAsync(denuncia.ProdutoId);` é confusa pois aparemente o método returna Produto, quando na verdade retorna um booleano.

### Business

* Em `ClienteService`, o método `Exclui()` sempre retorna `true`. Não é claro o motivo de retornar um `bool`.
* Em `ComentarioService`, no método `Alterar()`:
  - as variáveis são chamadas de `post` ao invés de `comentario`.
  - recomenda-se usar _null propagation_ `??` ao invés de `if`. Ex: `var comentario = _repo.Get(id) ?? throw new Exception();`
  - retorna `ArgumentoException`, mas não há um erro de argumento, uso indevido do tipo de exceção.
* Em `ComentarioService`, no método `Excluir()`, retorna uma exceção caso o comentario não exista, mas isso quebra sua idempotência.

### Api

* Existe um `ApplicationDbContext` em ./Data`, mas não é usado.
* Em vários _controllers_ o método `Update()` possui regras de negócio (aka `if`) que deveria estar na camada `Business`.

### Geral
* Namespace devem acompanhar o caminho físico do arquivo. Ex. `./src/MBA.Modulo2.Data/Domain/Categoria.cs` deve ser `namespace MBA.Modulo2.Data.Domain;`.
* Remover `usings` não utilizados.
* Evitar comentários desnecessários.
* Remover códigos comentados.
* Várias classes usam construtor primário e possuem campos privados desnecessários.
* Métodos devem começar com verbo no imperativo.

## Eficiência e Desempenho 20%

Avalie o desempenho e a eficiência das soluções implementadas.

* Existem algumas chamadas ao banco de dados que podem ser otimizadas, como mencionado na seção de Qualidade do Código.

## Inovação e Diferenciais

10% Considere a criatividade e inovação na solução proposta.

* A solução é sólida e atende bem aos requisitos, mas não apresenta inovações ou diferenciais significativos além do esperado.

## Documentação e Organização 10%

Verifique a qualidade e completude da documentação, incluindo README.md.

* Existe uma pasta órfã `./src/TemplateEngineHost` que não faz parte do projeto.
* O Markdown do README.md está inválido e mal formatado.
* Menciona _Internet Explorer_ sendo que IE sequer é distribuído.
* O documento pede para clonar um repositório diferente do compartilhado para avaliação.
* O documento menciona `cd DeveloperStore` mas o repositório clonado é `MBA.Modulo2`.
* Em "Configuração do Banco de Dados":
  - Menciona `appsettings.json` mas não menciona de qual projeto.
  - Não diz como "rodar" o projeto, tentei `dotnet run` em `MBA.Modulo2.Data` mas sem sucesso.
  - Consegui fazer migrations via projeto `API`
* Ao executar `dotnet run`:
  - em `DeveloperStore.App`, iniciou no endereço e porta: http://localhost:5297, diferente do mencionado no README.md.
  - em `DeveloperStore.Api`, iniciou no endereço e porta: http://localhost:5296, diferente do mencionado no README.md.
  - em `DeveloperStore.SPA`, iniciou no endereço e porta: http://localhost:5245, diferente do mencionado no README.md.
  - Tive que executar usando `dotnet run --launch-profile https` para conseguir iniciar o projetos usando HTTPS nas portas corretas.
* Em "Documentação da API", o endereço da documentação via Swagger está incorreto. Deveria ser https://localhost:7015/index.html

## Resolução de Feedbacks 10%

Avalie a resolução dos problemas apontados na primeira avaliação de feedback.
* Alguns comentários do feedback anterior foram endereçados, mas muitos permanecem sem resolução.
  - Um produto pode ser desabilitado por um admin, não existe um método para isso, logo entende-se que fica a cargo da atualização do produto, mas não existe validação de "dono" do produto abrindo uma brecha para apenas o admin ou o "dono" editar.
  - Documentação do README desatualizada
  - Mescla de Ingles e Portugues (classes, metodos)

## Notas

| Critério                     | Peso | Nota | Nota Ponderada |
|------------------------------|------|-----:|---------------:|
| Funcionalidade               | 30%  |    5 |            1.5 |
| Qualidade do Código          | 20%  |    7 |            1.4 |
| Eficiência e Desempenho      | 20%  |    9 |            1.8 |
| Inovação e Diferenciais      | 10%  |    8 |            0.8 |
| Documentação e Organização   | 10%  |    5 |            0.5 |
| Resolução de Feedbacks       | 10%  |    5 |            0.5 |
| **Total**                    |      |      |        **6.5** |
