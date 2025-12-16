// Définition de la classe Player dans l'espace de noms TournamentManager.Models
namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un joueur participant à un tournoi.
    /// </summary>
    public class Player
    {
        // Propriétés de base d'un joueur
        public int Id { get; set; }       // Identifiant unique du joueur
        public string Name { get; set; }  // Nom du joueur
        public int Age { get; set; }     // Age du joueur
        public string Position { get; set; } // Position du joueur ex: Attaquant, Défenseur

        // Constructeur pour initialiser un joueur avec ses informations de base
        public Player(int id, string name, int age, string position)
        {
            Id = id;
            Name = name;
            Age = age;
            Position = position;
        }
    }
}
