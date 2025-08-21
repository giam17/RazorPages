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
        public int TamanoPagina { get; private set; } = 8;

        public void OnGet(int pagina = 1)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tareas.json");
            var json = System.IO.File.ReadAllText(path);
            var todas = JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();
            todas = todas.OrderBy(t => t.fechaVencimiento).ToList();

            var total = todas.Count;
            TotalPaginas = Math.Max(1, (int)Math.Ceiling(total / (double)TamanoPagina));
            PaginaActual = Math.Min(Math.Max(1, pagina), TotalPaginas);

            Tareas = todas
                .Skip((PaginaActual - 1) * TamanoPagina)
                .Take(TamanoPagina)
                .ToList();
        }

        public string Badge(string? estado)
        {
            return estado?.ToLowerInvariant() switch
            {
                "completada" => "badge bg-success",
                "pendiente" => "badge bg-warning text-dark",
                "en curso" => "badge bg-info",
                "cancelada" => "badge bg-danger",
                _ => "badge bg-secondary"
            };
        }
    }
}
