using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IDataService
{
    //Users Statistics
    Task<IActionResult> GetAvailable();
    Task<IActionResult> GetBudget();
    Task<IActionResult> GetProfit();
}