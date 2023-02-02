using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC23.Models;

namespace MVC23.Controllers
{
    public class SerieController : Controller
    {
        // GET: SerieController

        public Contexto contexto { get; }

        public SerieController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public ActionResult Index()
        {
            var lista = contexto.Serie.Include(s => s.Marca).ToList();
            return View(lista);
        }

        public ActionResult List(int ID)
        {
            MarcaModelo marca = contexto.Marcas.Include("Series").FirstOrDefault(m => m.ID == ID);
            return View(marca);
        }

        public ActionResult Listado()
        {
            List<SerieModelo> lista = contexto.Serie.Include("Marca").ToList();
            return View(lista);
        }

        // GET: SerieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SerieController/Create
        public ActionResult Create()
        {
            ViewBag.MarcaID = new SelectList(contexto.Marcas, "ID", "NomMarca");
            return View();
        }

        // POST: SerieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SerieModelo serie)
        {
            try
            {
                contexto.Serie.Add(serie);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: SerieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SerieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SerieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SerieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
