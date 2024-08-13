using Cwiczenia_5.Controllers;

namespace Cwiczenia_5.Services
{
    public interface IAnimalService
    {
        public IEnumerable<Animal> GetAnimals(string orderBy);

        public int AddAnimal(Animal animal);

        public int UpdateAnimal(Animal animal, int idAnimal);

        public int DeleteAnimal(int idAnimal);

    }
}