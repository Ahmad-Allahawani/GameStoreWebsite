using aspPro2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspPro2.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index ()
        {
            return View(await _context.gameInfo.ToListAsync());
        }






        public IActionResult Create()
        {
            return View();
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create (Games game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);


        }







        //get action for search

        public IActionResult Search()
        {
            return View();
        }
        //post action for search


        [HttpPost, ActionName("search")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string? search)
        {
            if (search != null)
            {
                var Game = await _context.gameInfo.FirstOrDefaultAsync(s => s.Name == search);
                return View($"{nameof(SearchedGames)}", Game);
                
                //if there is a null value if statment

                //if (Game != null) 
                //{
                //    return View($"{nameof(SearchedGames)}", Game);


                //}
                //else
                //{
                //    return NotFound();
                //}
            }

            else
            {
                return NotFound();
            }


        }

        public async Task<IActionResult> SearchedGames(int? id)
        {
            if (id != null)
            {
                var game = await _context.users.FirstOrDefaultAsync(m => m.Id == id);
                return View(game);

            }
            else
            {
                return NotFound();
            }

        }
    }
}
