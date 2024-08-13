namespace LegacyApp.Validators;

public class NameValidator
{
    public static bool IsValidName(string firstName, string lastName)
    {
        return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
    }
}