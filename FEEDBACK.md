# Feedback - Avaliação Geral

## Front End

### Navegação
- Será avaliado na entrega final

### Design
- Será avaliado na entrega final

### Funcionalidade
- Será avaliado na entrega final

## Back End

### Arquitetura
  * Pontos positivos:
    - Projeto de arquitetura enxuto e coeso    
 
### Funcionalidade
  * Pontos positivos:
    - Os cases de negócio que foram implementados até o momento estão dentro do padrão esperado, porém faltam mais comportamentos.

  * Pontos negativos:
    - O registro de usuário (AspNetUser) não cria a entidade de negócio.
    - Assim como o vendedor, o cliente também é uma entidade de negócios e precisa possuir um AspNetUser e um registro na tabela Cliente  
    - Um produto pode ser desabilitado por um admin, não existe um método para isso, logo entende-se que fica a cargo da atualização do produto, mas não existe validação de "dono" do produto abrindo uma brecha para apenas o admin ou o "dono" editar.

### Modelagem
  * Pontos negativos:
    - Modelagem de domínio dentro de Data sendo que existe camada Business
    - Modelagem de ApplicationUser em Data (nem Data nem Business)

## Projeto

### Organização
  * Pontos positivos:
    - Organização de pastas e arquivos

  * Pontos negativos:
    - Documentação do README desatualizada
    - Mescla de Ingles e Portugues (classes, metodos)

### Documentação
- Será avaliado na entrega final
 
### Instalação
  * Pontos positivos:
    - Uso de SQLite e Seed de dados

  * Pontos negativos:
    - Seed de dados apenas no MVC 
