#pragma warning disable CS8618


using System.ComponentModel.DataAnnotations;
namespace CRUDeliciousCore.Models;

public class Dish
{
    [Key]
    // DishId =========================
    public int DishId {get; set;}


    // Name ========================= 
    [Required]
    public string Name {get; set;}
    

    // Chef ======================== 
    [Required]
    public string Chef {get; set;}


    // Tastiness ======================== 
    [Required]
    [Range(1,6, ErrorMessage = "You must include a Tastiness score between 1 - 5.")]
    public int Tastiness {get; set;}


    // Calories ======================== 
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0!")]
    public int Calories {get; set;}

    // Description ======================== 
    [Required]
    public string Description {get; set;}

    // CreatedAt ======================== 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    


    // UpdatedAt ======================== 
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



}
