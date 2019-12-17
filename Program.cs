using System;
using System.Collections.Generic;
using System.Linq;

namespace JurassicParkDB
{
  class Program
  {
    // List of dinosaurs
    // static List<Dinosaur> Dinosaurs = new List<Dinosaur>();
    static DinosaurContext Db = new DinosaurContext();

    static void AddDino()
    // This method creates a new dinosaur and adds it to the Dinosaurs list
    {
      Console.WriteLine();
      Console.WriteLine("What SPECIES is the dinosaur?");
      Console.Write("...:");
      var dinoSpecies = Console.ReadLine();
      Console.WriteLine();
      Console.WriteLine("What is the NAME of the dinosaur?");
      Console.Write("...:");
      var dinoName = Console.ReadLine();
      Console.WriteLine();
      Console.WriteLine("Is the dinosaur a carnivore or herbivore?");
      Console.Write("...:");
      var dinoDiet = Console.ReadLine();
      Console.WriteLine();
      Console.WriteLine("How many pounds does the dinosaur weigh?");
      Console.Write("...:");
      var dinoWeight = Console.ReadLine();
      Console.WriteLine();
      Console.WriteLine("What is the NUMBER of the dinosaur's enclosure?");
      Console.Write("...:");
      var dinoEnclosure = Console.ReadLine();

      // Create a new dinosaur
      var dino = new Dinosaur();
      // Make it yours
      dino.Species = dinoSpecies;
      dino.Name = dinoName;
      //   dino.Diet = dinoDiet;
      dino.Diet = (dinoDiet[0]).ToString() == "h" ? "Herbivore" : "Carnivore";
      dino.Weight = decimal.Parse(dinoWeight);
      dino.EnclosureNumber = int.Parse(dinoEnclosure);
      dino.DateAcquired = DateTime.UtcNow;

      // Add this dinosaur to the dinosaurs list
      //   Dinosaurs.Add(item: dino);
      // Add this dinosaur to the dinosaurs database xxx
      Db.Dinosaurs.Add(dino);
      Db.SaveChanges(); // save changes

      Console.WriteLine();
      Console.WriteLine("Here are the details for the new addition:");
      WriteDinoDetail(dino);
    }

    static void Hatch()
    {
      // Create a new dinosaur
      var dino = new Dinosaur();

      // Assign a random name to the dinosaur from the array below.
      string[] dinoNames = new string[10] { "Felecia", "Mongo", "Jonathan", "Peggy", "Xander", "Lexa", "Lane", "Xyler", "Mark", "Jason" };
      Random randomName = new Random();
      dino.Name = dinoNames[randomName.Next(0, 10)];
      // Assign a random diet to the dino.
      string[] dinoDiets = new string[2] { "Carnivore", "Herbivore" };
      Random randomDiet = new Random();
      dino.Diet = dinoDiets[randomDiet.Next(0, 2)];
      // Assign a random weight to the dino. Being a baby, it won't be big.
      //  https://www.scholastic.com/teachers/articles/teaching-content/dinosaur-eggs-and-babies/
      Random randomWeight = new Random();
      dino.Weight = randomWeight.Next(1, 11);
      dino.DateAcquired = DateTime.UtcNow;

      // Add this dinosaur to the dinosaurs database
      Db.Dinosaurs.Add(dino);
      Db.SaveChanges(); // save changes

      Console.WriteLine("Here are the details for the baby:");
      WriteDinoDetail(dino);

    }

    static void ViewAll()
    // This method is executed when the user selects option to view all
    {
      //   DisplayListOfDinosaurs(Dinosaurs);
      DisplayListOfDinosaurs(Db.Dinosaurs); //xxx
    }

    static void DisplayListOfDinosaurs(IEnumerable<Dinosaur> dinosUnsorted)
    // This method displays all the dinosaurs sorted to show newest additions first
    {
      //Sort Dinos in descending order by date acquired
      var dinos = dinosUnsorted.OrderByDescending(dino => dino.DateAcquired);
      Console.WriteLine();
      Console.WriteLine("Dinosaur Inventory (newest additions first)");

      // Display the details of each dinosaur in the list
      foreach (var dino in dinos)
      {
        WriteDinoDetail(dino);
      }
      Console.WriteLine();
    }

