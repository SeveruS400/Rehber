using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class NotesRepository : RepositoryBase<Notes>, INotesRepository
    {
        public NotesRepository(DataContext context) : base(context)
        {
        }

        public async Task AddNotes(Notes note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public IEnumerable<Notes> GetAllNotes()
        {
            var Notes = _context.Notes.ToList();

            return Notes;
        }

        public Notes GetLastNoteByUserId(int userId)
        {
            return _context.Notes
                .Where(note => note.ProductId == userId)
                .OrderByDescending(note => note.RequestCreateTime)
                .FirstOrDefault();
        }
    }
}
