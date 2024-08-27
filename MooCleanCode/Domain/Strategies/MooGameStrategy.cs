using System.Text;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Strategies;

public class MooGameStrategy : IGameStrategy
{
    private readonly int largestDigitForSecret = 9;
    public string CreateSecretCode()
    {
        var randomGenerator = new Random();
        var uniqueDigits = new HashSet<int>();
        var uniqueCode = new StringBuilder();

        while (uniqueDigits.Count < Game.SecretCodeLength)
        {
            int randomDigit = randomGenerator.Next(largestDigitForSecret + 1);
            if (uniqueDigits.Add(randomDigit))
                uniqueCode.Append(randomDigit);
        }

        return uniqueCode.ToString();
    }
}