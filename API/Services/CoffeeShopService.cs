using API.Models;
using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CoffeeShopService : ICoffeeShopService {
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CoffeeShopService(ApplicationDbContext context, IMapper mapper) {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<List<CoffeeShopModel>> List() {
        //No mapper solution

        //IQueryable<CoffeeShop> coffeeShopsQueryable = context.CoffeeShops;
        //List<CoffeeShopModel> coffeeShopsEnumerable = await coffeeShopsQueryable.Select(x => MapCoffeeShop(x)).ToListAsync();
        //return coffeeShopsEnumerable;

        //Automapper
        return mapper.Map<IQueryable<CoffeeShop>, List<CoffeeShopModel>>(context.CoffeeShops);
    }
    
    private static CoffeeShopModel MapCoffeeShop(CoffeeShop entity) {
        CoffeeShopModel model = new CoffeeShopModel {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            OpeningHours = entity.OpeningHours,
        };
        return model;
    }
}
