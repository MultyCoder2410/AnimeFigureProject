using AnimeFigureProject.EntityModels;

namespace AnimeFigureProject.WebApp.Models;

public record AllAnimeFiguresModel
(

    IList<AnimeFigure>? AnimeFigures,
    IList<EntityModels.Type>? Types,
    IList<Brand>? Brands,
    IList<Origin>? Origins

);