using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotaTest.Models;

namespace NotaTest.Controllers
{
    public class NotaController : Controller
    {

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

            var notas = from s in _context.Nota
                        select s;

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
        [HttpGet]
        public IActionResult Nota_Detalle(int idNota)
        {
            NotaModel notaModel = new NotaModel();

            if(idNota != 0)
            {
                notaModel = _context.Nota.Find(idNota);
            }

            return View(notaModel);
        }

        //recibe el objeto para poder guardarlo en bd
        [HttpPost]
        public IActionResult Nota_Detalle(NotaModel oNota)
        {

            if (!ModelState.IsValid)
                return View();
            
            
            if(oNota.IdNota == 0)
            {

                _context.Nota.Add(oNota);

            }
            else
            {
                _context.Nota.Update(oNota);
            }

            _context.SaveChanges();

             return RedirectToAction("Listar", "Nota");
   

        }


        //metodo recibe el objeto seleccionado para la vista
        [HttpGet]
        public IActionResult Eliminar(int IdNota)
        {

            NotaModel notaModel = _context.Nota.Where(x => x.IdNota == IdNota).FirstOrDefault();
            return View(notaModel);
        }

        [HttpPost]
        //metodo recibe el objeto para eliminar
        public IActionResult Eliminar(NotaModel oNota)
        {

            _context.Nota.Remove(oNota);

            _context.SaveChanges();

            return RedirectToAction("Listar", "Nota");
        }
    }
}
