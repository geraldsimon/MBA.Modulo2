# Feedback - Avaliação Geral

## Funcionalidade 30%

Avalie se o projeto atende a todos os requisitos funcionais definidos.

* Em Backoffice:
    * Consistência no idioma da interface. Ou faz tudo em inglês ou tudo em português.
    * A aplicação dá erro ao clicar em "Denunciar Produto" na detalhe de produtos.
        * `A database operation failed while processing the request. SqliteException: SQLite Error 1: 'no such table: Denuncias'.`
    * Como Admin, em Vendedores, ao clicar em Editar Produtos, sempre lista produtos do primeiro vendedor.
    * Loguei com um Vendedor desativado e pude Ativar produtos. Os produtos corretamente não aparecem na Vitrine.

* Em Loja:
    * Não faz nada ao clicar em "Denunciar Produto" na detalhe de produtos.
    * Mensagem de erro ao adicionar um produto ao favorito (HTTP 403 Forbidden).

* Demais funcionalidades funcionaram corretamente.

## Qualidade do Código 20%

Considere clareza, organização, uso de padrões de codificação.

### Data
* `UuidExtensions.NewUuidV7()` aparemente faz o mesmo que `Guid.Parse()`.
* Em `ProdutoRepository.PegarPorIsAsync()` não faz o que o nome do método sugere.
* O método `ProdutoRepository.DetalheProduto()`:
    * não possui um nome que segue convenção. Sugestão `ObterDetalhesProduto()`.
    * retorna resultados diferentes dependendo do estado persistido do produto. Isso não é esperado de um método de repositório.
* A abstração `Repository<T>` possui métodos públicos que permitem modificar o estado da entidade, como `Add`, `Update` e `Remove` sem ser pela classe especializada.
* Em `DenunciaRepository`:
    - o método `UsuarioJaDenunciouProduto()` sugere que o retorno é um booleano, mas na verdade retorna uma ID de entidade. Métodos booleanos devem ser nomeados com prefixos como `Esta`, `Tem/Possui`, `Pode`, `Deve`, etc.
    - A linha `var produto = await _produtoRepository.PegaPorIdAsync(denuncia.ProdutoId);` é confusa pois aparemente o método returna Produto, quando na verdade retorna um booleano, mas nem o nome do método sugesre isso.

### Business

* Em `ClienteService`, o método `Exclui()` sempre retorna `true`. Não é claro o motivo de retornar um `bool`.
* Em `ComentarioService`, no método `Alterar()`:
    - as variáveis são chamadas de `post` ao invés de `comentario`.
    - recomenda-se usar _null propagation_ `??` ao invés de `if`. Ex: `var comentario = _repo.Get(id) ?? throw new Exception();`
    - retorna `ArgumentException`, mas não há um erro de argumento, uso indevido do tipo de exceção.
* Em `ComentarioService`, no método `Excluir()`, retorna uma exceção caso o comentario não exista, mas isso quebra sua idempotência.

### Api

* Existe um `ApplicationDbContext` em ./Data`, mas não é usado.
* Em vários _controllers_ o método `Update()` possui regras de negócio (aka `if`) que deveria estar na camada `Business`.

### App
* As _controllers_ possui muitas regras de negócio (aka `if`) que deveria estar na camada `Business`.

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
* Em "Passos para Execução", ao executar `dotnet run`:
    - em `DeveloperStore.App`, iniciou no endereço e porta: http://localhost:5297, diferente do mencionado no README.md.
    - em `DeveloperStore.Api`, iniciou no endereço e porta: http://localhost:5296, diferente do mencionado no README.md.
    - em `DeveloperStore.SPA`, iniciou no endereço e porta: http://localhost:5245, diferente do mencionado no README.md.
    - Tive que executar usando `dotnet run --launch-profile https` para conseguir iniciar o projetos usando HTTPS nas portas corretas.
    - Em "Executar SPA", a documentação indica o mesmo caminho do projeto API.
* Em "Documentação da API", o endereço da documentação via Swagger está incorreto. Deveria ser https://localhost:7015/index.html

## Resolução de Feedbacks 10%

Avalie a resolução dos problemas apontados na primeira avaliação de feedback.
* Alguns comentários do feedback anterior foram endereçados, mas muitos permanecem sem resolução.
    - Documentação do README desatualizada
    - Mescla de Ingles e Portugues (classes, metodos)

## Notas

| Critério                     | Peso | Nota | Nota Ponderada |
|------------------------------|------|-----:|---------------:|
| Funcionalidade               | 30%  |    8 |            2.4 |
| Qualidade do Código          | 20%  |    7 |            1.4 |
| Eficiência e Desempenho      | 20%  |    9 |            1.8 |
| Inovação e Diferenciais      | 10%  |    8 |            0.8 |
| Documentação e Organização   | 10%  |    5 |            0.5 |
| Resolução de Feedbacks       | 10%  |    8 |            0.8 |
| **Total**                    |      |      |        **7.7** |