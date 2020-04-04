﻿using System;

using TraceDefense.Entities.Protos;

namespace TraceDefense.DAL.Helpers
{
	/// <summary>
	/// Helper functions for working with rounding numbers with controlled precision.
	/// Used to interpret <see cref="Region"/>
	/// </summary>
	public static class PrecisionHelper
	{
		/// <summary>
		/// Returns difference between 2 closest numbers for given precision parameter
		/// </summary>
		/// <param name="precision">Precision parameter, any integer</param>
		/// <returns>1 / 2^precision</returns>
		public static double GetStep(int precision)
		{
			return precision < 0 ? (double)(1 << (-precision)) : 1.0 / (double)(1 << precision);
		}

		/// <summary>
		/// Rounds given number and precision parameter.
		/// </summary>
		/// <param name="d">Any double number</param>
		/// <param name="precision">Precision parameter, any integer</param>
		/// <returns>Nearest number aligned with precision grid, equal to d or closer to zero</returns>
		/// <remarks>
		/// Reference: https://csharpindepth.com/articles/FloatingPoint
		/// Reference: https://jonskeet.uk/csharp/DoubleConverter.cs
		/// </remarks>
		public static double Round(double d, int precision)
		{
			long bits = BitConverter.DoubleToInt64Bits(d);

			long negative = bits & (1L << 63);
			int exponent = (int)((bits >> 52) & 0x7ffL);
			long mantissa = bits & 0xfffffffffffffL;

			int mantissaLog = 52;
			if (exponent == 0)
			{
				mantissaLog = (int)Math.Log(mantissa, 2);
			}
			else
			{
				mantissa = mantissa | (1L << 52);
			}

			int precisionShift = mantissaLog + exponent - 1075;

			int maskLength = Math.Min(precision + precisionShift, 52);

			mantissa = mantissa >> (52 - maskLength);
			mantissa = mantissa << (52 - maskLength);

			if (mantissa == 0)
			{
				exponent = 0;
			}
			long result = negative |
				((long)(exponent & 0x7ffL) << 52) |
				(mantissa & 0xfffffffffffffL);

			return BitConverter.Int64BitsToDouble(result);
		}
	}
}