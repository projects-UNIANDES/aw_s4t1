using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aw_s4t1.Models;

namespace aw_s4t1.Controllers
{
    public class DetallesVentaController : Controller
    {
        private readonly FerreteriaDbContext _context;

        public DetallesVentaController(FerreteriaDbContext context)
        {
            _context = context;
        }

        // GET: DetallesVenta
        public async Task<IActionResult> Index()
        {
            var ferreteriaDbContext = _context.DetallesVenta.Include(d => d.Producto).Include(d => d.Venta);
            return View(await ferreteriaDbContext.ToListAsync());
        }

        // GET: DetallesVenta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetallesVenta
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // GET: DetallesVenta/Create
        public IActionResult Create()
        {
            CargarListas();
            return View(new DetalleVenta { Cantidad = 1, Descuento = 0 });
        }

        // POST: DetallesVenta/Create
        // PrecioUnitario is not bound: it is always taken from the selected product.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VentaId,ProductoId,Cantidad,Descuento")] DetalleVenta detalleVenta)
        {
            await AsignarPrecioAsync(detalleVenta);

            if (ModelState.IsValid)
            {
                _context.Add(detalleVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CargarListas(detalleVenta);
            return View(detalleVenta);
        }

        // GET: DetallesVenta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetallesVenta.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            CargarListas(detalleVenta);
            return View(detalleVenta);
        }

        // POST: DetallesVenta/Edit/5
        // PrecioUnitario is not bound: it is kept or taken from the selected product.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VentaId,ProductoId,Cantidad,Descuento")] DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.Id)
            {
                return NotFound();
            }

            var original = await _context.DetallesVenta.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
            if (original == null)
            {
                return NotFound();
            }

            if (original.ProductoId == detalleVenta.ProductoId)
            {
                // Same product: keep the price it was sold at.
                detalleVenta.PrecioUnitario = original.PrecioUnitario;
                ModelState.Remove(nameof(DetalleVenta.PrecioUnitario));
            }
            else
            {
                await AsignarPrecioAsync(detalleVenta);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentaExists(detalleVenta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            CargarListas(detalleVenta);
            return View(detalleVenta);
        }

        // GET: DetallesVenta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetallesVenta
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // POST: DetallesVenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleVenta = await _context.DetallesVenta.FindAsync(id);
            if (detalleVenta != null)
            {
                _context.DetallesVenta.Remove(detalleVenta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetallesVenta.Any(e => e.Id == id);
        }

        // The unit price always comes from the product, never from the form.
        private async Task AsignarPrecioAsync(DetalleVenta detalleVenta)
        {
            var producto = await _context.Productos.FindAsync(detalleVenta.ProductoId);
            if (producto == null)
            {
                ModelState.AddModelError(nameof(DetalleVenta.ProductoId), "Seleccione un producto válido");
            }
            else
            {
                detalleVenta.PrecioUnitario = producto.Precio;
            }
            ModelState.Remove(nameof(DetalleVenta.PrecioUnitario));
        }

        private void CargarListas(DetalleVenta detalleVenta = null)
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos.OrderBy(p => p.Nombre), "Id", "Nombre", detalleVenta?.ProductoId);
            // Each sale is listed as "Numero - Cliente" so it is easy to identify.
            var ventas = _context.Ventas
                .OrderBy(v => v.Numero)
                .Select(v => new { v.Id, Texto = v.Numero + " - " + v.Cliente.Nombres })
                .ToList();
            ViewData["VentaId"] = new SelectList(ventas, "Id", "Texto", detalleVenta?.VentaId);
            // Product prices so the form can show the price of the selected product.
            ViewData["PreciosProductos"] = _context.Productos.ToDictionary(p => p.Id, p => p.Precio);
        }
    }
}
