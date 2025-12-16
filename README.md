# Tournament Manager

**Projet C# pour la gestion de tournois sportifs.**

---

## Description
Ce projet permet de gérer des tournois, des équipes, des joueurs et des matchs. Il est structuré en trois parties :
- **TournamentManager.Models** : Contient les classes de données (Player, Team, Match, Tournament).
- **TournamentManager.Services** : Contient la logique métier (TournamentService).
- **TournamentManager.App** : Application console pour interagir avec le projet.

---

## Fonctionnalités
- Création et gestion de tournois.
- Ajout de matchs entre équipes.
- Calcul du gagnant d’un match.
- Affichage des résultats des tournois.

---

## Architecture
- Utilisation des interfaces pour une meilleure modularité.
- Injection de dépendances pour faciliter les tests et l’évolutivité.

---

## Prérequis
- .NET 6.0 SDK

---

## Comment exécuter le projet
1. Cloner le dépôt.
2. Ouvrir la solution dans Visual Studio ou via la ligne de commande.
3. Exécuter le projet `TournamentManager.App`.

---

## Exemple d’utilisation
```csharp
// Exemple dans Program.cs
ITournamentService tournamentService = new TournamentService();
tournamentService.CreateTournament("Tournament 2025");
// ... (voir Program.cs pour la suite)
