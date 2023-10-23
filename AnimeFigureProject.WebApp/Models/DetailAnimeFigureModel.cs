using AnimeFigureProject.EntityModels;

namespace AnimeFigureProject.WebApp.Models;

public record DetailAnimeFigureModel
(

    AnimeFigure NewAnimeFigure,
    IList<string> Images

);