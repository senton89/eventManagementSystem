using System.Collections.Generic; // Добавлено для IEnumerable
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EventManagementSystem.APIs.Common.Attributes;

public class RegularExpressionEnumerable : RegularExpressionAttribute
{
    public RegularExpressionEnumerable(string pattern)
        : base(pattern) { }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is not IEnumerable<string> values)
            return false;

        foreach (var val in values)
        {
            if (!Regex.IsMatch(val, Pattern))
                return false;
        }

        return true;
    }
}
