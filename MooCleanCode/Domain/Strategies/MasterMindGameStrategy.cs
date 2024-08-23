using System.Text;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Strategies;

public class MasterMindGameStrategy : IGameStrategy
{

    public string MakeGoal()
    {
        var randomGenerator = new Random();
        var goal = new StringBuilder();

        while (goal.Length < 4)
        {
            int randomDigit = randomGenerator.Next(6);
            goal.Append(randomDigit);
        }

        return goal.ToString();
    }
}