# Winter Olympics Management System

**Système complet de gestion des Jeux Olympiques d'hiver en C#**

---

## 📋 Description

Ce projet est un système complet de gestion des Jeux Olympiques d'hiver permettant de gérer tous les aspects d'un événement olympique :

- Création et gestion de l'événement olympique
- Gestion des sports et disciplines
- Inscription des pays et athlètes
- Organisation des compétitions
- Calcul automatique des résultats et classements
- Attribution des médailles
- Tableau des médailles en temps réel
- Calendrier des événements

---

## 🏗️ Architecture

Le projet est structuré en trois parties principales :

### **TournamentManager.Models**

Contient tous les modèles de données :

- **Entités principales** : `WinterOlympics`, `Sport`, `Discipline`, `Venue`
- **Participants** : `Athlete`, `NationalTeam`
- **Compétitions** : `Event`, `Result`, `MedalStandings`
- **Énumérations** : `CompetitionType`, `MedalType`, `EventStatus`, `Gender`

### **TournamentManager.Services**

Contient la logique métier avec 6 services spécialisés :

- **OlympicGameService** : Gestion globale des Jeux Olympiques
- **SportService** : Gestion des sports et disciplines
- **CompetitionService** : Organisation et gestion des événements
- **ScoringService** : Calcul des scores et classements
- **ScheduleService** : Gestion du calendrier
- **MedalStandingsService** : Tableau des médailles

### **TournamentManager.App**

Application console démontrant toutes les fonctionnalités du système.

---

## ✨ Fonctionnalités principales

### 🏅 Gestion des Jeux Olympiques

- Création d'événements olympiques avec dates, lieu hôte, devise
- Ajout de sports et disciplines
- Inscription de pays participants
- Programmation d'événements de compétition

### 🎿 Types de compétitions supportés

- **Chronométrées** (Timed) : Ski alpin, patinage de vitesse, bobsleigh

  - Meilleur temps gagne
  - Gestion des pénalités de temps
  - Support de plusieurs manches

- **Par points/score** (Scored) : Patinage artistique, saut à ski, snowboard freestyle

  - Notes de jury avec élimination des extrêmes
  - Calcul de score final
  - Pénalités de points

- **En face-à-face** (HeadToHead) : Hockey sur glace, curling
  - Matchs avec scores
  - Système de tournoi

### 🏆 Gestion des médailles

- Attribution automatique des médailles (or, argent, bronze)
- Tableau des médailles en temps réel
- Classement des pays par nombre de médailles d'or, puis argent, puis bronze
- Statistiques par sport et par pays

### 📅 Calendrier

- Organisation des événements par date et heure
- Vérification des conflits d'horaires
- Affichage du calendrier quotidien et complet
- Gestion des reports et annulations

### 📊 Résultats et statistiques

- Enregistrement des performances (temps ou scores)
- Calcul automatique des classements
- Affichage détaillé des résultats
- Gestion des disqualifications
- Historique complet des athlètes

---

## 🎯 Exemple d'utilisation

```csharp
// 1. Initialiser les services
var olympicService = new OlympicGameService();
var sportService = new SportService();
var competitionService = new CompetitionService(new ScoringService());

// 2. Créer les Jeux Olympiques
olympicService.CreateWinterOlympics(
    "Beijing 2026",
    "Beijing",
    "China",
    new DateTime(2026, 2, 4),
    new DateTime(2026, 2, 20),
    "Together for a Shared Future"
);

// 3. Créer un sport et une discipline
var alpineSkiing = sportService.CreateSport("Ski Alpin", CompetitionType.Timed);
var menDownhill = sportService.CreateDiscipline("Descente Hommes", alpineSkiing.Id, Gender.Male);

// 4. Inscrire un pays et un athlète
var france = new NationalTeam(1, "France", "FRA", "🇫🇷");
var athlete = new Athlete(1, "Alexis", "Pinturault", 33, "France", "FRA", Gender.Male, 1);
france.AddAthlete(athlete);
olympicService.AddCountry(1, france);

// 5. Créer un événement
var venue = new Venue(1, "Alpine Centre", "Yanqing", 5000, "Alpine Skiing");
var event = competitionService.CreateEvent(
    "Descente Hommes - Finale",
    menDownhill.Id,
    new DateTime(2026, 2, 6),
    new TimeSpan(10, 0, 0),
    venue
);

// 6. Enregistrer une performance
competitionService.RegisterParticipant(event, athlete);
competitionService.RecordTimedPerformance(event, athlete, TimeSpan.FromSeconds(95.23));

// 7. Finaliser et afficher les résultats
competitionService.FinalizeEvent(event, menDownhill);
competitionService.DisplayEventResults(event);

// 8. Afficher le tableau des médailles
var medalService = new MedalStandingsService();
medalService.DisplayStandings(olympics);
```

---

## 🚀 Comment exécuter le projet

### Prérequis

- .NET 6.0 SDK ou supérieur

### Installation et exécution

1. **Cloner le dépôt**

   ```bash
   git clone <repository-url>
   cd TournamentManager
   ```

2. **Compiler le projet**

   ```bash
   dotnet build
   ```

3. **Exécuter l'application**
   ```bash
   dotnet run --project TournamentManager.App
   ```

---

## 📦 Structure du projet

