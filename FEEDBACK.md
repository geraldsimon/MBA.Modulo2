# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Possui views e rotas definidas usando ASP.NET Core MVC
    - Implementação com Razor Pages/Views
    - Interface web estruturada para gestão de produtos e categorias
 
### Design
    - Será avaliado na entrega final
 
### Funcionalidade
  * Pontos positivos:
    - Implementação completa do CRUD de Produtos e Categorias
    - Sistema de autenticação implementado com Identity
    - Diferenciação entre usuários comuns e logados
 
## Back End
### Arquitetura
  * Pontos positivos:
    - Arquitetura em camadas bem definida (App, Api, Business e Data)
    - Separação clara entre regras de negócio (Business) e acesso a dados (Data)
    - Compartilhamento adequado de camadas entre MVC e API

  * Pontos negativos:
    - ApplicationUser com dependencias de Identity na camada de negócios, além de acoplamento é desnecessário
 
### Funcionalidade
  * Pontos positivos:
    - Implementação completa da API RESTful
    - Uso do Entity Framework Core com SQL Server/SQLite
    - Implementação de autenticação JWT para API
    - Seed de dados automático na inicialização
    - Funcionalidades extras na API (Posts e Comentários)
    - AddDatabaseSelector foi uma ótima sacada.

  * Pontos negativos:
    - Nenhum ponto negativo identificado
 
### Modelagem
  * Pontos positivos:
    - Modelagem de dados bem estruturada
    - Uso apropriado do Entity Framework Core
    - Separação clara entre camadas de dados e negócio

  * Pontos negativos:
    - Uso de DataAnnotations em entidades
 
## Projeto
### Organização
  * Pontos positivos:
    - Projeto bem organizado com pasta src na raiz
    - Solução (.sln) na raiz do projeto
    - Separação clara em projetos: App, Api, Business e Data    

  * Pontos negativos:
    - Nenhum ponto negativo identificado na organização
 
### Documentação
  * Pontos positivos:
    - README.md detalhado com todas as instruções necessárias
    - Swagger implementado para documentação da API
    - Documentação dos usuários de teste
    - Instruções claras de configuração e execução

  * Pontos negativos:
    - Não foram identificados pontos negativos na documentação
 
### Instalação
  * Pontos positivos:
    - Configuração do SQLite com instruções claras
    - Seed de dados automático na inicialização
    - Migrations configuradas

  * Pontos negativos:
    - Não foram identificados pontos negativos