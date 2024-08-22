using System.Text;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Strategies;

public class MooGameStrategy : IGameStrategy
{

    public string MakeGoal()
    {
        Random randomGenerator = new();
        HashSet<int> uniqueDigits = new();
        StringBuilder goal = new();

        while (uniqueDigits.Count < 4)
        {
            int randomDigit = randomGenerator.Next(10);
            if (uniqueDigits.Add(randomDigit))
                goal.Append(randomDigit);
        }

        return goal.ToString();
    }
}