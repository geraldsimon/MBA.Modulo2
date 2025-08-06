# ** DeveloperStore - Aplicação de  Gestão de Mini Loja Virtual com MVC, API RESTful e SPA**

## **1. Apresentação**

Bem-vindo ao repositório do projeto DeveloperStore,esse projeto faz parte da entrega do MBA DevXpert Full Stack .NET, referente ao Módulo 2 – Desenvolvimento Full-Stack Avançado com ASP.NET Core.
O objetivo é dar continuidade ao sistema desenvolvido no Módulo 01, evoluindo de uma simples plataforma de gestão de produtos para uma loja virtual completa.
Nesta nova versão, foram implementadas funcionalidades que não estavam presentes anteriormente, tais como:
Cadastro e login de vendedores;
Cadastro e login de clientes;
CRUD de categorias e produtos com vínculo entre eles;
Visualização pública de produtos;
Lista de favoritos para clientes autenticados;
Painel de moderação para administradores.

Com isso, é possível realizar o controle completo (CRUD) de vendedores, clientes, produtos e categorias, utilizando três aplicações distintas:
-  1º MVC (Model-View-Controller): Responsável pelo cadastro/login de vendedores, gerenciamento de produtos e categorias, além do painel administrativo.
-  2º API RESTful: Responsável pela centralização das regras de negócio e comunicação entre as aplicações.
-  3º SPA (Single Page Application): Desenvolvida com Blazor, essa aplicação é voltada ao cliente final, permitindo navegação pública e funcionalidades autenticadas (como favoritar produtos).

A aplicação MVC e a API RESTful compartilham a mesma camada de negócio, camada de dados e base de dados, já a aplicação SPA se comunica exclusivamente com a API via requisições HTTP, utilizando autenticação JWT para operações seguras.


### **Autor(es)**
- **Alberto Luis Tarastchuck Borges**
- **Diego Lobo**
- **Douglas dos Santos Costa**
- **Geraldo Alves Simao Junior**
- **José Ricardo de Castro** 
- **Leonardo da Silva Rocha**
- **Silvio Cesar Kinaake**

## **2. Proposta do Projeto**

O projeto DeveloperStore tem como proposta principal a construção de uma plataforma completa, com foco em modularidade, segurança e boa experiência do usuário. 
com o objetivo de continuar evoluindo sistema de gestão de produtos desenvolvido no Módulo 01, agora expandido para incluir funcionalidades voltadas ao cliente final e à gestão multiusuário.

Escopo Funcional:
- **Área Administrativa: Para vendedores e administradores realizarem o cadastro e gerenciamento de produtos e categorias.
- **Loja Virtual: Para navegação pública de produtos, com recursos como login de cliente e lista de favoritos.
- **API RESTful: Centraliza as regras de negócio e permite a comunicação entre as aplicações.

Benefícios Técnicos:
- **Separação clara de camadas e responsabilidades.
- **Aplicação de conceitos modernos de autenticação e autorização.
- **Adoção de padrões REST e documentação via Swagger.
- **Utilização de ferramentas e frameworks atualizados (.NET 8, Blazor, EF Core).
- **Banco de dados inicializado automaticamente para facilitar o setup em ambientes de desenvolvimento.



## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:**
  - SQL Server
  - SQLite (para rapidas execução em testes)
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** 
  - Swagger
- **SPA:** 
  - Blazor

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

- src/
  - MBA.Modulo2.App/ - Projeto MVC
  - MBA.Modulo2.Api/ - API RESTful
  - MBA.Modulo2.Business/ - Regras de negócio 
  - MBA.Modulo2.Data/ - Modelos de Dados e Configuração do EF Core
  - MBA.Modulo2.SPA/ - Projeto SPA
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

 - **CRUD Área Administrativa (MVC):**
  -- CRUD de Produtos e Categorias: Permite criação, edição, visualização e inativação de produtos (pelo vendedor) e categorias (apenas pelo administrador).
  -- Gerenciamento de Vendedores: Cadastro e autenticação de vendedores com ASP.NET Identity.
  -- Painel de Moderação: Acesso exclusivo para administradores, com gerenciamento de vendedores e produtos.
  -- Autenticação via Cookies: Controle de acesso ao backoffice garantindo navegação restrita apenas a usuários autenticados.
  
 - ** API RESTful (Web API)
  -- Exposição de Endpoints: Operações CRUD para produtos, categorias, clientes, vendedores e lista de favoritos.
  -- Autenticação via JWT: Proteção de rotas com tokens JWT, garantindo segurança nas interações com a SPA.
  -- Documentação com Swagger: Documentação automática da API com suporte a autenticação JWT.
  -- Regras de Validação: Implementação de validações como nome obrigatório, preço positivo, estoque não negativo, imagem obrigatória e associação a categorias.

- ** Loja Virtual (SPA - Blazor)
  -- Navegação Pública: Exibição de produtos ativos disponível para qualquer visitante, sem necessidade de login.
  -- Cadastro e Login de Cliente: Registro e autenticação de clientes diretamente na SPA, utilizando JWT.
  -- Visualização de Detalhes: Acesso aos detalhes do produto, informações do vendedor e lista de produtos ativos do mesmo vendedor.
  -- Lista de Favoritos: Clientes autenticados podem adicionar e remover produtos dos favoritos, com persistência no banco de dados.
  -- Integração com API RESTful: Toda a comunicação com o back-end é feita por meio de requisições HTTP via API, utilizando Blazor WebAssembly.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server (A aplicação está configurada para usar SQLite automaticamente em ambiente de desenvolvimento)
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git
- Navegador moderno:
  Qualquer navegador atualizado como Microsoft Edge, Google Chrome, Brave, Firefox, ETC.
  Observação: O Internet Explorer não é compatível com aplicações Blazor.


### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/geraldsimon/DeveloperStore.git`
   - `cd DeveloperStore`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a Aplicação MVC:**
   - `cd src/DeveloperStore.App/`
   - `dotnet run`
   - Acesse a aplicação em: https://localhost:7009

4. **Executar a API:**
   - `cd src/DeveloperStore.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7015
 
 4. **Executar a SPA:**
   - `cd src/DeveloperStore.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7096

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

 https://localhost:7015/swagger/index.html
 
 ## **9. Testes**

  - ** O projeto apresenta geração automática de dados quando a aplicação Web ASP.NET MVC é iniciada, com o Data Seed.
 
- Usuario Comum = joaomelo@gmail.com
- Senha = Teste@123

- Usuario Admin = Admin@gmail.com
- Senha = Teste@123

-   - ** Para testes na aplicação API o prjeto conta com arquivos de configuração do Postman, que podem ser importados para facilitar os testes.
- O arquivo `DeveloperStore.postman_collection.json` contém a coleção de requisições para a API.
- O arquivo `DeveloperStore.postman_environment.json` contém o ambiente de configuração para o Postman. 

## **10. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.