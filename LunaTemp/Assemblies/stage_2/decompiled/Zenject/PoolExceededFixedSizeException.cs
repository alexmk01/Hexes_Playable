using System;

namespace Zenject
{
	[NoReflectionBaking]
	public class PoolExceededFixedSizeException : Exception
	{
		public PoolExceededFixedSizeException(string errorMessage)
			: base(errorMessage)
		{
		}
	}
}
