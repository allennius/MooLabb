using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Presentation.Interfaces;

public interface IUI
{
    void PutString(string s);
    string GetString();
    void ShowToplist(IEnumerable<IPlayer> toplist);
    string GetUsername();
    T MenuSelector<T>(List<T> meny, string header);
}