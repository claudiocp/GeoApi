using System;

namespace GeoApi.Models
{
    public class GeoApiException : Exception
    {
        public GeoApiException() : base() { }
        public GeoApiException(string message) : base(message) { }
        public GeoApiException(string message, Exception innerException) : base(message, innerException) { }
    }
} 