namespace AnimeFigureProject.EntityModels
{

    public class Collector
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Collection>? Collections { get; set; }
        public string? AuthenticationUserId { get; set; }

    }

}
