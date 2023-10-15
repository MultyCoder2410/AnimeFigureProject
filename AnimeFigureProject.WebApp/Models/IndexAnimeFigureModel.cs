using AnimeFigureProject.EntityModels;

namespace AnimeFigureProject.WebApp.Models;

public record AllAnimeFiguresModel
(

    IList<AnimeFigure>? AnimeFigures,
    IList<Category>? Categories,
    IList<Brand>? Brands,
    IList<Origin>? Origins

);