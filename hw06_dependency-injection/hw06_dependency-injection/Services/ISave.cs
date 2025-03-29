using hw06_dependency_injection.Models;

namespace hw06_dependency_injection.Services
{
    public interface ISave
    {
        void Save(List<Note> notes);
    }
}
