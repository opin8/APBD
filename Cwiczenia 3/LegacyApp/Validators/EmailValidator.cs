namespace LegacyApp.Validators;

public class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }
}