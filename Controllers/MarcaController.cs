using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC23.Models;

namespace MVC23.Controllers
{
    public class MarcaController : Controller
    {
        // GET: MarcaController

        private readonly Contexto contexto;

        public MarcaController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public ActionResult Index()
        {
            var lista = contexto.Marcas.ToList();
            return View(lista);
        }

        public ActionResult List()
        {
            List<MarcaModelo> lista = contexto.Marcas.ToList();
            return View(lista);
        }

        public ActionResult Desplegable()
        {
            ViewBag.Marcas = new SelectList(contexto.Marcas, "ID", "NomMarca");
            ViewBag.Marcas2 = contexto.Marcas.ToList();
            return View();
        }

        // GET: MarcaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarcaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarcaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MarcaModelo marca)
        {
            try
            {
                contexto.Marcas.Add(marca);
                contexto.Database.EnsureCreated();
                contexto.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: MarcaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MarcaController/Edit/5
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

        // GET: MarcaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarcaController/Delete/5
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
