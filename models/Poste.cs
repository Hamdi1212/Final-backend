namespace app.models
{
    public class Poste
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LigneId { get; set; } // non nullable
        public Ligne Ligne { get; set; }
    }
}