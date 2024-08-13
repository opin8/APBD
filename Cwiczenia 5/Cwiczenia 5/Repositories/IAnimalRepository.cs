using Cwiczenia_5.Controllers;

namespace Cwiczenia_5.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals();
    int CreateAnimal(Animal animal);
    Animal GetAnimal(int idAnimal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
}