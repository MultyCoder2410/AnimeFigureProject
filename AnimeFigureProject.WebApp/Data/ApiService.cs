using AnimeFigureProject.EntityModels;

namespace AnimeFigureProject.WebApp.Data
{
 
    public class ApiService
    {

        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
        
            this.httpClient = httpClient;
        
        }

        public async Task<AnimeFigure> GetAnimeFigure(int id)
        {

            AnimeFigure? animeFigure = null;
            HttpResponseMessage response = await httpClient.GetAsync("api/AnimeFigure/animefigure/" + id.ToString());
            
            if (response.IsSuccessStatusCode)
                animeFigure = await response.Content.ReadAsAsync<AnimeFigure>();

            if (animeFigure == null)
                animeFigure = new AnimeFigure();

            return animeFigure;

        }

    }

}
