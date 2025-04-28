
using System.Text.RegularExpressions;

namespace PersonSrv.Domain.Entities;


public class Person
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalCode { get; private set; }
    public DateTime BirthDate { get; private set; }
    protected Person()
    {
        
    }

    private Person(string name, string lastName, string nationalCode, DateTime birthDate)
    {
        Id = Guid.NewGuid();
        FirstName = name ?? throw new ArgumentNullException(nameof(name));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        NationalCode = ValidateNationalCode(nationalCode);
        BirthDate = ValidateBirthDate(birthDate);
    }

    public static Person Create(string name, string lastName, string nationalCode, DateTime birthDate)
    {
        return new Person(
            name,
            lastName,
            nationalCode,
            birthDate
        );
    }

    public void Update(string name, string lastName, string nationalCode, DateTime birthDate)
    {
        FirstName = name ?? throw new ArgumentNullException(nameof(name));
        LastName= lastName ?? throw new ArgumentNullException(nameof(lastName));
        NationalCode = ValidateNationalCode(nationalCode);
        BirthDate = ValidateBirthDate(birthDate);
    }

    private DateTime ValidateBirthDate(DateTime birthDate)
    {
        if (birthDate >= DateTime.Today)
            throw new ArgumentException("Birth date must be in the past.", nameof(birthDate));
        return birthDate;
    }

    private string ValidateNationalCode(string nationalCode)
    {
        if (nationalCode == null)
            throw new ArgumentNullException(nameof(nationalCode));
        if (nationalCode.Length != 10)
            throw new ArgumentException("National code must be exactly 10 digits.", nameof(nationalCode));
        if (!Regex.IsMatch(nationalCode, @"^[0-9]+$"))
            throw new ArgumentException("National code must contain only digits.", nameof(nationalCode));

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += int.Parse(nationalCode[i].ToString()) * (10 - i);
        int remainder = sum % 11;
        int checkDigit = int.Parse(nationalCode[9].ToString());
        if (remainder < 2 && checkDigit != remainder || remainder >= 2 && checkDigit != 11 - remainder)
            throw new ArgumentException("Invalid national code checksum.", nameof(nationalCode));

        return nationalCode;
    }
}
