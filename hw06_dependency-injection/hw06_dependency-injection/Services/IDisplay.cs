using hw06_dependency_injection.Models;

namespace hw06_dependency_injection.Services
{
    public interface IDisplay
    {
        void Display(List<Note> notes);
    }
}
