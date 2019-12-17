using System;

namespace JurassicParkDB
{
  // D I N O S A U R you are a dinosaur class
  public class Dinosaur
  {

    public int Id { get; set; }
    public string Species { get; set; } = "";
    public string Name { get; set; } = "";
    public string Diet { get; set; } = "Carnivore";
    public DateTime DateAcquired { get; set; }
    public decimal Weight { get; set; } = 0;
    public int EnclosureNumber { get; set; } = 0;
  }
}