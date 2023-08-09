using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDeliciousCore.Models;
using System.ComponentModel;

namespace CRUDeliciousCore.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    
    // Add field - adding context into our class // "db" can eb any name
    private MyContext db;


    // update below adding context, etc
    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }


    // Get AllDishes Route * ============================================
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> allDishes = db.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
        return View("AllDishes", allDishes);
    }


    // New Route * ============================================
    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("New");
    }


    // Create Dish method ============================================
    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return View("New");
        }
        //! using db table name "Dishes"
        db.Dishes.Add(newDish);
        db.SaveChanges();
        return RedirectToAction("Index");
    }


    // view one Dish method ============================================
    [HttpGet("dishes/{dishId}")]
    public IActionResult ViewDish(int dishId)
    {
        //Query below:
        Dish dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if(dish == null) 
        {
            return RedirectToAction("Index");
        }
        return View("ViewDish", dish );
    }


    // EDIT one dish method ============================================
    [HttpGet("dishes/{dishId}/edit")]
    public IActionResult Edit(int dishId)
    {
        //Query below:
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if(dish == null) 
        {
            return RedirectToAction("Index");
        }
        return View("Edit", dish );
    }

    //Update a dish Method ============================================
    [HttpPost("dishes/{dishId}/update")]
    // MatchCasing to the dishId route
    public IActionResult Update(Dish editDish, int dishId)

    {
        if(!ModelState.IsValid)
        {
            return Edit(dishId);
            // return View("Edit");
        }
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if(dish == null) 
        {
            return RedirectToAction("Index");
        }
        dish.Name = editDish.Name;
        dish.Chef = editDish.Chef;
        dish.Tastiness = editDish.Tastiness;
        dish.Calories = editDish.Calories;
        dish.Description = editDish.Description;
        db.Dishes.Update(dish);
        db.SaveChanges();
        return RedirectToAction("ViewDish", new {dishId = dishId});

    }


    //Delete one dish Method ============================================
    [HttpPost("dishes/{dishId}/delete")]
    public IActionResult Delete(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        db.Dishes.Remove(dish);
        db.SaveChanges();
        // ListSortDescription in the all posts for Index*
        return RedirectToAction("Index");
    }



    // Privacy Route ============================================
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
