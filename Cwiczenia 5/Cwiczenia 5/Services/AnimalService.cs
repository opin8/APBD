using Cwiczenia_5.Controllers;
using Cwiczenia_5.Repositories;

namespace Cwiczenia_5.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }
        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            var data = _animalRepository.GetAnimals();
            
            return data;
        }

        public int AddAnimal(Animal animal)
        {
            return _animalRepository.CreateAnimal(animal);
        }

        public int UpdateAnimal(Animal animal, int idAnimal)
        {
            return _animalRepository.UpdateAnimal(animal);
        }

        public int DeleteAnimal(int idAnimal)
        {
            return _animalRepository.DeleteAnimal(idAnimal);
        }
    }
}
