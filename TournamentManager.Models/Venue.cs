namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un lieu de compétition (ex: stade, piste de ski, patinoire).
    /// </summary>
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string VenueType { get; set; } // Ex: "Alpine Skiing", "Ice Hockey Arena", "Speed Skating Oval"

        public Venue(int id, string name, string location, int capacity, string venueType)
        {
            Id = id;
            Name = name;
            Location = location;
            Capacity = capacity;
            VenueType = venueType;
        }

        public override string ToString()
        {
            return $"{Name} - {Location} (Capacité: {Capacity})";
        }
    }
}
