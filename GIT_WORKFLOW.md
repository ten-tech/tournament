# 🌿 Workflow Git - TournamentManager

Guide complet de la stratégie de branches et des workflows Git pour le projet.

---

## 📋 Table des matières

1. [Vue d'ensemble](#vue-densemble)
2. [Stratégie de branches](#stratégie-de-branches)
3. [Workflow quotidien](#workflow-quotidien)
4. [Gestion des versions](#gestion-des-versions)
5. [Conventions de commits](#conventions-de-commits)
6. [Commandes utiles](#commandes-utiles)

---

## 🎯 Vue d'ensemble

Ce projet utilise une **stratégie de branchement professionnelle** avec deux branches principales :

- **`main`** : Branche de **production** (code stable uniquement)
- **`dev`** : Branche de **développement** (code en cours)

### Principe fondamental

> **La branche `main` doit TOUJOURS être stable et prête pour la production.**
>
> Tout le développement actif se fait sur `dev` ou des branches de fonctionnalités.

---

## 🌳 Stratégie de branches

### Branches principales

```
main (production)
  ↓
dev (développement actif)
  ↓
feature/* (fonctionnalités individuelles)
```

#### **`main` - Production**

- ✅ **Code stable uniquement**
- ✅ **Testé et validé**
- ✅ **Taggé avec des versions (v1.0.0, v1.1.0, etc.)**
- ✅ **Déployable à tout moment**
- ❌ **Pas de commits directs** (sauf corrections critiques)
- ❌ **Pas de code expérimental**

**Commits typiques sur main :**
- Merges depuis `dev` après validation complète
- Hotfixes critiques (avec tag patch : v1.0.1)

#### **`dev` - Développement**

- ✅ **Code en cours de développement**
- ✅ **Nouvelles fonctionnalités**
- ✅ **Corrections de bugs non-critiques**
- ✅ **Point de départ pour les feature branches**
- ⚠️ **Doit compiler sans erreurs**
- ⚠️ **Testé avant merge vers main**

**Commits typiques sur dev :**
- Nouvelles fonctionnalités
- Refactoring
- Améliorations
- Corrections de bugs

#### **`feature/*` - Branches de fonctionnalités**

- ✅ **Une branche par fonctionnalité**
- ✅ **Nommage descriptif** (ex: `feature/add-athlete-module`)
- ✅ **Créée depuis `dev`**
- ✅ **Mergée vers `dev` une fois terminée**
- ✅ **Supprimée après merge**

**Exemples de branches feature :**
```
feature/add-athlete-module
feature/improve-scoring-algorithm
feature/add-schedule-export
bugfix/fix-medal-calculation
```

---

## 🚀 Workflow quotidien

### 1. Démarrer une nouvelle fonctionnalité

```bash
# 1. Basculer sur dev et mettre à jour
git checkout dev
git pull origin dev

# 2. Créer une branche de fonctionnalité
git checkout -b feature/nom-de-la-fonctionnalite

# 3. Travailler sur la fonctionnalité
# ... (éditer les fichiers, coder, tester)

# 4. Commiter régulièrement
git add .
git commit -m "feat: ajout de la fonctionnalité X"

# 5. Pousser la branche
git push -u origin feature/nom-de-la-fonctionnalite
```

### 2. Intégrer une fonctionnalité dans dev

```bash
# 1. S'assurer que dev est à jour
git checkout dev
git pull origin dev

# 2. Merger la feature branch
git merge feature/nom-de-la-fonctionnalite

# 3. Résoudre les conflits si nécessaire
# ... (éditer les fichiers en conflit)
git add .
git commit -m "merge: intégration de la fonctionnalité X"

# 4. Pousser dev
git push origin dev

# 5. Supprimer la feature branch (optionnel)
git branch -d feature/nom-de-la-fonctionnalite
git push origin --delete feature/nom-de-la-fonctionnalite
```

### 3. Préparer une release (dev → main)

```bash
# 1. S'assurer que dev est stable et testé
git checkout dev
make clean-all
make build
make run    # Tester l'application

# 2. Basculer sur main
git checkout main
git pull origin main

# 3. Merger dev dans main
git merge dev --no-ff -m "release: version 1.1.0"

# 4. Créer un tag de version
git tag -a v1.1.0 -m "Version 1.1.0 - Nouvelles fonctionnalités

- Ajout du module Athlètes
- Amélioration du système de scoring
- Corrections de bugs
"

# 5. Pousser main et le tag
git push origin main
git push origin v1.1.0
```

### 4. Hotfix critique (correction d'urgence)

```bash
# 1. Créer une branche hotfix depuis main
git checkout main
git checkout -b hotfix/correction-critique

# 2. Faire la correction
# ... (éditer le code)
git add .
git commit -m "fix: correction du bug critique X"

# 3. Merger dans main
git checkout main
git merge hotfix/correction-critique

# 4. Créer un tag patch
git tag -a v1.0.1 -m "Version 1.0.1 - Hotfix

- Correction du bug critique X
"

# 5. Merger aussi dans dev pour garder la correction
git checkout dev
git merge hotfix/correction-critique

# 6. Pousser tout
git push origin main
git push origin dev
git push origin v1.0.1

# 7. Supprimer la branche hotfix
git branch -d hotfix/correction-critique
```

---

## 🏷️ Gestion des versions

### Semantic Versioning (SemVer)

Le projet utilise le **semantic versioning** : `MAJOR.MINOR.PATCH`

```
v1.2.3
│ │ │
│ │ └─ PATCH: Corrections de bugs
│ └─── MINOR: Nouvelles fonctionnalités (compatible)
└───── MAJOR: Changements incompatibles
```

#### Exemples

- `v1.0.0` : Version initiale stable
- `v1.1.0` : Ajout de nouvelles fonctionnalités (compatibles)
- `v1.1.1` : Correction de bugs
- `v2.0.0` : Changements majeurs (breaking changes)

### Créer un tag

```bash
# Tag annoté (recommandé)
git tag -a v1.2.0 -m "Version 1.2.0 - Description des changements"

# Tag simple (non recommandé)
git tag v1.2.0

# Pousser le tag
git push origin v1.2.0

# Pousser tous les tags
git push origin --tags
```

### Lister les tags

```bash
# Lister tous les tags
git tag

# Voir les détails d'un tag
git show v1.0.0

# Lister les tags avec descriptions
git tag -n
```

### Supprimer un tag

```bash
# Supprimer localement
git tag -d v1.0.0

# Supprimer sur le remote
git push origin --delete v1.0.0
```

---

## 📝 Conventions de commits

### Format standard

```
<type>(<scope>): <description courte>

<description détaillée optionnelle>

<footer optionnel>
```

### Types de commits

| Type | Description | Exemple |
|------|-------------|---------|
| `feat` | Nouvelle fonctionnalité | `feat: ajout du module athlètes` |
| `fix` | Correction de bug | `fix: correction du calcul des médailles` |
| `docs` | Documentation uniquement | `docs: mise à jour du README` |
| `style` | Formatage, indentation | `style: formatage du code avec dotnet format` |
| `refactor` | Refactoring sans changement de fonctionnalité | `refactor: simplification de ScoringService` |
| `test` | Ajout ou correction de tests | `test: ajout de tests pour CompetitionService` |
| `chore` | Tâches de maintenance | `chore: mise à jour des dépendances` |
| `perf` | Amélioration de performance | `perf: optimisation du tri des médailles` |

### Exemples de bons commits

```bash
# Fonctionnalité simple
git commit -m "feat: ajout de la gestion des athlètes"

# Fonctionnalité avec détails
git commit -m "feat(athletes): ajout du module de gestion des athlètes

- Interface interactive pour ajouter des athlètes
- Validation des données (âge, genre, nationalité)
- Affichage de la liste des athlètes par pays
- Tests unitaires pour AthleteService"

# Correction de bug
git commit -m "fix(medals): correction du tri par médailles d'or"

# Documentation
git commit -m "docs: ajout du guide de workflow Git"

# Refactoring
git commit -m "refactor(services): extraction de la logique de validation"
```

### Exemples de mauvais commits

❌ `git commit -m "fix"` (pas assez descriptif)
❌ `git commit -m "changed stuff"` (vague)
❌ `git commit -m "WIP"` (work in progress sur main/dev)
❌ `git commit -m "test 123"` (pas professionnel)

---

## 🔧 Commandes utiles

### Vérifier l'état du repo

```bash
# Voir les branches locales
git branch

# Voir toutes les branches (locales + remote)
git branch -a

# Voir les commits récents
git log --oneline -10

# Voir l'historique graphique
git log --graph --oneline --all --decorate

# Voir les différences non commitées
git diff

# Voir les différences entre branches
git diff main..dev
```

### Synchronisation

```bash
# Mettre à jour toutes les branches
git fetch --all

# Mettre à jour la branche courante
git pull

# Pousser la branche courante
git push

# Pousser une nouvelle branche
git push -u origin nom-de-branche
```

### Gestion des branches

```bash
# Créer une branche
git checkout -b nom-de-branche

# Basculer entre branches
git checkout nom-de-branche

# Supprimer une branche locale
git branch -d nom-de-branche

# Forcer la suppression (si non mergée)
git branch -D nom-de-branche

# Supprimer une branche remote
git push origin --delete nom-de-branche

# Renommer la branche courante
git branch -m nouveau-nom
```

### Annuler des changements

```bash
# Annuler les modifications non commitées d'un fichier
git checkout -- fichier.cs

# Annuler tous les changements non commitées
git reset --hard

# Annuler le dernier commit (garder les changements)
git reset --soft HEAD~1

# Annuler le dernier commit (supprimer les changements)
git reset --hard HEAD~1

# Revenir à un commit spécifique
git reset --hard <commit-hash>
```

### Stash (sauvegarder temporairement)

```bash
# Sauvegarder les modifications en cours
git stash

# Lister les stash
git stash list

# Appliquer le dernier stash
git stash pop

# Appliquer un stash spécifique
git stash apply stash@{0}

# Supprimer tous les stash
git stash clear
```

---

## 📊 Workflow visuel

```
┌─────────────────────────────────────────────────────────────┐
│                     WORKFLOW COMPLET                        │
└─────────────────────────────────────────────────────────────┘

main (production)
  │
  │ tag v1.0.0
  ├─────────────────────────────────────────────► (stable)
  │
  │ merge release
  ◄─────────────────────────┐
                            │
dev (développement)         │
  │                         │
  ├───► feature/athletes ───┤ (merge après tests)
  │                         │
  ├───► feature/schedule ───┤ (merge après tests)
  │                         │
  ├───► bugfix/medals ──────┤ (merge après tests)
  │                         │
  └─────────────────────────┘
```

### Flux de travail type

1. **Développement** : `feature/* → dev`
2. **Tests et validation** : sur `dev`
3. **Release** : `dev → main` (avec tag)
4. **Hotfix si nécessaire** : `hotfix/* → main` et `→ dev`

---

## 🎯 Best Practices

### ✅ À FAIRE

- ✅ Commiter souvent avec des messages clairs
- ✅ Tester avant de merger vers dev
- ✅ Créer des branches pour chaque fonctionnalité
- ✅ Utiliser des tags pour les versions
- ✅ Garder main toujours stable
- ✅ Mettre à jour dev régulièrement depuis main
- ✅ Documenter les changements importants

### ❌ À ÉVITER

- ❌ Commiter directement sur main
- ❌ Pousser du code non testé sur dev
- ❌ Créer des commits "WIP" ou "test"
- ❌ Laisser des branches feature ouvertes longtemps
- ❌ Oublier de tagger les releases
- ❌ Merger sans résoudre les conflits
- ❌ Forcer un push (`git push --force`) sur main ou dev

---

## 📚 Ressources

### Documentation officielle

- [Git Documentation](https://git-scm.com/doc)
- [GitHub Flow](https://guides.github.com/introduction/flow/)
- [Semantic Versioning](https://semver.org/)
- [Conventional Commits](https://www.conventionalcommits.org/)

### Outils utiles

- **GitKraken** : Interface graphique Git
- **SourceTree** : Client Git gratuit
- **VSCode Git** : Extension Git intégrée
- **GitHub Desktop** : Client officiel GitHub

### Commandes Make intégrées

Le Makefile du projet inclut aussi des commandes Git :

```bash
# Voir l'état Git
make git-status

# Voir les commits récents
make git-log

# Voir les branches
make git-branches
```

---

## 🆘 Aide et dépannage

### J'ai commité sur la mauvaise branche

```bash
# Annuler le commit (garder les changements)
git reset --soft HEAD~1

# Basculer sur la bonne branche
git checkout bonne-branche

# Refaire le commit
git add .
git commit -m "message"
```

### J'ai des conflits lors d'un merge

```bash
# Voir les fichiers en conflit
git status

# Éditer les fichiers et résoudre les conflits
# Chercher les marqueurs <<<<<<<, =======, >>>>>>>

# Marquer comme résolu
git add fichier-résolu.cs

# Terminer le merge
git commit
```

### J'ai poussé du code par erreur

```bash
# ATTENTION : ne jamais forcer sur main ou dev en équipe !

# Si personne n'a récupéré le commit
git reset --hard HEAD~1
git push --force-with-lease

# Sinon, créer un commit de revert
git revert HEAD
git push
```

---

**Version du document** : 1.0.0
**Dernière mise à jour** : 2024
**Mainteneur** : Équipe TournamentManager

Pour toute question sur le workflow Git, consultez ce document ou demandez à l'équipe.
