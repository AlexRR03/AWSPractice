using AWSPractice.Data;
using AWSPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace AWSPractice.Repositories
{
    public class Repository
    {
        private PersonajesContext context;
        public Repository(PersonajesContext context)
        {
            this.context = context;
        }
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }
        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            return await this.context.Personajes.FirstOrDefaultAsync(p => p.IdPersonaje == id);
        }
        public async Task<int> GetMaxId() 
        { 
            return await this.context.Personajes.MaxAsync(p => p.IdPersonaje);
        }
        public async Task AddPersonajeAsync(string nombre, string posicion, int fecha, string imagen)
        {
            Personaje personaje = new Personaje
            {
                IdPersonaje = await this.GetMaxId() + 1,
                Nombre = nombre,
                Posicion = posicion,
                Fecha = fecha,
                Imagen = imagen
            };
            await this.context.Personajes.AddAsync(personaje);
            await this.context.SaveChangesAsync();
        }
        public async Task UpdatePersonajeAsync(int id, string nombre, string posicion, int fecha, string imagen)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            if (personaje != null)
            {
                personaje.Nombre = nombre;
                personaje.Posicion = posicion;
                personaje.Fecha = fecha;
                personaje.Imagen = imagen;
                this.context.Personajes.Update(personaje);
                await this.context.SaveChangesAsync();
            }
        }
        public async Task DeletePersonajeAsync(int id)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            if (personaje != null)
            {
                this.context.Personajes.Remove(personaje);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
