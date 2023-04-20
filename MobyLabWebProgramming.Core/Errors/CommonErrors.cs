﻿using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage BookingNotFound => new(HttpStatusCode.NotFound, "Booking doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage MessageNotFound => new(HttpStatusCode.NotFound, "Message doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage PaymentNotFound => new(HttpStatusCode.NotFound, "Payment doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage PropertyNotFound => new(HttpStatusCode.NotFound, "Property doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ReviewNotFound => new(HttpStatusCode.NotFound, "Review doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}