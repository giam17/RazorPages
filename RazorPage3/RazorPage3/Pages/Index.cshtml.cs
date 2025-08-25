using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage3.Models;
using System.Text.Json;

namespace RazorPage3.Pages
{
    public class IndexModel : PageModel
    {
        public List<Tarea> Tareas { get; private set; } = new();
        public int PaginaActual { get; private set; }
        public int TotalPaginas { get; private set; }
        public int TamanoPagina { get; private set; } = 5;

        public string Filtro { get; private set; } = "todos";

        public void OnGet(int pagina = 1, string? filtro = "todos", int? tamano = null)
        {
            Filtro = string.IsNullOrWhiteSpace(filtro) ? "todos" : filtro;
            if (tamano.HasValue && tamano.Value > 0) TamanoPagina = tamano.Value;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tareas.json");
            var json = System.IO.File.ReadAllText(path);
            var todasLasTareas = JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();

            IEnumerable<Tarea> tareasFiltradas = todasLasTareas;
            string Norm(string? s) => (s ?? "").Trim();

            switch (Filtro.ToLowerInvariant())
            {
                case "finalizado":
                    tareasFiltradas = todasLasTareas.Where(t => Norm(t.estado) == "Finalizado");
                    break;
                case "pendiente":
                    tareasFiltradas = todasLasTareas.Where(t => Norm(t.estado) == "Pendiente");
                    break;
                case "en curso":
                    tareasFiltradas = todasLasTareas.Where(t => Norm(t.estado) == "En curso");
                    break;
                case "todos":
                default:
                    tareasFiltradas = todasLasTareas;
                    break;
            }

            PaginaActual = pagina < 1 ? 1 : pagina;
            var total = tareasFiltradas.Count();
            TotalPaginas = (int)Math.Ceiling(total / (double)TamanoPagina);
            if (TotalPaginas == 0) TotalPaginas = 1;
            if (PaginaActual > TotalPaginas) PaginaActual = TotalPaginas;

            Tareas = tareasFiltradas
                .Skip((PaginaActual - 1) * TamanoPagina)
                .Take(TamanoPagina)
                .ToList();
        }

        public string Badge(string? estado)
        {
            switch ((estado ?? "").Trim().ToLowerInvariant())
            {
                case "finalizado":
                    return "badge bg-success";
                case "pendiente":
                    return "badge bg-warning text-dark";
                case "en curso":
                    return "badge bg-info";
                default:
                    return "badge bg-secondary";
            }
        }
    }
}
