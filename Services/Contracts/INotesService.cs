using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface INotesService
    {
        IEnumerable<Notes> GetAllNotes();
        Task AddNotes(Notes note);
        Notes GetLastNoteByUserId(int userId);
    }
}
