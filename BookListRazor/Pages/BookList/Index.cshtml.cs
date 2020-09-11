using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
   public class IndexModel : PageModel
   {
      private readonly ApplicationDbContext _db;

      public IndexModel(ApplicationDbContext db)
      {
         _db = db;
      }

      public IEnumerable<Book> Books { get; set; }

      public async Task OnGet()
      {
         try
         {
            Books = await _db.Book.ToListAsync();
         }
         catch (Exception e)
         {
            Console.WriteLine($"Exception at OnGet, when calling _db.Book.ToListAsync(), exception message: '{e.Message}'");
         }
      }

      public async Task<IActionResult> OnPostDelete(int id)
      {
         var book = await _db.Book.FindAsync(id);
         if (book == null)
         {
            return NotFound();
         }
         _db.Book.Remove(book);
         await _db.SaveChangesAsync();

         return RedirectToPage("Index");
      }

   }
}