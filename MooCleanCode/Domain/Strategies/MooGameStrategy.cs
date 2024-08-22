using System.Text;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Strategies;

public class MooGameStrategy : IGameStrategy
{

    public string MakeGoal()
    {
        Random randomGenerator = new();
        HashSet<int> uniqueDigits = new();
        StringBuilder uniqueGoal = new();

        while (uniqueDigits.Count < 4)
        {
            int randomDigit = randomGenerator.Next(10);
            if (uniqueDigits.Add(randomDigit))
                uniqueGoal.Append(randomDigit);
        }

        return uniqueGoal.ToString();
    }
}