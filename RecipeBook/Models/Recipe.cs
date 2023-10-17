using System.Collections.Generic;
using MySqlConnector;
using RecipeBook.Models;

namespace RecipeBook.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<RecipeTag> JoinEntities { get; }
    public ApplicationUser User { get; set; }

  }
}