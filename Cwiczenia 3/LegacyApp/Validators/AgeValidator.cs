using System;

namespace LegacyApp.Validators;

public class AgeValidator
{
    public static bool IsOverMinimumAge(DateTime dateOfBirth, int minimumAge)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
        {
            age--;
        }

        return age >= minimumAge;
    }
}