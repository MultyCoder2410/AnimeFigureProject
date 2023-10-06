using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeFigureProject.EntityModels
{

    public class AnimeFigure
    {

        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Value { get; set; }

        public Brand? Brand { get; set; }
        public Type? Type { get; set; }

        public List<Origin>? Origins {  get; set; }
        public List<Category>? Categories { get; set; }
        public List<Review>? Reviews {  get; set; }

        public string? ImageFolderPath { get; set; }

    }

}
