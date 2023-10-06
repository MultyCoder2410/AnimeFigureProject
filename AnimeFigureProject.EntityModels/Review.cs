namespace AnimeFigureProject.EntityModels
{

    public class Review
    {

        public int Id { get; set; }
        public string? Text { get; set; }

        public Collector? Owner {  get; set; }
        public AnimeFigure? AnimeFigure { get; set; }


    }

}
