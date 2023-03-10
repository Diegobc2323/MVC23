using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC23.Models;

namespace MVC23.Controllers
{
    public class VehiculoController : Controller
    {

        public class VehiculoTotal
        {
            public string NomMarca { get; set; }
            public string NomSerie { get; set; }
            public string Matricula { get; set; }
            public string color { get; set; }

        }

        public Contexto contexto { get; }


        public VehiculoController(Contexto contexto)
        {
            this.contexto = contexto;
        }


        // GET: VehiculoController
        public ActionResult Index()
        {
            ViewBag.lasMarcas = contexto.Marcas.ToList();
            ViewBag.Extras = contexto.Extras.ToList();
            var lista = contexto.Vehiculo.Include(v => v.Serie).Include(v =>v.VehiculoExtras).ToList();
            return View(lista);
        }

        public ActionResult Listado(int ID)
        {
            ViewBag.lasMarcas = contexto.Marcas.ToList();
            SerieModelo serie = contexto.Serie.Include("Vehiculos").FirstOrDefault(s => s.ID == ID);
            return View(serie);
        }

        public ActionResult Listado2(int marcaID=1, int serieID=1)
        {
            ViewBag.lasMarcas = new SelectList(contexto.Marcas, "ID", "NomMarca", marcaID);
            ViewBag.lasSeries = new SelectList(contexto.Serie.Where(s => s.MarcaID==marcaID), "ID", "NomSerie", serieID);

            List<VehiculoModelo> vehiculos = contexto.Vehiculo.Where(v => v.SerieID == serieID).ToList();
            return View(vehiculos);
        }

        public ActionResult Listado3(String color="%")
        {
            var elColor = new SqlParameter("@ColorSel", color);
            //List<VehiculoTotal> lista = contexto.VistaTotal.ToList();
            ViewBag.miColor = new SelectList(contexto.Vehiculo.Select(v => new { color = v.color }).Distinct(), "color", "color", color);
            //                                                                                      ARRIBA      ID, VALUE, SELECTED
            List<VehiculoTotal> lista = contexto.VistaTotal.FromSql($"EXECUTE getVehiculosPorColor {elColor}").ToList();
            //List<VehiculoTotal> lista = contexto.VistaTotal.FromSql($"SELECT Marcas.NomMarca, Serie.NomSerie, Vehiculo.Matricula, Vehiculo.color FROM Vehiculo JOIN Serie ON Serie.ID = Vehiculo.SerieID JOIN Marcas ON Marcas.ID = Serie.MarcaID WHERE Vehiculo.color like {color}").ToList();
            return View(lista);
        }

        public ActionResult Listado4()
        {
            List<VehiculoTotal> lista = contexto.VistaTotal.FromSql($"EXECUTE getSeriesVehiculos").ToList();
            return View(lista);
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
            ViewBag.VehiculoExtras = new MultiSelectList(contexto.Extras, "ID", "TipoExtra");
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

                foreach (var xtraID in vehiculo.ExtrasSeleccionados)
                {
                    var obj = new VehiculoExtraModelo() { ExtraID = xtraID, VehiculoID = vehiculo.ID };
                    contexto.VehiculosExtras.Add(obj);
                }

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
            List<int> extrasSeleccionados = new List<int>();

            extrasSeleccionados = (from ve in contexto.VehiculosExtras where (ve.VehiculoID==id) select ve.ExtraID).ToList();
            VehiculoModelo vehiculo = contexto.Vehiculo.Find(id);
            //extrasSeleccionados = vehiculo.ExtrasSeleccionados;

            ViewBag.SerieID = new SelectList(contexto.Serie, "ID", "NomSerie");
            ViewBag.VehiculoExtras = new MultiSelectList(contexto.Extras, "ID", "TipoExtra", extrasSeleccionados);
            
            return View(vehiculo);
        }

        // POST: VehiculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehiculoModelo vehiculoModificado)
        {
            try
            {

                VehiculoModelo vehiculoActual = contexto.Vehiculo.FirstOrDefault(v => v.ID==id);
                vehiculoActual.Matricula = vehiculoModificado.Matricula;
                vehiculoActual.color = vehiculoModificado.color;
                vehiculoActual.SerieID = vehiculoModificado.SerieID;
                contexto.SaveChanges();

                var extrasActuales = contexto.VehiculosExtras.Where(v => v.VehiculoID==id).ToList();

                foreach (VehiculoExtraModelo vExtra in extrasActuales)
                {

                    contexto.VehiculosExtras.Remove(vExtra);
                }

                foreach (var xtraID in vehiculoModificado.ExtrasSeleccionados)
                {
                    var obj = new VehiculoExtraModelo() { ExtraID = xtraID, VehiculoID = vehiculoModificado.ID };
                    contexto.VehiculosExtras.Add(obj);
                }
                contexto.SaveChanges();

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
