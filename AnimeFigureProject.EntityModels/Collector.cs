using Microsoft.AspNetCore.Identity;

namespace AnimeFigureProject.EntityModels
{

    public class Collector : IdentityUser<int>
    {

        public List<Collection>? Collections { get; set; }

    }

}
