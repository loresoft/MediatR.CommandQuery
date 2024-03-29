using System;

using FluentValidation;

using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Validation;

public partial class UserLoginUpdateModelValidator
    : AbstractValidator<UserLoginUpdateModel>
{
    public UserLoginUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.EmailAddress).NotEmpty();
        RuleFor(p => p.EmailAddress).MaximumLength(256);
        RuleFor(p => p.Browser).MaximumLength(256);
        RuleFor(p => p.OperatingSystem).MaximumLength(256);
        RuleFor(p => p.DeviceFamily).MaximumLength(256);
        RuleFor(p => p.DeviceBrand).MaximumLength(256);
        RuleFor(p => p.DeviceModel).MaximumLength(256);
        RuleFor(p => p.IpAddress).MaximumLength(50);
        RuleFor(p => p.FailureMessage).MaximumLength(256);
        #endregion
    }

}
