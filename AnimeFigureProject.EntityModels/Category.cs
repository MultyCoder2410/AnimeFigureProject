namespace AnimeFigureProject.EntityModels
{

    public class Category
    {

        public int Id { get; set; }
        public string? Name { get; set; }

        public List<AnimeFigure>? AnimeFigures { get; set; }

    }

}
