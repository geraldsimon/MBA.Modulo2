# ** DeveloperStore - Aplicação de  Gestão de Mini Loja Virtual Simples com MVC e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **DeveloperStore**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo principal desenvolver uma aplicação de Gestão de Mini Loja Virtual com Cadastro de Produtos e Categorias
que permite aos usuários criar, editar, visualizar e excluir Produtos e categorias, tanto através de uma interface Web utilizando MVC quanto através de uma API RESTful.

A Aplicação MVC e API RESTful compartilha da camada de business, camada de dados e consequenemente a base de dados.


### **Autor(es)**
- **Geraldo Alves Simao Junior**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com a Loja Virtual.
- **API RESTful:** Exposição dos recursos da Loja Virtual para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando usuários logados e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

- src/
  - DeveloperStore.App/ - Projeto MVC
  - DeveloperStore.Api/ - API RESTful
  - DeveloperStore.Business/ - Regras de negócio 
  - DeveloperStore.Data/ - Modelos de Dados e Configuração do EF Core
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **CRUD para Produtos e categorias:** Permite criar, editar, visualizar e excluir proddutos e categorias.
- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários, "somente na Web Api".
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e usuários logados.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

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

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

 https://localhost:7015/swagger/index.html
 
 ## **9. Testes**

  - ** O projeto apresenta geração automática de dados quando a aplicação Web ASP.NET MVC é iniciada, com o Data Seed.
- Usuario = joaomelo@gmail.com
- Senha = Teste@123

  - ** Para testes na aplicação API o prjeto conta com arquivos de configuração do Postman, que podem ser importados para facilitar os testes.
- O arquivo `DeveloperStore.postman_collection.json` contém a coleção de requisições para a API.
- O arquivo `DeveloperStore.postman_environment.json` contém o ambiente de configuração para o Postman. 

## **10. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.