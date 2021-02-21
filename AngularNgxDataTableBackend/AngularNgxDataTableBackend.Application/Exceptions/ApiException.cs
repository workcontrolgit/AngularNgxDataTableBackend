﻿using System;
using System.Globalization;

namespace AngularNgxDataTableBackend.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
