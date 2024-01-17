using System.Text.RegularExpressions;

namespace OriginConsole.Utils;

public static class StringExtensions
{

    public static string FormatCard(string cardNumber)
    {
        var formattedCard = Regex.Replace(cardNumber, ".{4}", "$0-");
        var removeLastCharacter = formattedCard.Remove(formattedCard.Length - 1);

        return removeLastCharacter;
    }
}