```
TournamentManager/
├── TournamentManager.Models/
│   ├── Enums/
│   │   ├── CompetitionType.cs
│   │   ├── MedalType.cs
│   │   ├── EventStatus.cs
│   │   └── Gender.cs
│   ├── WinterOlympics.cs
│   ├── Sport.cs
│   ├── Discipline.cs
│   ├── Venue.cs
│   ├── Athlete.cs
│   ├── NationalTeam.cs
│   ├── Event.cs
│   ├── Result.cs
│   └── MedalStandings.cs
├── TournamentManager.Services/
│   ├── IOlympicGameService.cs / OlympicGameService.cs
│   ├── ISportService.cs / SportService.cs
│   ├── ICompetitionService.cs / CompetitionService.cs
│   ├── IScoringService.cs / ScoringService.cs
│   ├── IScheduleService.cs / ScheduleService.cs
│   └── IMedalStandingsService.cs / MedalStandingsService.cs
└── TournamentManager.App/
    └── Program.cs
```

---

## 🎨 Fonctionnalités avancées

### Gestion des scores avec jury

Le système supporte le calcul de scores avec jury :

- Élimination automatique des notes extrêmes (plus haute et plus basse)
- Calcul de la moyenne des notes restantes
- Support de pénalités de points

### Système de pénalités

- Pénalités de temps pour les sports chronométrés
- Pénalités de points pour les sports notés
- Disqualifications avec raison

### Validation des données

- Vérification de la compatibilité athlète/discipline (genre, sport)
- Vérification des conflits d'horaires des venues
- Validation des performances (temps/scores réalistes)

### Affichages riches

- Tableaux formatés avec bordures Unicode
- Drapeaux et médailles
- Codes couleur et symboles pour les statuts
- Affichage hiérarchique des informations

---

## 🔧 Extensibilité

Le système est conçu pour être facilement extensible :

- **Ajouter un nouveau sport** : Créer via `SportService.CreateSport()`
- **Ajouter une discipline** : Créer via `SportService.CreateDiscipline()`
- **Nouveau type de compétition** : Ajouter à l'enum `CompetitionType` et adapter `ScoringService`
- **Nouveaux calculs** : Étendre `ScoringService` avec de nouvelles méthodes
- **Nouvelles statistiques** : Ajouter des méthodes à `MedalStandingsService`

---

## 🧪 Tests

Le système inclut :

- Validation des entrées à tous les niveaux
- Gestion complète des erreurs
- Messages d'information clairs pour chaque opération
- Exemple complet dans `Program.cs` démontrant toutes les fonctionnalités

---

## 📝 Principes de conception

- **Separation of Concerns** : Modèles, services et application séparés
- **Dependency Injection** : Utilisation d'interfaces pour tous les services
- **Single Responsibility** : Chaque service a une responsabilité claire
- **Open/Closed Principle** : Facile à étendre sans modifier le code existant
- **Encapsulation** : Logique métier encapsulée dans les services

---

## 🌟 Exemple de sortie

Lorsque vous exécutez le programme, vous verrez :

```
╔════════════════════════════════════════════════════════════╗
║        SYSTÈME DE GESTION DES JEUX OLYMPIQUES D'HIVER      ║
╚════════════════════════════════════════════════════════════╝

━━━ CRÉATION DES JEUX OLYMPIQUES ━━━
✓ Jeux Olympiques créés: Beijing 2026 - Beijing, China (04/02/2026 - 20/02/2026)

━━━ CRÉATION DES SPORTS ━━━
✓ Sport créé: Ski Alpin (Timed)
✓ Discipline créée: Descente Hommes (Male)
...

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
                    RÉSULTATS DES COMPÉTITIONS
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

╔════════════════════════════════════════════════════════════╗
║  Descente Hommes - Finale                                  ║
╚════════════════════════════════════════════════════════════╝

--- RÉSULTATS ---
1. Aksel Svindal (NOR) - 01:34.85 🥇
2. Alexis Pinturault (FRA) - 01:35.23 🥈

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
                   TABLEAU DES MÉDAILLES
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

╔════════════════════════════════════════════════════════════╗
║              TABLEAU DES MÉDAILLES OLYMPIQUES              ║
╚════════════════════════════════════════════════════════════╝

Rang  Pays                        Or Argent Bronze  Total
-----------------------------------------------------------------
1     🇳🇴 Norway                    1      0      0      1
2     🇺🇸 United States             1      0      0      1
3     🇷🇺 Russia                    1      0      0      1
4     🇫🇷 France                    0      1      0      1
...
```

---

## 📄 Licence

Ce projet est un exemple éducatif de système de gestion des Jeux Olympiques d'hiver.

---

## 👥 Contribution

Les contributions sont les bienvenues ! N'hésitez pas à :

- Ajouter de nouveaux sports
- Améliorer les calculs de scores
- Ajouter de nouvelles statistiques
- Optimiser les performances
- Corriger les bugs

---

## 🎓 Apprentissage

Ce projet démontre :

- ✅ Architecture en couches (Models, Services, App)
- ✅ Utilisation d'interfaces et injection de dépendances
- ✅ Énumérations pour les types
- ✅ Gestion de collections complexes
- ✅ Calculs métier avancés
- ✅ Formatage et affichage de données
- ✅ Principes SOLID
- ✅ Séparation des responsabilités

---

**Bon développement ! 🚀**
