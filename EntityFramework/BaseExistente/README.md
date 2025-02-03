# EF - Conexão com bases existentes

A ideia deste projeto é mostrar como podemos conectar o Entity Framework com bases de dados existentes, sem a necessidade de criar um novo banco de dados.
Obs.: Projeto de exemplo é um console application minimalista.

### Arvore do projeto

```text
📂 MinimalApiProdutos
├── 📂 Application         # Camada de aplicação (Regras de negócio, DTOs e Services)
│   ├── 📂 DTOs
│   │   ├── 📄 CategoriaDTO.cs
│   │   ├── 📄 ProdutoDTO.cs
│   ├── 📂 Interfaces      # Interfaces dos serviços
│   │   ├── 📄 ICategoriaService.cs
│   │   ├── 📄 IProdutoService.cs
│   ├── 📂 Services        # Implementação dos serviços
│   │   ├── 📄 CategoriaService.cs
│   │   ├── 📄 ProdutoService.cs
│
├── 📂 Domain             # Entidades do domínio (Modelos do banco)
│   ├── 📂 Entities
│   │   ├── 📄 Categoria.cs
│   │   ├── 📄 Produto.cs
│
├── 📂 Infrastructure      # Camada de infraestrutura (Banco de dados e Repositórios)
│   ├── 📂 Data
│   │   ├── 📄 ApplicationDbContext.cs  # DbContext do EF Core
│   │   ├── 📂 Migrations/  # Diretório das migrações do EF Core
│   ├── 📂 Repositories     # Implementação dos Repositórios
│   │   ├── 📄 BaseRepository.cs
│   │   ├── 📄 CategoriaRepository.cs
│   │   ├── 📄 ProdutoRepository.cs
│
├── 📂 MinimalApiProdutos  # Camada da API (Ponto de entrada do sistema)
│   ├── 📄 Program.cs       # Arquivo principal da API
│   ├── 📄 appsettings.json # Configurações do banco e outros parâmetros
│   ├── 📄 appsettings.Development.json # Configuração para ambiente de desenvolvimento
│
└── 📄 MinimalApiProdutos.sln # Solução do Visual Studio

```

### Query 

```sql
CREATE TABLE Categorias (
    Id INT IDENTITY,
    Nome VARCHAR(100) NOT NULL,
	CONSTRAINT PK_Categorias_Id Primary Key(Id)
);

CREATE TABLE Produtos (
    Id INT IDENTITY,
    Nome VARCHAR(100) NOT NULL,
    CategoriaId INT NOT NULL,
    Quantidade INT NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
	DataCadastro DATETIME2 NOT NULL DEFAULT GETDATE(),
	DataDesativacao DATETIME2 NULL,
    DataEntrada DATETIME2 NULL,
	DataSaida DATETIME2 NULL,
	CONSTRAINT PK_Produtos_Id Primary Key(Id),
    CONSTRAINT FK_Produtos_Categorias FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id)
);

-- Inserir categorias
INSERT INTO Categorias (Nome) VALUES 
    ('Periféricos'),    -- ID 1
    ('Monitores'),      -- ID 2
    ('Áudio e Vídeo'),  -- ID 3
    ('Móveis'),         -- ID 4
    ('Notebooks'),      -- ID 5
    ('Hardware');       -- ID 6
```

### Pacotes Nuget

1. Instalar o Entity Framework Core

```bash
dotnet add package Microsoft.EntityFrameworkCore
```

2. Instalar o Provedor do SQL Server

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

3. Instalar as Ferramentas do EF Core (para rodar migrations)

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

4. Instalar a Injeção de Dependência (Microsoft Extensions)

```bash
dotnet add package Microsoft.Extensions.DependencyInjection
```

Comando EF para aplicar em caso de tabelas existentes na base de dados:

```bash
Scaffold-DbContext "Data Source=SeuHostSQLServer;Initial Catalog=EluminiCatalogo;Integrated Security=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Infrastructure/Data -Context ApplicationDbContext -ContextDir Infrastructure/Data -UseDatabaseNames -NoOnConfiguring -Force
```