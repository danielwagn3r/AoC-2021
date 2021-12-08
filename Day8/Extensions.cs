internal static class Extensions
{
    public static bool FitsOn(this string source, string target)
    {
        foreach (var c in source)
        {
            if (!target.Contains(c))
                return false;
        }

        return true;
    }
}