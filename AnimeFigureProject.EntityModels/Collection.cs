using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeFigureProject.EntityModels
{

    public class Collection
    {

        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TotalPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TotalValue { get; set; }

        public List<AnimeFigure>? AnimeFigures { get; set; }
        public List<Collector>? Collectors { get; set; }

        public int OwnerId { get; set; }

        public decimal? CalculatePriceValueDiffrence() { return TotalPrice - TotalValue; }

    }

}