    static void NeedsASheep(IEnumerable<Dinosaur> dinosAll)
    // This method displays the lightest carnivore detail.
    {
      //Filter to carnivores only, sort ascending by weight.
      var dinos = dinosAll.Where(dino => dino.Diet == "Carnivore").OrderBy(dino => dino.Weight);
      Console.WriteLine();
      Console.WriteLine("Awe....Look at the little dino. It needs a snack.");
      Console.WriteLine("Send in the sheep!");
      // Display the details for the smallest carnivore
      WriteDinoDetail(dinos.FirstOrDefault());
      Console.WriteLine();
    }
    static void DisplayHeaviestDinosaurs(IEnumerable<Dinosaur> dinosUnsorted)
    // This method displays all the dinosaurs sorted to show newest additions first
    {
      //Sort Dinos in descending order by date acquired
      var dinos = dinosUnsorted.OrderByDescending(dino => dino.Weight);
      Console.WriteLine();
      Console.WriteLine("The three heaviest dinosaurs in inventory are listed below:");

      // Display the details of each dinosaur in the list
      int count = 0;
      foreach (var dino in dinos)
      // for (int i = 0; i < 3; i++)
      {
        WriteDinoDetail(dino);
        count++;
        if (count == 3)
        { break; }
      }
      Console.WriteLine();
    }

    static void WriteDinoDetail(Dinosaur dino)
    {
      Console.WriteLine("__________________");
      Console.Write("Species:     "); Console.WriteLine(dino.Species);
      Console.Write("Name:        "); Console.WriteLine(dino.Name);
      Console.Write("Diet:        "); Console.WriteLine(dino.Diet);
      Console.Write("Weight:      "); Console.WriteLine($"{String.Format("{0:n0}", dino.Weight)} pounds.");
      Console.Write("Acquired:    "); Console.WriteLine(dino.DateAcquired);
      Console.Write("Enclosure #: "); Console.WriteLine(dino.EnclosureNumber);

    }

    static void DeleteDino(IEnumerable<Dinosaur> dinos)
    // This method allows user to select a dinosaur by name to delete. 
    // will delete all dinosaurs of that name.
    {
      Console.WriteLine();
      Console.WriteLine("What is the name of the dinosaur to be deleted?");
      Console.Write("Dinosaur's name: ");
      var dinoName = Console.ReadLine();
      var deleteDinos = Db.Dinosaurs
            .FirstOrDefault(dinosaur =>
            dinosaur.Name.ToLower()
            == dinoName.ToLower());
      Db.Dinosaurs.Remove(deleteDinos);
      Db.SaveChanges(); // save changes
      Console.WriteLine();
      Console.WriteLine($"All dinosaurs with the name '{dinoName}' were deleted.");
      Console.WriteLine();
    }

    static void TransferDino(IEnumerable<Dinosaur> dinos)
    {
      Console.WriteLine();
      Console.WriteLine("What is the name of the dinosaur you need to transfer to a new enclosure?");
      Console.Write("Dinosaur's name: ");
      var dinoName = Console.ReadLine();
      //   var transferDino = Dinosaurs.FirstOrDefault(dino => dino.Name.ToLower() == dinoName.ToLower());
      var transferDino = Db.Dinosaurs.FirstOrDefault(dino => dino.Name.ToLower() == dinoName.ToLower()); //xxx

      Console.WriteLine($"{transferDino.Name} is currently in enclosure # {transferDino.EnclosureNumber}.");
      Console.Write($"What is {transferDino.Name}'s new enclosure #? : ");
      var dinoEnclosure = Console.ReadLine();
      transferDino.EnclosureNumber = int.Parse(dinoEnclosure);
      Db.SaveChanges(); //xxx
      Console.WriteLine();
      Console.WriteLine($"{transferDino.Name} is now in enclosure # {transferDino.EnclosureNumber}.");
      Console.WriteLine();
    }

