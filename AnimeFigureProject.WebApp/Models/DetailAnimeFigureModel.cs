using AnimeFigureProject.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace AnimeFigureProject.WebApp.Models;

public record DetailAnimeFigureModel
(

    AnimeFigure NewAnimeFigure,
    IList<FileContentResult> Images

);