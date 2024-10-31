
using Rise.Domain.Users;
﻿﻿using Rise.Domain.Leveranciers;
using Rise.Domain.Products;

namespace Rise.Persistence;

public class Seeder
{
    private readonly ApplicationDbContext dbContext;

    public Seeder(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Seed()
    {
        if (HasAlreadyBeenSeeded())
            return;
//        this.dbContext.Database.EnsureDeleted();
        
        SeedLeveranciers();
        SeedProducts();
        SeedUsers();
    }

    private bool HasAlreadyBeenSeeded()
    {
        return dbContext.Products.Any() || dbContext.Leveranciers.Any() || dbContext.Users.Any(); 
    }

    private void SeedLeveranciers()
    {
        //dbContext.Leveranciers.RemoveRange(dbContext.Leveranciers); 

        var leveranciers = new List<Leverancier>
            {
                new Leverancier { Name = "Leverancier A", Email = "a@example.com", Address = "Address A" },
                new Leverancier { Name = "Leverancier B", Email = "b@example.com", Address = "Address B" },
                new Leverancier { Name = "Leverancier C", Email = "c@example.com", Address = "Address C" }
            };

        dbContext.Leveranciers.AddRange(leveranciers);
        dbContext.SaveChanges(); 
    }

    private void SeedUsers()
    {
        //dbContext.Users.RemoveRange(dbContext.Users);

        var availableRoles = Enum.GetValues(typeof(Rol)).Cast<Rol>().ToList();

        
        var users = Enumerable.Range(1, 10)
                              .Select(i => new User
                              {
                                  Voornaam = $"Voornaam {i}",
                                  Naam = $"Naam {i}",
                                  Email = $"user{i}@example.com",
                                  TelNr = $"12345" ,
                                  Rol = availableRoles[i % availableRoles.Count]
                              })
                              .ToList();
        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();
    }


    private void SeedProducts()
    {
        //Bij triggers kijken om met flag te werken ipv ze echt te verwijderen
        //dbContext.Products.RemoveRange(dbContext.Products);
        var leveranciers = dbContext.Leveranciers.ToList();
        var products = Enumerable.Range(1, 20)
                                 .Select(i => new Product 
                                 {
                                     Name = $"Product {i}",
                                     Location = $"Location {i}", 
                                     Description = $"Description for Product {i}", 
                                     Reusable = i % 2 == 0 ,
                                     Quantity = 1,
                                     Barcode = "123456" + i.ToString(),
                                     Leverancier = leveranciers[i % leveranciers.Count]
                                 })
                                 .ToList();

        dbContext.Products.AddRange(products);
        dbContext.SaveChanges();
    }
}