using Microsoft.AspNetCore.Mvc;
using MvcPeliculasApiAWS.Models;
using MvcPeliculasApiAWS.Services;

namespace MvcPeliculasApiAWS.Controllers
{
    public class PeliculasController : Controller
    {
        private ServiceApiPeliculas service;

        public PeliculasController(ServiceApiPeliculas service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Pelicula> pelis = await this.service.GetPeliculasAsync();
            return View(pelis);
        }

        public IActionResult PeliculasActores()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PeliculasActores(string actor)
        {
            List<Pelicula> pelis = await this.service.GetPeliculasByActorAsync(actor);
            return View(pelis);
        }

        public async Task<IActionResult> Details(int idpelicula)
        {
            Pelicula peli = await this.service.FindPeliculaAsync(idpelicula);
            return View(peli);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pelicula peli)
        {
            await this.service.CreatePeliculaAsync(peli);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int idpelicula)
        {
            Pelicula peli = await this.service.FindPeliculaAsync(idpelicula);
            return View(peli);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Pelicula peli)
        {
            await this.service.UpdatePeliculaAsync(peli);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idpelicula)
        {
            await this.service.DeletePeliculaAsync(idpelicula);
            return RedirectToAction("Index");
        }
    }
}
