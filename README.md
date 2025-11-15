# Backend - Guia de Execução

## Requisitos
- Visual Studio Code (VSCode)
- .NET SDK 9
- MySQL
- Git

## Clonar o repositório
git clone https://github.com/AndrePhilLeandro/Trabalho_Banco_de_Dados

## Restaurar dependências
dotnet restore

## Compilar
dotnet build
## Conferir String de Conexão
 "DefaultConnection": "server=localhost;port=3306;database=Faculdade;uid="Usuario";password="Senhaaqui"
 Coloque no uid o seu login do banco de dados
 Coloque no password o seu login do banco de dados


## Rodar migrations para o banco de dados
dotnet ef migrations Add Avaliacao --context AvaliacaoDb
dotnet ef migrations Add Curso --context CursoDb
dotnet ef migrations Add Disciplina --context DisciplinaDb
dotnet ef migrations Add Frequencia --context FrequenciaDb
dotnet ef migrations Add Nota --context NotaDb
dotnet ef migrations Add Turma --context TurmaDb
dotnet ef migrations Add Usuarios --context UsuarioDb

## Atualizar o banco de dados
dotnet ef database update --context AvaliacaoDb
dotnet ef database update --context CursoDb
dotnet ef database update --context DisciplinaDb
dotnet ef database update --context FrequenciaDb
dotnet ef database update --context NotaDb
dotnet ef database update --context TurmaDb
dotnet ef database update --context UsuarioDb

## Rodar o backend
dotnet watch run
## Documentação
Rotas do swagger deve aparecer no terminal: 
Now listening on: http://localhost:{porta}
