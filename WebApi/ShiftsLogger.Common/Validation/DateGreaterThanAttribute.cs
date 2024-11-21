﻿using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Common.Validation;
public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var currentValue = (DateTime)value;

        var comparisonValue = (DateTime)validationContext.ObjectType
            .GetProperty(_comparisonProperty)
            .GetValue(validationContext.ObjectInstance);

        if (currentValue < comparisonValue)
        {
            return new ValidationResult(ErrorMessage = "End date must be later than start date!");
        }

        return ValidationResult.Success;
    }
}
