using AnimeFigureProject.EntityModels;

namespace AnimeFigureProject.WebApp.Models;

public record CreateAnimeFigureModel
(

    AnimeFigure NewAnimeFigure,
    IList<EntityModels.Type>? Types,
    IList<Brand>? Brands,
    IList<Origin>? Origins

);