using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class NotesManager : INotesService
    {
        private readonly IRepositoryManager _manager;

        public NotesManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
        public Task AddNotes(Notes note)
        {
            return _manager.Notes.AddNotes(note);
        }

        public IEnumerable<Notes> GetAllNotes()
        {
            return _manager.Notes.GetAllNotes();
        }

        public Notes GetLastNoteByUserId(int userId)
        {
            return _manager.Notes.GetLastNoteByUserId(userId);
        }
    }
}
