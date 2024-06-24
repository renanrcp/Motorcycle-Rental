
# Motorcycle-Rental

## Introdução

Esse é um software que tem a ideia de ser um gerenciador de locação de motos.

## Domínio

### Banco De Dados

O projeto usa PostgreSQL com a estrutura abaixo:
![Banco Motorcycle](/assets/db.png)

O projeto também utiliza MongoDB para salvar eventos de Domínio e Aplicação por fora.

### Projetos

O projeto utiliza DDD de domínio rico, Clean Architecture, Microsserviços e EF Core:
![Estrutura Projetos](/assets/project_structure.png)

- Como curiosidade o projeto não segue 90% dos projetos `Clean .NET` que recria a `camada de Repository`, uma vez que o `EF Core` já implementa esse Pattern e o `Unit Of Work`, esse projeto convida qualquer experiente no `.NET` a ver uma diferente implementação de queries compartilhadas sem perder o poder de controle oferecido pelo `EF Core`.

## Testes
O projeto não possui todos os testes implementados mas há espaço para implementar todos quase que tranquilamente, seguindo apenas o padrão já existente.

Você pode rodar os testes existentes usando o comando `dotnet test`.

## Rodando o projeto
- Clone esse repositório.
- Atenção aos scripts existentes na pasta `scripts`.
- Para cada script `.sh` existe um script `.bat` equivalente.
- O projeto foi desenvolvido no Ubuntu 22.04, e por isso pode ter uma compatibilidade melhor (bem como a sua compatibilidade com Docker).
- Para usuários de VS Code o arquivo `MotorcycleRental.code-workspace` é recomendado para abrir o projeto.
- O projeto possui vários .csproj em várias pastas, por isso é possível que, dependendo de onde o repositório foi clonado você tenha que modificar o limite de caracteres de path do Windows.

### Requisitos

- Docker
- .NET 8
- As seguintes portas tem que estar livres: 80, 5432, 27017, 5000, 5001, 5002 e 5003

### Setup

Para começar o projeto é necessário você rodar o script `setup.sh`, ele criará todos os volumes de docker externos necessários, e também instalará a tool global do EF Core.

### Build

A partir daqui você tem 2 opções, seguir usando docker totalmente (modo Release), ou apenas os bancos de dados e migrations.

- Rode o script `build.sh` caso queira usar o projeto pronto.
- Rode o script `build-migrations.sh` caso queira rodar apenas os bancos de dados e as migrations.

### Rodando

Ao rodar o script `up.sh` você terá todos os serviços rodando e acessíveis pela porta 80, onde teremos os mesmos separados por pathing.

Caso você queria fazer debugging de um dos projetos, ou rodá-los manualmente você pode utilizar o script `up-migrations.sh` e depois debuggar manualmente ou usar o comando `dotnet run` na pasta `Presentation` do projeto desejado.

OBS: Alguns projetos podem ter outros serviços como dependências:

- Deliverers
  - Users
- Motorcycles
  - Rentals
- Rentals
  - Deliverers
  - Motorcycles

Ao terminar de rodar as coisas você deve, rodar o comando `down.sh` ou `down-migrations.sh`.

### Paths (produção)
Cada serviço está disponível em seu respectivo pathing, `localhost/users` para o serviço de `users`, por exemplo.

### Logs
Ao rodar os projetos no modo release os logs ficarão disponíveis em `/logs/{service}`, `/logs/users/` por exemplo.

Caso você os rode manualmente o pathing será apenas `/logs` dentro do projeto de `Presentation` do serviço escolhido.

### Documentação (Swagger)
A documentação swagger (tanto em produção como manual) fica disponível sempre no pathing `{service}/swagger/index.html`, `deliverers/swagger/index.html`, por exemplo.

### Imagens
Ao rodar os projetos no modo release as imagens ficarão disponíveis em `/images/`, enquanto manualmente, ficará em `/images/` dentro do projeto de `Presentation` do serviço de `Deliverers`.

### Login
O banco de dados vem com um usuário `admin` (todas as permissões) criado.

O login é feito na rota POST `/users/login/` e receberá o seguinte body:
```json
{
  "email": "email@dominio",
  "password": "senha"
}
```

para o usuário `Admin` o login é o seguinte:
```json
{
  "email": "admin@motorcyclerental.com",
  "password": "admin"
}
```

Um `JWT Bearer Token` será gerado para ser usado no header dar requests no formato padrão (e.g `Bearer TOKEN1223424242424242`).

Vale lembrar que não é possível usar o usuário admin no serviço de `Rentals` (exceção ao endpoint de checagem de moto).

### Usuário do tipo `entregador`
Usuários do tipo `entregadores` podem ser criados pelo serviço de `Deliverers` no Endpoint `POST /delivers`.

## Tratamento de Erros
Todas as exceptions são salvas no `MongoDB` como `application_events` (cada serviço tem sua própria `collection`).

Erros esperados pela aplicação são todos respondidos de forma padronizada:
```json
{
  "event_id": "string?", // id do evento (disponível quando uma exception foi gerada, facilitando os devs a encontrarem os erros).
  "code": "string", // disponível em todos os erros, um código de erro da aplicação que indica qual foi o erro que ocorreu.
  "error": "string?", // disponível em quase todos os erros, com exceção da maioria dos BadRequests (400).
  "validationErrors": "Dictionary<string,string[]>?" // Lista de erros de validação, disponível apenas quando o Status Code for 400.
  // A chave do objeto se refere a qual propriedade dentro do corpo da request teve um erro de validação.
  // Os valores são um array de string com as mensagens de erro emitidas por essa propriedade.
  // Caso a propriedade possua um "$" significa que o corpo foi inserializável também.
  // Caso a propriedade possua um "@" significa que uma validação ocorreu e ela não tem propriedades para indicar.
}
```

## Criando novas migrations
O projeto `src/Core/MotorcycleRental.Core.Migrator` reune a lógica de criação de migrações de todos os serviços em uma única base, caso você faça qualquer alteração na estrutura do banco basta rodar `dotnet ef migrations add NomeDaMigration` que as alterações serão geradas automaticamente.

Para rodar as migrações é necessário derrubar todos os serviços, rebuildar e rodar; `down.sh`, `build.sh` e `up.sh` para produção ou `down-migrations.sh`, `build-migrations.sh` e `up-migrations.sh` para manual.


## Requisitos que esse projeto deve/deveria seguir
Os requisitos originais estão disponíveis [nesse link](Requisitos.md).
