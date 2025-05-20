using AWSPractice.Models;
using AWSPractice.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AWSPractice.Controllers
{
    public class PersonajesController : Controller
    {
        private Repository repo;
        public PersonajesController(Repository repo)
        {
            this.repo = repo;
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
        public async Task<IActionResult> Create(Personaje personaje)
        {
            
            await this.repo.AddPersonajeAsync(personaje.Nombre,personaje.Posicion,personaje.Fecha,personaje.Imagen);
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
