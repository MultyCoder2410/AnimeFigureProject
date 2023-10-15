using AnimeFigureProject.EntityModels;

namespace AnimeFigureProject.WebApp.Models;

public record CreateAnimeFigureModel
(

    AnimeFigure NewAnimeFigure,
    IList<Category>? Categories,
    IList<Brand>? Brands,
    IList<Origin>? Origins

);