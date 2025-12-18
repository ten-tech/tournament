# 📘 Guide d'Utilisation du Makefile

**Système de Gestion des Jeux Olympiques d'Hiver**

Ce document explique toutes les commandes disponibles dans le Makefile pour faciliter le développement et l'utilisation du projet.

---

## 🚀 Démarrage Rapide

### Première utilisation
```bash
# 1. Restaurer les dépendances
make restore

# 2. Compiler le projet
make build

# 3. Lancer l'application
make run
```

### Raccourcis pratiques
```bash
make r    # Lancer l'app (raccourci de 'make run')
make b    # Compiler (raccourci de 'make build')
make c    # Nettoyer (raccourci de 'make clean')
```

---

## 📚 Commandes Détaillées

### 🏗️ COMPILATION

#### `make restore`
**Description** : Restaure les packages NuGet nécessaires au projet.

**Quand l'utiliser** :
- Première fois que vous clonez le projet
- Après avoir ajouté de nouvelles dépendances
- Si les dépendances sont corrompues

**Exemple** :
```bash
make restore
```

**Sortie attendue** :
```
📦 Restauration des dépendances NuGet...
✓ Dépendances restaurées avec succès
```

---

#### `make build`
**Description** : Compile tout le projet en mode Debug.

**Quand l'utiliser** :
- Après avoir modifié du code
- Pour vérifier que le code compile sans erreur
- Avant de lancer l'application

**Exemple** :
```bash
make build
```

**Sortie attendue** :
```
🏗️  Compilation du projet en mode Debug...
✓ Compilation réussie
```

**Logique métier** :
- Compile avec symboles de débogage
- Optimisations désactivées pour faciliter le debug
- Idéal pour le développement

---

#### `make build-release`
**Description** : Compile le projet en mode Release (optimisé).

**Quand l'utiliser** :
- Avant de publier l'application
- Pour tester les performances en production
- Pour créer une version distribuable

**Exemple** :
```bash
make build-release
```

**Logique métier** :
- Optimisations du compilateur activées
- Pas de symboles de débogage
- Taille du binaire réduite

---

#### `make rebuild`
**Description** : Nettoie puis recompile entièrement le projet.

**Quand l'utiliser** :
- Après des changements structurels majeurs
- Si vous avez des erreurs de compilation bizarres
- Pour être sûr d'avoir un build propre

**Exemple** :
```bash
make rebuild
```

**Ce qui se passe** :
1. Exécute `make clean`
2. Exécute `make build`

---

### 🚀 EXÉCUTION

#### `make run`
**Description** : Lance l'application interactive.

**Quand l'utiliser** :
- Pour tester l'application
- Pour utiliser le système interactif
- Utilisation normale du programme

**Exemple** :
```bash
make run
```

**Ce qui se passe** :
```
🚀 Lancement de l'application interactive...

╔════════════════════════════════════════════════════════════╗
║        SYSTÈME DE GESTION DES JEUX OLYMPIQUES D'HIVER     ║
║                    VERSION INTERACTIVE                     ║
╚════════════════════════════════════════════════════════════╝
```

---

#### `make run-demo`
**Description** : Lance la version démo avec simulation automatique.

**Quand l'utiliser** :
- Pour une démonstration rapide du système
- Pour tester toutes les fonctionnalités automatiquement
- Pour voir un exemple complet

**Note** : Actuellement, la démo est dans ProgramDemo.cs mais n'est pas active par défaut.

**Exemple** :
```bash
make run-demo
```

---

#### `make watch`
**Description** : Lance l'application en mode développement avec rechargement automatique.

**Quand l'utiliser** :
- Pendant le développement actif
- Pour voir les changements immédiatement
- Évite de relancer manuellement à chaque modification

**Exemple** :
```bash
make watch
```

**Logique métier** :
- Surveille tous les fichiers .cs
- Recompile automatiquement à chaque modification
- Relance l'application automatiquement

**Pour arrêter** : `Ctrl+C`

---

### 🧹 NETTOYAGE

#### `make clean`
**Description** : Supprime les fichiers compilés (dossiers bin).

