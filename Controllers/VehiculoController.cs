using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC23.Models;

namespace MVC23.Controllers
{
    public class VehiculoController : Controller
    {

        public Contexto contexto { get; }

        public VehiculoController(Contexto contexto)
        {
            this.contexto = contexto;
        }


        // GET: VehiculoController
        public ActionResult Index()
        {
            ViewBag.lasMarcas = contexto.Marcas.ToList();
            var lista = contexto.Vehiculo.Include(v => v.Serie).ToList();
            return View(lista);
        }

        public ActionResult Listado(int ID)
        {
            ViewBag.lasMarcas = contexto.Marcas.ToList();
            SerieModelo serie = contexto.Serie.Include("Vehiculos").FirstOrDefault(s => s.ID == ID);
            return View(serie);
        }

        public ActionResult Busqueda(String cadena="")
        {
            ViewBag.cadena = cadena;
            var lista = (from v in contexto.Vehiculo where (v.Matricula.Contains(cadena)) select v).Include(v => v.Serie).ToList();
            return View(lista);
        }

        public ActionResult Seleccionar(String matricula="")
        {
            ViewBag.Matriculas = new SelectList(contexto.Vehiculo, "Matricula", "Matricula", matricula);
            var coche = (from v in contexto.Vehiculo where (v.Matricula==matricula) select v).Include(v => v.Serie);
            return View(coche);
        }

        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {
            
            VehiculoModelo vehiculo = contexto.Vehiculo.Include(v => v.Serie).FirstOrDefault(v => v.ID==id);
            return View(vehiculo);
        }

        // GET: VehiculoController/Create
        public ActionResult Create()
        {
            ViewBag.SerieID = new SelectList(contexto.Serie, "ID", "NomSerie");
            return View();
        }

        // POST: VehiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehiculoModelo vehiculo)
        {
            try
            {
                contexto.Vehiculo.Add(vehiculo);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehiculoController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SerieID = new SelectList(contexto.Serie, "ID", "NomSerie");
            VehiculoModelo vehiculo = contexto.Vehiculo.Find(id);
            return View(vehiculo);
        }

        // POST: VehiculoController/Edit/5
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

        // GET: VehiculoController/Delete/5
        public ActionResult Delete(int id)
        {
            VehiculoModelo vehiculo = contexto.Vehiculo.Include(v => v.Serie).FirstOrDefault(v => v.ID == id);
            return View(vehiculo);
        }

        // POST: VehiculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                VehiculoModelo vehiculo = contexto.Vehiculo.Find(id);
                contexto.Vehiculo.Remove(vehiculo);
                contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
