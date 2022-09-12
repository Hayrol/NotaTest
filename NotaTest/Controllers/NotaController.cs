using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaTest.Datos;
using NotaTest.Models;

namespace NotaTest.Controllers
{
    public class NotaController : Controller
    {
        NotaData _NotaData = new NotaData();

        private readonly DBTESTContext _context;

        public NotaController(DBTESTContext context)
        {
            _context = context;
        }

        //Mostrara una lista de notas
        public async Task<IActionResult> Listar(string sortOrder, string buscar)
        {
            //Procedimiento para ordenar por columna
            ViewData["TituloSortParm"] = String.IsNullOrEmpty(sortOrder) ? "titulo_desc" : "";
            ViewData["CuerpoSortParm"] = sortOrder == "cuerpo_asc" ? "cuerpo_desc" : "cuerpo_asc";
            ViewData["FechaSortParm"] = sortOrder == "fecha_asc" ? "fecha_desc" : "fecha_asc";

            var notas = from s in _context.Nota select s;

            switch (sortOrder)
            {
                case "titulo_desc":
                    notas = notas.OrderByDescending(s => s.Titulo);
                    break;

                case "cuerpo_desc":
                    notas = notas.OrderByDescending(s => s.Cuerpo);
                    break;

                case "cuerpo_asc":
                    notas = notas.OrderBy(s => s.Cuerpo);
                    break;

                case "fecha_desc":
                    notas = notas.OrderByDescending(s => s.Fecha);
                    break;

                case "fecha_asc":
                    notas = notas.OrderBy(s => s.Fecha);
                    break;

                default:
                    notas = notas.OrderBy(s => s.Titulo);
                    break;
            }
            //Procedimiento de la busqueda
            if (!String.IsNullOrEmpty(buscar))
            {
                notas = notas.Where(s => s.Titulo!.Contains(buscar) || s.Cuerpo!.Contains(buscar)); 
            }

            return View(await notas.AsNoTracking().ToListAsync());

        }

        //metodo solo devuelve la vista para guardar
        public IActionResult Guardar()
        {
            return View();
        }

        //recibe el objeto para poder guardarlo en bd
        [HttpPost]
        public IActionResult Guardar(NotaModel oNota)
        {

            if (!ModelState.IsValid)
                return View();

            var respuesta = _NotaData.Guardar(oNota);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();

        }

        //metodo solo devuelve la vista
        public IActionResult Editar(int IdNota)
        {

            var oNota = _NotaData.ObtenerPorId(IdNota);
            return View(oNota);
        }

        [HttpPost]
        //metodo recibe el objeto para editar
        public IActionResult Editar(NotaModel oNota)
        {

            if (!ModelState.IsValid)
                return View();

            var respuesta = _NotaData.Editar(oNota);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        //metodo recibe el objeto seleccionado para la vista
        public IActionResult Eliminar(int IdNota)
        {

            var oNota = _NotaData.ObtenerPorId(IdNota);
            return View(oNota);
        }

        [HttpPost]
        //metodo recibe el objeto para eliminar
        public IActionResult Eliminar(NotaModel oNota)
        {

            var respuesta = _NotaData.Eliminar(oNota.IdNota);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