**Quand l'utiliser** :
- Avant un rebuild
- Pour libérer de l'espace disque
- Si vous voulez forcer une recompilation

**Exemple** :
```bash
make clean
```

**Ce qui est supprimé** :
- Tous les dossiers `bin/`
- Fichiers .dll compilés

**Ce qui est gardé** :
- Dossiers `obj/` (fichiers intermédiaires)

---

#### `make clean-all`
**Description** : Nettoyage complet (bin + obj).

**Quand l'utiliser** :
- Avant de committer dans Git
- Pour un état complètement propre
- Si vous avez des problèmes de cache

**Exemple** :
```bash
make clean-all
```

**Ce qui est supprimé** :
- Tous les dossiers `bin/`
- Tous les dossiers `obj/`
- Tous les fichiers générés

**Attention** : Nécessite un `make restore` et `make build` après.

---

### 🧪 TESTS ET QUALITÉ

#### `make format`
**Description** : Formate automatiquement tout le code selon les conventions .NET.

**Quand l'utiliser** :
- Avant de committer
- Pour uniformiser le style de code
- Après avoir écrit beaucoup de code

**Exemple** :
```bash
make format
```

**Ce qui se passe** :
- Indentation corrigée
- Espaces ajustés
- Conventions .NET appliquées

---

#### `make check`
**Description** : Vérifie le code sans compiler (analyse statique).

**Quand l'utiliser** :
- Pour détecter les warnings
- Vérification rapide avant commit
- Analyse de qualité du code

**Exemple** :
```bash
make check
```

---

### 📦 PUBLICATION

#### `make publish`
**Description** : Publie l'application (version portable nécessitant .NET).

**Quand l'utiliser** :
- Pour créer une version distribuable
- Pour partager l'application
- Version légère

**Exemple** :
```bash
make publish
```

**Sortie** : Dossier `./publish/` avec les fichiers

**Exécution** :
```bash
dotnet ./publish/TournamentManager.App.dll
```

---

#### `make publish-release`
**Description** : Publie l'application en version auto-suffisante (inclut .NET).

**Quand l'utiliser** :
- Pour distribution à des utilisateurs sans .NET
- Version production
- Déploiement serveur

**Exemple** :
```bash
make publish-release
```

**Logique métier** :
- Inclut le runtime .NET
- Fichier exécutable unique
- Plus gros mais autonome

**Exécution** :
```bash
./publish/TournamentManager.App
```

---

### 📊 INFORMATIONS

#### `make info`
**Description** : Affiche les statistiques du projet.

**Exemple** :
```bash
make info
```

**Sortie** :
```
╔════════════════════════════════════════════════════════════╗
║           INFORMATIONS DU PROJET                           ║
╚════════════════════════════════════════════════════════════╝

📂 Structure du projet:
  • TournamentManager.Models    - Modèles de données
  • TournamentManager.Services  - Logique métier
  • TournamentManager.App       - Application console

📊 Statistiques:
  • Fichiers .cs : 38
  • Lignes de code : 4063
```

---

#### `make version`
**Description** : Affiche la version de .NET installée.

**Exemple** :
```bash
make version
```

**Sortie** :
```
📌 Version de .NET:
10.0.100

📌 SDKs installés:
10.0.100 [/usr/share/dotnet/sdk]
```

---

#### `make tree`
**Description** : Affiche l'arborescence du projet.

**Exemple** :
```bash
make tree
```

---

### 🔧 DÉVELOPPEMENT

#### `make new-module NAME=xxx`
**Description** : Crée automatiquement un nouveau module avec template.

**Quand l'utiliser** :
- Pour ajouter un nouveau module au système
- Automatise la création avec structure standard

**Exemple** :
```bash
make new-module NAME=Participants
```

**Ce qui est créé** :
- Fichier `TournamentManager.App/Modules/ParticipantsModule.cs`
- Structure de base pré-remplie
- Méthode ShowMenu() template

