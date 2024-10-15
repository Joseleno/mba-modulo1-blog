# Feedback do Instrutor

#### 14/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Separação de responsabilidades.
- Controle satisfatório de usuários com autorização e roles.
- Demonstrou conhecimento em Identity e JWT.
- Bom uso de mapeador manual (no lugar do AutoMapper)
- Mostrou entendimento do ecossistema de desenvolvimento em .NET
- Documentou bem o repositório

## Pontos Negativos:

- Uso desnecessário da camada Utils, poderia estar dentro de infra ou negócio (a depender da responsabilidade)
- Não criou a entidade principal "Autor"
- Uso desnecessário de uma Domain layer:
    - A camada está super básica e as entidades anemicas
    - Utilizou DataAnnotations em entidades de domínio.
    - Nesse caso a complexidade da aplicação não pede uma camada de domínio, bastaria uma camada de aplicação com modelos atendendo via serviço as duas aplicações Web.
- Falhas de design:
    - "HasAthorization" não precisaria ler um repositório
    - "NotFoundException" é desnecessário, já existe tratamento nativo para esse cenário
    - Organização excessiva em "DependencjyInjection" utilizando métodos para resolução particionados
    - DTOs espalhadas por camadas diferentes

## Sugestões:

- Uma arquitetura mais coesa e simplificada faria mais sentido.
- Separar melhor responsabilidades de domain, infra e dados.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
