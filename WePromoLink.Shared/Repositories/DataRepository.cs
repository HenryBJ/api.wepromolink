using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.Enums;
using WePromoLink.Models;

namespace WePromoLink.Repositories;

public partial class DataRepository
{
    private readonly DataContext _db;
    public DataRepository(DataContext db)
    {
        _db = db;
    }

}