**Template généré** :
```csharp
using System;
using TournamentManager.App.Core;
using TournamentManager.App.Helpers;

namespace TournamentManager.App.Modules
{
    public static class ParticipantsModule
    {
        private static ApplicationContext Context => ApplicationContext.Instance;

        public static void ShowMenu()
        {
            // TODO: Implémenter le menu du module Participants
            ConsoleHelper.DisplayTitle("MODULE PARTICIPANTS");
            ConsoleHelper.DisplayWarning("Module en cours de développement...");
            ConsoleHelper.PressKeyToContinue();
        }
    }
}
```

---

#### `make todo`
**Description** : Recherche tous les TODO/FIXME/HACK dans le code.

**Quand l'utiliser** :
- Pour voir les tâches en suspens
- Avant de committer
- Planification du travail

**Exemple** :
```bash
make todo
```

**Sortie** :
```
📝 TODO trouvés dans le code:
./TournamentManager.App/Program.cs:145:// TODO: Implémenter module participants
./TournamentManager.Services/CompetitionService.cs:89:// FIXME: Gérer les ex-aequo
```

---

### 📚 DOCUMENTATION

#### `make readme`
**Description** : Affiche le README dans le terminal.

**Exemple** :
```bash
make readme
```

---

#### `make git-workflow`
**Description** : Affiche le guide du workflow Git dans le terminal.

**Quand l'utiliser** :
- Pour consulter la stratégie de branches (main/dev)
- Pour voir les conventions de commits
- Pour apprendre à créer des feature branches
- Pour comprendre le workflow de release

**Exemple** :
```bash
make git-workflow
```

**Contenu du guide** :
- Stratégie de branchement (main = prod, dev = développement)
- Workflow quotidien (feature branches)
- Gestion des versions et tags
- Conventions de commits (feat, fix, docs, etc.)
- Commandes Git utiles
- Résolution de problèmes courants

---

## 🎯 Workflows Recommandés

### Workflow de développement quotidien
```bash
# 1. Récupérer les dernières modifications (si Git)
git pull

# 2. Compiler
make build

# 3. Lancer en mode watch pour développer
make watch

# 4. Formater avant de committer
make format

# 5. Vérifier
make check
```

---

### Workflow de nouvelle fonctionnalité
```bash
# 1. Créer un nouveau module
make new-module NAME=MaFonctionnalite

# 2. Développer en mode watch
make watch

# 3. Tester
make run

# 4. Formatter et vérifier
make format
make check

# 5. Nettoyer avant commit
make clean-all
```

---

### Workflow de publication
```bash
# 1. Nettoyer complètement
make clean-all

# 2. Compiler en Release
make build-release

# 3. Publier
make publish-release

# 4. Tester l'exécutable
./publish/TournamentManager.App
```

---

## ⚡ Astuces

### Combiner des commandes
```bash
# Nettoyer, compiler et lancer
make clean && make build && make run

# Ou utiliser rebuild
make rebuild && make run
```

### Voir l'aide à tout moment
```bash
make help
# ou simplement
make
```

### Développement rapide
```bash
# Utiliser les raccourcis
make r    # = make run
make b    # = make build
make c    # = make clean
```

---

## 🐛 Dépannage

### Erreur "make: command not found"
**Solution** : Installer make
```bash
sudo apt install make   # Ubuntu/Debian
sudo yum install make   # RedHat/CentOS
```

### Erreur de compilation
```bash
# 1. Nettoyer complètement
make clean-all

# 2. Restaurer les dépendances
make restore

# 3. Recompiler
make build
```

### L'application ne se lance pas
```bash
# Vérifier que .NET est installé
make version

# Recompiler
make rebuild

# Lancer
make run
```

---

## 📝 Notes Importantes

1. **Première utilisation** : Toujours faire `make restore` avant `make build`
2. **Mode watch** : Très pratique en développement mais consomme plus de ressources
3. **Clean vs Clean-all** : `clean` est plus rapide, `clean-all` est plus complet
4. **Publication** : `publish-release` crée un gros fichier mais autonome

---

**Aide** : Pour voir toutes les commandes disponibles, tapez `make help`
