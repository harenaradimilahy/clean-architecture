# Clean Architecture Template

## 🚀 Démarrage du projet

Pour démarrer le projet, exécutez les commandes suivantes dans le terminal :

```bash
dotnet ef migrations add "NomDeVotreMigration"
dotnet ef database update
```
Ensuite, corrigez les fichiers de migration générés automatiquement si certains éléments ne sont pas compatibles avec Visual Studio, puis lancez le projet.

##  Architecture

- SharedKernel project with common Domain-Driven Design abstractions.
- Domain layer with sample entities.
- Application layer with abstractions for:
  - CQRS
  - Example use cases
  - Cross-cutting concerns (logging, validation)
- Infrastructure layer with:
  - EF Core, SqlServer
  - Serilog
- Testing projects
  - Architecture testing
