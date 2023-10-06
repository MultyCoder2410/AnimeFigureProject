namespace AnimeFigureProject.EntityModels
{
    
    public class Collector
    {

        public int Id { get; set; }
        
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public string? Name { get; set; }
        
        public List<Collection>? Collections { get; set; }

    }

}
