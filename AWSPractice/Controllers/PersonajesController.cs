using AWSPractice.Helper;
using AWSPractice.Models;
using AWSPractice.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AWSPractice.Controllers
{
    public class PersonajesController : Controller
    {
        private Repository repo;
        private HelperBuckets helper;
        public PersonajesController(Repository repo, HelperBuckets helper)
        {
            this.repo = repo;
            this.helper = helper;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.repo.GetPersonajesAsync();

            return View(personajes);
        }
        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje = await this.repo.FindPersonajeAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            return View(personaje);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje, IFormFile imagen)
        {
            
            await this.repo.AddPersonajeAsync(personaje.Nombre,personaje.Posicion,personaje.Fecha,imagen);
            return RedirectToAction("Index");    
        }
        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.repo.FindPersonajeAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            return View(personaje);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Personaje personaje)
        {
            await this.repo.UpdatePersonajeAsync(id, personaje.Nombre, personaje.Posicion, personaje.Fecha, personaje.Imagen);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeletePersonajeAsync(id);
            return RedirectToAction("Index");
        }

    }
}
