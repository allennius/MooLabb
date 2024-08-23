using System.Text;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Strategies;

public class MooGameStrategy : IGameStrategy
{

    public string MakeGoal()
    {
        Random randomGenerator = new Random();
        HashSet<int> uniqueDigits = new HashSet<int>();
        StringBuilder uniqueGoal = new StringBuilder();

        while (uniqueDigits.Count < 4)
        {
            int randomDigit = randomGenerator.Next(10);
            if (uniqueDigits.Add(randomDigit))
                uniqueGoal.Append(randomDigit);
        }

        return uniqueGoal.ToString();
    }
}