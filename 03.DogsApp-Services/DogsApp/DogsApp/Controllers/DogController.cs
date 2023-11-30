
using System.Security.Claims;

using DogsApp.Core.Contracts;
using DogsApp.Infrastructure.Data;
using DogsApp.Infrastructure.Data.Domain;
using DogsApp.Models.Breed;
using DogsApp.Models.Dog;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DogsApp.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        private readonly IDogService _dogService;
        private readonly IBreedService _breedService;

        public DogController(IDogService dogsService, IBreedService breedService)
        {
            this._dogService = dogsService;
            this._breedService = breedService;
        }

        public IActionResult Create()
        {
            var dog = new DogCreateViewModel();
            dog.Breeds = _breedService.GetBreeds()
                .Select(c => new BreedPairViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
            return View(dog);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]DogCreateViewModel dog)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
              var createdId=  _dogService.Create(dog.Name, dog.Age, dog.BreedId, dog.Picture, currentUserId);
                if (createdId)
                {
                    return this.RedirectToAction(nameof(Index));
                }
            }
          
            return this.View();
        }
        public IActionResult Edit(int id)
        {
            
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            DogEditViewModel dog = new DogEditViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedId = item.BreedId,
                Picture=item.Picture
            };
            dog.Breeds = _breedService.GetBreeds()
                .Select(c => new BreedPairViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
            return View(dog);
        }

        [HttpPost]
        public IActionResult Edit(int id, DogEditViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _dogService.UpdateDog(id, bindingModel.Name, bindingModel.Age, bindingModel.BreedId, bindingModel.Picture);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
                
            }
            return View(bindingModel);

        }
        

        public IActionResult Delete(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName = item.Breed.Name,
                Picture = item.Picture
            };
            return View(dog);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _dogService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("Index", "Dogs");
            }
            else
            {
                return View();
            }
           
        }
            public IActionResult Success()
        {
            return this.View();

        }
        [AllowAnonymous]
        public IActionResult Index(string searchStringBreed, string searchStringName)
        {

            List<DogAllViewModel> dogs = _dogService.GetDogs( searchStringBreed,  searchStringName)
                .Select(item=> new DogAllViewModel
                {
                    Id= item.Id,
                    Name = item.Name,
                    Age= item.Age,
                    Breed= item.Breed.Name,
                    Picture= item.Picture,
                    FullName = item.Owner.FirstName + " " + item.Owner.LastName
                }).ToList();
           
            return this.View(dogs);
        }

        public IActionResult Details(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                BreedName = item.Breed.Name,
                Picture = item.Picture,
                FullName = item.Owner.FirstName + " " + item.Owner.LastName
            };
            return View(dog);
        }
        
        
    }
}
