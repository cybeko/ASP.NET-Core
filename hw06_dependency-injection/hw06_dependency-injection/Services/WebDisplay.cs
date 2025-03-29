using hw06_dependency_injection.Models;

namespace hw06_dependency_injection.Services
{
    public class WebDisplay : IDisplay
    {
        public void Display(List<Note> notes)
        {
            foreach (var note in notes)
            {
                Console.WriteLine($"{note.Name}: {note.Text}");
            }
        }
    }
}
