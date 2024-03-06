namespace LawSchool.Exceptions;

public sealed class MaxGpaRangeBadRequest : BadRequestException
{
    public MaxGpaRangeBadRequest() :
        base($"The Gpa value is not valid. It must be less than 5.00 and greater or equal to 0.00")
    { }
}