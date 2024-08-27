using System.Text;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Strategies;

public class MasterMindGameStrategy : IGameStrategy
{
    private readonly int largestDigitForSecret = 5;
    public string CreateSecretCode()
    {
        var randomGenerator = new Random();
        var code = new StringBuilder();

        while (code.Length < Game.SecretCodeLength)
        {
            int randomDigit = randomGenerator.Next(largestDigitForSecret + 1);
            code.Append(randomDigit);
        }

        return code.ToString();
    }
}