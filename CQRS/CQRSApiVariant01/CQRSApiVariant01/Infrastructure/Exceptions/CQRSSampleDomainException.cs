using System;

namespace CQRSApiVariant01.Infrastructure.Exceptions
{
	public class CqrsSampleDomainException : Exception
	{
		public CqrsSampleDomainException()
		{
		}

		public CqrsSampleDomainException(string message)
			: base(message)
		{
		}

		public CqrsSampleDomainException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