    static void ReleaseDino(IEnumerable<Dinosaur> dinos)
    {
      Console.WriteLine();
      Console.WriteLine("What is the name of the dinosaur you want to release?");
      Console.Write("Dinosaur's name: ");
      var dinoName = Console.ReadLine();
      var releaseDino = Db.Dinosaurs.FirstOrDefault(dino => dino.Name.ToLower() == dinoName.ToLower());
      Console.WriteLine($"{releaseDino.Name} was in enclosure # {releaseDino.EnclosureNumber}.");
      releaseDino.EnclosureNumber = 0;
      Db.SaveChanges(); //xxx
      Console.WriteLine();
      Console.WriteLine($"{releaseDino.Name} has been released.");
      Console.WriteLine();
    }

    static void DietarySummary(IEnumerable<Dinosaur> dinos)
    {
      int totalHerbavores = 0;
      int totalCarnivores = 0;

      foreach (var dino in dinos)
      {
        if (dino.Diet == "Carnivore")
        {
          totalCarnivores++;
        }
        else
        {
          totalHerbavores++;
        }
      }
      Console.WriteLine();
      Console.WriteLine("       Carnivores  |  Herbavores");
      Console.WriteLine("       ----------  |  ----------");
      Console.Write("Total:       ");
      Console.WriteLine($"{totalCarnivores}           {totalHerbavores}");

    }
    static void Main(string[] args)
    {
      // Display the welcome greeting.
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("            DMS");
      Console.WriteLine("(Dinosaur Management System)");
      Console.WriteLine(" ");
      Console.WriteLine("do NOT feed the dinosaurs");
      Console.WriteLine("  ...they eat people...");
      Console.WriteLine();
      // Initialize variables
      var input = "";
      var lowerCaseInput = "";

      // Loop on console output and input until user exits

      while ((lowerCaseInput) != "e")
      {
        //Ask the user what they want to do, and solicit input
        Console.WriteLine();
        Console.WriteLine("Select an option.");
        Console.WriteLine("...type the letter enclosed in ' ' and hit Enter/Return...");
        Console.WriteLine();
        Console.WriteLine("'A'dd | 'D'elete | 'T'ransfer | 'R'elease | 'H'atch");
        Console.WriteLine("'V'iew all | 'L'argest | 'S'ummary of Diet");
        Console.WriteLine("'N'eeds a Sheep | 'E'xit");
        Console.WriteLine();
        Console.Write("...:");
        input = Console.ReadLine();

        // evaluate input and trigger event as appropriate
        if (input.Length == 0) { input = " "; }
        lowerCaseInput = input.ToLower();
        lowerCaseInput = input[0].ToString().ToLower();
        switch (lowerCaseInput)
        {
          case "a":
            Console.WriteLine("You opted to add a dinosaur.");
            AddDino();
            break;
          case "v":
            Console.WriteLine("You opted to view all dinosaurs.");
            ViewAll();
            break;
          case "l":
            Console.WriteLine("You opted to view the heaviest dinosaurs.");
            DisplayHeaviestDinosaurs(Db.Dinosaurs);
            break;
          case "d":
            Console.WriteLine("You opted to delete a dinosaur.");
            DeleteDino(Db.Dinosaurs);
            break;
          case "t":
            Console.WriteLine("You opted to Transfer a dinosaur");
            Console.WriteLine("to a new enclosure.");
            TransferDino(Db.Dinosaurs);
            break;
          case "s":
            Console.WriteLine("You opted to view the Dietary Summary.");
            DietarySummary(Db.Dinosaurs);
            break;
          case "h":
            Console.WriteLine("An egg is hatching. Oh Joy!!!");
            Hatch();
            break;
          case "r":
            Console.WriteLine("You elected the option to release a dinosaur. That's brilliant!");
            ReleaseDino(Db.Dinosaurs);
            break;
          case "n":
            Console.WriteLine("You opted to view the Dietary Summary.");
            NeedsASheep(Db.Dinosaurs);
            break;
          case "e":
            Console.WriteLine();
            Console.WriteLine("Thank you for using the DMS.");
            Console.WriteLine();
            break;
          default:
            Console.WriteLine("You did NOT make a valid selection.");
            break;
        }

      }
    }
  }
}
