# GeoApi - API de Georreferenciamento

API para gerenciamento de estados e cidades com suas coordenadas geográficas desenvolvida em ASP.NET Core.

## Requisitos

- .NET 9.0 SDK
- SQL Server
- Entity Framework Core CLI (`dotnet-ef`)

## Configuração

### 1. Connection String

Antes de executar o projeto, configure a string de conexão no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=GeoApiDb;Integrated Security=SSPI;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

Substitua `SEU_SERVIDOR` pelo nome da sua instância do SQL Server.

### 2. Migrações do Banco de Dados

É necessário executar as migrações para criar o banco de dados antes de iniciar a aplicação:

```bash
# Instalar a ferramenta do Entity Framework (caso não tenha)
dotnet tool install --global dotnet-ef

# Criar o banco de dados
dotnet ef database update
```

## Executando o Projeto

Para iniciar a API:

```bash
dotnet run
```

Por padrão, a API estará disponível em:
- https://localhost:5001
- http://localhost:5000

A documentação da API (Swagger) estará disponível em:
- https://localhost:5001/swagger

## Endpoints da API

### Estados

- **POST /api/Geo** - Adiciona um novo estado com suas cidades
- **GET /api/Geo/{statePostalCode}** - Obtém um estado pelo código postal e suas cidades

### Cidades

- **GET /api/Geo/{statePostalCode}/{cityName}** - Obtém uma cidade específica pelo código postal do estado e nome da cidade

## Estrutura de Dados

### Adicionar Estado (POST)

```json
{
  "statePostalCode": "MA",
  "name": "Massachusetts",
  "capital": "Boston",
  "cities": [
    {
      "city": "Worcester",
      "longitude": 40.07386811,
      "latitude": -92.97242767
    },
    {
      "city": "Acton",
      "longitude": 40.07386811,
      "latitude": -92.97242767
    }
  ]
}
```

## Respostas

### Sucesso
Em caso de sucesso, a API retorna diretamente os dados solicitados com status 200 (OK).

### Erro
Em caso de erro, a API retorna uma resposta no formato:

```json
{
  "error": true,
  "message": "Mensagem detalhada do erro"
}
```

Com os códigos de status apropriados (400 Bad Request ou 404 Not Found). 