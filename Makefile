# ═══════════════════════════════════════════════════════════════════════════
# Makefile - Système de Gestion des Jeux Olympiques d'Hiver
# ═══════════════════════════════════════════════════════════════════════════
# Ce Makefile centralise toutes les commandes nécessaires pour gérer le projet.
# Logique métier : Automatise les tâches répétitives et standardise les workflows.
# ═══════════════════════════════════════════════════════════════════════════

# ───────────────────────────────────────────────────────────────────────────
# VARIABLES DE CONFIGURATION
# ───────────────────────────────────────────────────────────────────────────

# Chemin vers le projet principal
PROJECT_APP = TournamentManager.App/TournamentManager.App.csproj
PROJECT_MODELS = TournamentManager.Models/TournamentManager.Models.csproj
PROJECT_SERVICES = TournamentManager.Services/TournamentManager.Services.csproj

# Configuration de build
CONFIGURATION = Debug
RUNTIME = linux-x64

# Couleurs pour les messages (si le terminal supporte)
COLOR_RESET = \033[0m
COLOR_INFO = \033[36m
COLOR_SUCCESS = \033[32m
COLOR_WARNING = \033[33m
COLOR_ERROR = \033[31m

# ───────────────────────────────────────────────────────────────────────────
# CIBLES PAR DÉFAUT
# ───────────────────────────────────────────────────────────────────────────

.PHONY: help
.DEFAULT_GOAL := help

# Affiche l'aide avec toutes les commandes disponibles
help:
	@echo ""
	@echo "$(COLOR_INFO)╔════════════════════════════════════════════════════════════╗$(COLOR_RESET)"
	@echo "$(COLOR_INFO)║     SYSTÈME DE GESTION DES JEUX OLYMPIQUES D'HIVER        ║$(COLOR_RESET)"
	@echo "$(COLOR_INFO)║                    COMMANDES MAKEFILE                      ║$(COLOR_RESET)"
	@echo "$(COLOR_INFO)╚════════════════════════════════════════════════════════════╝$(COLOR_RESET)"
	@echo ""
	@echo "$(COLOR_SUCCESS)🏗️  COMPILATION$(COLOR_RESET)"
	@echo "  make build                - Compiler tout le projet"
	@echo "  make build-release        - Compiler en mode Release"
	@echo "  make rebuild              - Nettoyer et recompiler"
	@echo "  make restore              - Restaurer les dépendances NuGet"
	@echo ""
	@echo "$(COLOR_SUCCESS)🚀 EXÉCUTION$(COLOR_RESET)"
	@echo "  make run                  - Lancer l'application interactive"
	@echo "  make run-demo             - Lancer la démo automatique"
	@echo "  make watch                - Mode développement avec rechargement auto"
	@echo ""
	@echo "$(COLOR_SUCCESS)🧹 NETTOYAGE$(COLOR_RESET)"
	@echo "  make clean                - Supprimer les fichiers compilés"
	@echo "  make clean-all            - Nettoyage complet (bin + obj)"
	@echo ""
	@echo "$(COLOR_SUCCESS)🧪 TESTS ET QUALITÉ$(COLOR_RESET)"
	@echo "  make format               - Formater le code avec dotnet format"
	@echo "  make check                - Vérifier les warnings et erreurs"
	@echo ""
	@echo "$(COLOR_SUCCESS)📦 PUBLICATION$(COLOR_RESET)"
	@echo "  make publish              - Publier l'application (auto-suffisante)"
	@echo "  make publish-release      - Publier en mode Release optimisé"
	@echo ""
	@echo "$(COLOR_SUCCESS)📊 INFORMATIONS$(COLOR_RESET)"
	@echo "  make info                 - Afficher les informations du projet"
	@echo "  make version              - Afficher la version de .NET"
	@echo "  make tree                 - Afficher l'arborescence du projet"
	@echo ""
	@echo "$(COLOR_SUCCESS)🔧 DÉVELOPPEMENT$(COLOR_RESET)"
	@echo "  make new-module NAME=xxx  - Créer un nouveau module"
	@echo "  make todo                 - Afficher les TODO dans le code"
	@echo ""
	@echo "$(COLOR_SUCCESS)📚 DOCUMENTATION$(COLOR_RESET)"
	@echo "  make readme               - Ouvrir le README"
	@echo "  make git-workflow         - Ouvrir le guide Git workflow"
	@echo ""

# ───────────────────────────────────────────────────────────────────────────
# COMPILATION
# ───────────────────────────────────────────────────────────────────────────

# Restaure les packages NuGet
# Logique métier : Télécharge toutes les dépendances nécessaires avant la compilation
.PHONY: restore
restore:
	@echo "$(COLOR_INFO)📦 Restauration des dépendances NuGet...$(COLOR_RESET)"
	@dotnet restore
	@echo "$(COLOR_SUCCESS)✓ Dépendances restaurées avec succès$(COLOR_RESET)"

# Compile le projet en mode Debug
# Logique métier : Build par défaut pour le développement avec symboles de débogage
.PHONY: build
build: restore
	@echo "$(COLOR_INFO)🏗️  Compilation du projet en mode $(CONFIGURATION)...$(COLOR_RESET)"
	@dotnet build --configuration $(CONFIGURATION) --no-restore
	@echo "$(COLOR_SUCCESS)✓ Compilation réussie$(COLOR_RESET)"

# Compile le projet en mode Release
# Logique métier : Build optimisé pour la production sans symboles de débogage
.PHONY: build-release
build-release: CONFIGURATION = Release
build-release: restore
	@echo "$(COLOR_INFO)🏗️  Compilation du projet en mode Release...$(COLOR_RESET)"
	@dotnet build --configuration Release --no-restore
	@echo "$(COLOR_SUCCESS)✓ Compilation Release réussie$(COLOR_RESET)"

# Nettoie et recompile le projet
# Logique métier : Utilisé quand des changements majeurs nécessitent un rebuild complet
.PHONY: rebuild
rebuild: clean build
	@echo "$(COLOR_SUCCESS)✓ Rebuild terminé$(COLOR_RESET)"

# ───────────────────────────────────────────────────────────────────────────
# EXÉCUTION
# ───────────────────────────────────────────────────────────────────────────

# Lance l'application interactive
# Logique métier : Point d'entrée principal pour l'utilisateur final
.PHONY: run
run:
	@echo "$(COLOR_INFO)🚀 Lancement de l'application interactive...$(COLOR_RESET)"
	@echo ""
	@dotnet run --project $(PROJECT_APP)

# Lance la version démo avec simulation automatique
# Logique métier : Utilisé pour les démonstrations et tests rapides
.PHONY: run-demo
run-demo:
	@echo "$(COLOR_INFO)🎬 Lancement de la démo automatique...$(COLOR_RESET)"
	@echo "$(COLOR_WARNING)Note: Pour utiliser la démo, décommentez RunDemo() dans ProgramDemo.cs$(COLOR_RESET)"
	@dotnet run --project $(PROJECT_APP)

# Mode watch pour le développement
# Logique métier : Recompile automatiquement à chaque modification de fichier
.PHONY: watch
watch:
	@echo "$(COLOR_INFO)👀 Mode développement activé (rechargement automatique)...$(COLOR_RESET)"
	@dotnet watch --project $(PROJECT_APP) run

# ───────────────────────────────────────────────────────────────────────────
# NETTOYAGE
# ───────────────────────────────────────────────────────────────────────────

# Nettoie les fichiers de compilation (bin)
# Logique métier : Supprime les binaires compilés mais garde les fichiers intermédiaires
.PHONY: clean
clean:
	@echo "$(COLOR_WARNING)🧹 Nettoyage des fichiers compilés...$(COLOR_RESET)"
	@dotnet clean
	@echo "$(COLOR_SUCCESS)✓ Nettoyage terminé$(COLOR_RESET)"

# Nettoyage complet (bin + obj)
# Logique métier : Supprime tous les fichiers générés pour un état complètement propre
.PHONY: clean-all
clean-all:
	@echo "$(COLOR_WARNING)🧹 Nettoyage complet (bin + obj)...$(COLOR_RESET)"
	@find . -type d -name "bin" -exec rm -rf {} + 2>/dev/null || true
	@find . -type d -name "obj" -exec rm -rf {} + 2>/dev/null || true
	@echo "$(COLOR_SUCCESS)✓ Nettoyage complet terminé$(COLOR_RESET)"

# ───────────────────────────────────────────────────────────────────────────
# TESTS ET QUALITÉ
# ───────────────────────────────────────────────────────────────────────────

# Formate le code selon les conventions .NET
# Logique métier : Assure une cohérence du style de code dans tout le projet
.PHONY: format
format:
	@echo "$(COLOR_INFO)✨ Formatage du code...$(COLOR_RESET)"
	@dotnet format
	@echo "$(COLOR_SUCCESS)✓ Code formaté$(COLOR_RESET)"

# Vérifie les warnings et erreurs sans compiler
# Logique métier : Analyse statique pour détecter les problèmes potentiels
.PHONY: check
check:
	@echo "$(COLOR_INFO)🔍 Vérification du code...$(COLOR_RESET)"
	@dotnet build --no-restore /p:TreatWarningsAsErrors=false
	@echo "$(COLOR_SUCCESS)✓ Vérification terminée$(COLOR_RESET)"

# ───────────────────────────────────────────────────────────────────────────
# PUBLICATION
# ───────────────────────────────────────────────────────────────────────────

# Publie l'application (version portable)
# Logique métier : Crée une version distribuable de l'application
.PHONY: publish
publish:
	@echo "$(COLOR_INFO)📦 Publication de l'application...$(COLOR_RESET)"
	@dotnet publish $(PROJECT_APP) \
		--configuration $(CONFIGURATION) \
		--output ./publish \
		--self-contained false
	@echo "$(COLOR_SUCCESS)✓ Application publiée dans ./publish$(COLOR_RESET)"

# Publie l'application en mode Release (auto-suffisante)
# Logique métier : Inclut le runtime .NET pour une distribution sans dépendances
.PHONY: publish-release
publish-release:
	@echo "$(COLOR_INFO)📦 Publication Release (auto-suffisante)...$(COLOR_RESET)"
	@dotnet publish $(PROJECT_APP) \
		--configuration Release \
		--runtime $(RUNTIME) \
		--output ./publish \
		--self-contained true \
		/p:PublishSingleFile=true \
		/p:IncludeNativeLibrariesForSelfExtract=true
	@echo "$(COLOR_SUCCESS)✓ Application publiée dans ./publish$(COLOR_RESET)"
	@ls -lh ./publish

# ───────────────────────────────────────────────────────────────────────────
# INFORMATIONS
# ───────────────────────────────────────────────────────────────────────────

# Affiche les informations du projet
# Logique métier : Donne un aperçu rapide de la structure du projet
.PHONY: info
info:
	@echo ""
	@echo "$(COLOR_INFO)╔════════════════════════════════════════════════════════════╗$(COLOR_RESET)"
	@echo "$(COLOR_INFO)║           INFORMATIONS DU PROJET                           ║$(COLOR_RESET)"
	@echo "$(COLOR_INFO)╚════════════════════════════════════════════════════════════╝$(COLOR_RESET)"
	@echo ""
	@echo "$(COLOR_SUCCESS)📂 Structure du projet:$(COLOR_RESET)"
	@echo "  • TournamentManager.Models    - Modèles de données"
	@echo "  • TournamentManager.Services  - Logique métier"
	@echo "  • TournamentManager.App       - Application console"
	@echo ""
	@echo "$(COLOR_SUCCESS)📊 Statistiques:$(COLOR_RESET)"
	@echo -n "  • Fichiers .cs : "
	@find . -name "*.cs" -not -path "*/obj/*" -not -path "*/bin/*" | wc -l
	@echo -n "  • Lignes de code : "
	@find . -name "*.cs" -not -path "*/obj/*" -not -path "*/bin/*" -exec wc -l {} + | tail -1 | awk '{print $$1}'
	@echo ""

# Affiche la version de .NET installée
# Logique métier : Vérification de l'environnement de développement
.PHONY: version
version:
	@echo "$(COLOR_INFO)📌 Version de .NET:$(COLOR_RESET)"
	@dotnet --version
	@echo ""
	@echo "$(COLOR_INFO)📌 SDKs installés:$(COLOR_RESET)"
	@dotnet --list-sdks

# Affiche l'arborescence du projet
# Logique métier : Visualisation de la structure des fichiers
.PHONY: tree
tree:
	@echo "$(COLOR_INFO)🌳 Arborescence du projet:$(COLOR_RESET)"
	@tree -I 'bin|obj|publish' -L 3 --dirsfirst || \
		find . -not -path "*/bin/*" -not -path "*/obj/*" -not -path "*/publish/*" -print | \
		sed 's|[^/]*/|  |g'

# ───────────────────────────────────────────────────────────────────────────
# DÉVELOPPEMENT
# ───────────────────────────────────────────────────────────────────────────

# Recherche les TODO dans le code
# Logique métier : Aide à suivre les tâches en suspens dans le code
.PHONY: todo
todo:
	@echo "$(COLOR_WARNING)📝 TODO trouvés dans le code:$(COLOR_RESET)"
	@grep -rn "TODO\|FIXME\|HACK\|XXX" \
		--include="*.cs" \
		--exclude-dir={bin,obj,publish} \
		. || echo "  Aucun TODO trouvé ✓"

# Crée un nouveau module interactif (template)
# Logique métier : Automatise la création de nouveaux modules avec structure standard
.PHONY: new-module
new-module:
ifndef NAME
	@echo "$(COLOR_ERROR)✗ Erreur: Spécifiez NAME=NomDuModule$(COLOR_RESET)"
	@echo "  Exemple: make new-module NAME=Competition"
else
	@echo "$(COLOR_INFO)➕ Création du module $(NAME)...$(COLOR_RESET)"
	@mkdir -p TournamentManager.App/Modules
	@echo "using System;" > TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "using TournamentManager.App.Core;" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "using TournamentManager.App.Helpers;" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "namespace TournamentManager.App.Modules" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "{" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "    public static class $(NAME)Module" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "    {" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "        private static ApplicationContext Context => ApplicationContext.Instance;" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "        public static void ShowMenu()" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "        {" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "            // TODO: Implémenter le menu du module $(NAME)" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "            ConsoleHelper.DisplayTitle(\"MODULE $(NAME)\");" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "            ConsoleHelper.DisplayWarning(\"Module en cours de développement...\");" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "            ConsoleHelper.PressKeyToContinue();" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "        }" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "    }" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "}" >> TournamentManager.App/Modules/$(NAME)Module.cs
	@echo "$(COLOR_SUCCESS)✓ Module $(NAME) créé dans TournamentManager.App/Modules/$(NAME)Module.cs$(COLOR_RESET)"
endif

# ───────────────────────────────────────────────────────────────────────────
# DOCUMENTATION
# ───────────────────────────────────────────────────────────────────────────

# Ouvre le README
# Logique métier : Accès rapide à la documentation
.PHONY: readme
readme:
	@cat README.md | less || more README.md

# Ouvre le guide du workflow Git
# Logique métier : Accès rapide à la stratégie de branches et conventions
.PHONY: git-workflow
git-workflow:
	@cat GIT_WORKFLOW.md | less || more GIT_WORKFLOW.md

# ───────────────────────────────────────────────────────────────────────────
# RACCOURCIS PRATIQUES
# ───────────────────────────────────────────────────────────────────────────

# Alias pour les commandes fréquentes
.PHONY: r b c
r: run          # Raccourci pour lancer l'app
b: build        # Raccourci pour compiler
c: clean        # Raccourci pour nettoyer

# ═══════════════════════════════════════════════════════════════════════════
# FIN DU MAKEFILE
# ═══════════════════════════════════════════════════════════════════════════
