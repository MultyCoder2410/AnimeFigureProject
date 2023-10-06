using Microsoft.AspNetCore.Identity;

namespace AnimeFigureProject.EntityModels
{

    public class Collector : IdentityUser
    {

        public List<Collection>? Collections { get; set; }

    }

}
