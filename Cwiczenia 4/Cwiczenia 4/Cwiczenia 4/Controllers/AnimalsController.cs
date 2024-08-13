using Cwiczenia_4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia_4.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal { Category = "Dog", HairColor = "Brown", AnimalId = 1, Mass = 50.0, Name = "Dong"},
        new Animal { Category = "Cat", HairColor = "White", AnimalId = 2, Mass = 10.0, Name = "Kat" },
        new Animal { Category = "Bird",HairColor = "Pink", AnimalId = 3, Mass = 5.0, Name = "BirdIsAWord"}
    };
    
    private static List<Visit> _visits = new()
    {
        new Visit{AnimalId = 1, Cost = 50.0, Description = "Tooth removal", IdVisit = 1, VisitDate = DateTime.Parse("2024-04-10")},
        new Visit{AnimalId = 2, Cost = 10.0, Description = "Claws clipping", IdVisit = 2, VisitDate = DateTime.Parse("2023-04-10")},
        new Visit{AnimalId = 3, Cost = 100.0, Description = "Beak sharpening", IdVisit = 3, VisitDate = DateTime.Parse("2024-03-10")}
    };
    
    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.AnimalId == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        _animals.Add(animal);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.AnimalId == id);

        if (animalToEdit == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.AnimalId == id);

        if (animalToEdit == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToEdit);
        return NoContent();
    }
    
    [HttpGet("{id:int}/visits")]
    public IActionResult GetAnimalVisits(int id)
    {
        var animalVisits = _visits.Where(v => v.AnimalId == id).ToList();
        return Ok(animalVisits);
    }
    
    [HttpPost("{id:int}/visits")]
    public IActionResult CreateVisit(int id, Visit visit)
    {
        var animal = _animals.FirstOrDefault(a => a.AnimalId == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        visit.AnimalId = id;
        
        _visits.Add(visit);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    
}