using System.Collections.Generic;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente une équipe participant à un tournoi.
    /// </summary>
    public class Team
    {
        // Propriétés de base d'une équipe
        public int Id { get; set; }                     // Identifiant unique de l'équipe
        public string Name { get; set; }                // Nom de l'équipe
        public List<Player> Players { get; set; }      // Liste des joueurs dans l'équipe

        // Constructeur pour initialiser une équipe avec ses informations de base
        public Team(int id, string name)
        {
            Id = id;
            Name = name;
            Players = new List<Player>(); // Initialisation de la liste des joueurs
        }

        // Méthode pour ajouter un joueur à l'équipe
        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}
