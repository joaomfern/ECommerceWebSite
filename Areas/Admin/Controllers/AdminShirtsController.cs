using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceProject.Context;
using EcommerceProject.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace EcommerceProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminShirtsController : Controller
    {
        private readonly AppDbContext _context;

        public AdminShirtsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminShirts
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Shirts.Include(s => s.Categoria);
        //    return View(await appDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            // Asnotracking permite melhor desempenho pois as entidades nao sao rastreadas pelo contexto (só pode ser usado para ler, nao para alterar)
            var resultado = _context.Shirts.Include(s=>s.Categoria).AsQueryable();

            //se tiver algum filtro a ser passado inclui na pesquisa
            if (!string.IsNullOrEmpty(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }
            //define paginação com o filtro
            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);

        }



        // GET: Admin/AdminShirts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shirts == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts
                .Include(s => s.Categoria)
                .FirstOrDefaultAsync(m => m.ShirtId == id);
            if (shirt == null)
            {
                return NotFound();
            }

            return View(shirt);
        }

        // GET: Admin/AdminShirts/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        // POST: Admin/AdminShirts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShirtId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsFavourite,EmStock,CategoriaId")] Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shirt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", shirt.CategoriaId);
            return View(shirt);
        }

        // GET: Admin/AdminShirts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shirts == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts.FindAsync(id);
            if (shirt == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", shirt.CategoriaId);
            return View(shirt);
        }

        // POST: Admin/AdminShirts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShirtId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsFavourite,EmStock,CategoriaId")] Shirt shirt)
        {
            if (id != shirt.ShirtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shirt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShirtExists(shirt.ShirtId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", shirt.CategoriaId);
            return View(shirt);
        }

        // GET: Admin/AdminShirts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shirts == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts
                .Include(s => s.Categoria)
                .FirstOrDefaultAsync(m => m.ShirtId == id);
            if (shirt == null)
            {
                return NotFound();
            }

            return View(shirt);
        }

        // POST: Admin/AdminShirts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shirts == null)
            {
                return Problem("Entity set 'AppDbContext.Shirts'  is null.");
            }
            var shirt = await _context.Shirts.FindAsync(id);
            if (shirt != null)
            {
                _context.Shirts.Remove(shirt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShirtExists(int id)
        {
          return _context.Shirts.Any(e => e.ShirtId == id);
        }
    }
}
