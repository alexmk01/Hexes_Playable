namespace ModestTree.Util
{
	public class ValuePair<T1, T2>
	{
		public readonly T1 First;

		public readonly T2 Second;

		public ValuePair()
		{
			First = default(T1);
			Second = default(T2);
		}

		public ValuePair(T1 first, T2 second)
		{
			First = first;
			Second = second;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ValuePair<T1, T2> that))
			{
				return false;
			}
			return Equals(that);
		}

		public bool Equals(ValuePair<T1, T2> that)
		{
			if (that == null)
			{
				return false;
			}
			return object.Equals(First, that.First) && object.Equals(Second, that.Second);
		}

		public override int GetHashCode()
		{
			int hash = 17;
			int num = hash * 29;
			int num2;
			if (First != null)
			{
				T1 first = First;
				num2 = first.GetHashCode();
			}
			else
			{
				num2 = 0;
			}
			hash = num + num2;
			int num3 = hash * 29;
			int num4;
			if (Second != null)
			{
				T2 second = Second;
				num4 = second.GetHashCode();
			}
			else
			{
				num4 = 0;
			}
			return num3 + num4;
		}
	}
	public class ValuePair<T1, T2, T3>
	{
		public readonly T1 First;

		public readonly T2 Second;

		public readonly T3 Third;

		public ValuePair()
		{
			First = default(T1);
			Second = default(T2);
			Third = default(T3);
		}

		public ValuePair(T1 first, T2 second, T3 third)
		{
			First = first;
			Second = second;
			Third = third;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ValuePair<T1, T2, T3> that))
			{
				return false;
			}
			return Equals(that);
		}

		public bool Equals(ValuePair<T1, T2, T3> that)
		{
			if (that == null)
			{
				return false;
			}
			return object.Equals(First, that.First) && object.Equals(Second, that.Second) && object.Equals(Third, that.Third);
		}

		public override int GetHashCode()
		{
			int hash = 17;
			int num = hash * 29;
			int num2;
			if (First != null)
			{
				T1 first = First;
				num2 = first.GetHashCode();
			}
			else
			{
				num2 = 0;
			}
			hash = num + num2;
			int num3 = hash * 29;
			int num4;
			if (Second != null)
			{
				T2 second = Second;
				num4 = second.GetHashCode();
			}
			else
			{
				num4 = 0;
			}
			hash = num3 + num4;
			int num5 = hash * 29;
			int num6;
			if (Third != null)
			{
				T3 third = Third;
				num6 = third.GetHashCode();
			}
			else
			{
				num6 = 0;
			}
			return num5 + num6;
		}
	}
	public class ValuePair<T1, T2, T3, T4>
	{
		public readonly T1 First;

		public readonly T2 Second;

		public readonly T3 Third;

		public readonly T4 Fourth;

		public ValuePair()
		{
			First = default(T1);
			Second = default(T2);
			Third = default(T3);
			Fourth = default(T4);
		}

		public ValuePair(T1 first, T2 second, T3 third, T4 fourth)
		{
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ValuePair<T1, T2, T3, T4> that))
			{
				return false;
			}
			return Equals(that);
		}

		public bool Equals(ValuePair<T1, T2, T3, T4> that)
		{
			if (that == null)
			{
				return false;
			}
			return object.Equals(First, that.First) && object.Equals(Second, that.Second) && object.Equals(Third, that.Third) && object.Equals(Fourth, that.Fourth);
		}

		public override int GetHashCode()
		{
			int hash = 17;
			int num = hash * 29;
			int num2;
			if (First != null)
			{
				T1 first = First;
				num2 = first.GetHashCode();
			}
			else
			{
				num2 = 0;
			}
			hash = num + num2;
			int num3 = hash * 29;
			int num4;
			if (Second != null)
			{
				T2 second = Second;
				num4 = second.GetHashCode();
			}
			else
			{
				num4 = 0;
			}
			hash = num3 + num4;
			int num5 = hash * 29;
			int num6;
			if (Third != null)
			{
				T3 third = Third;
				num6 = third.GetHashCode();
			}
			else
			{
				num6 = 0;
			}
			hash = num5 + num6;
			int num7 = hash * 29;
			int num8;
			if (Fourth != null)
			{
				T4 fourth = Fourth;
				num8 = fourth.GetHashCode();
			}
			else
			{
				num8 = 0;
			}
			return num7 + num8;
		}
	}
	public static class ValuePair
	{
		public static ValuePair<T1, T2> New<T1, T2>(T1 first, T2 second)
		{
			return new ValuePair<T1, T2>(first, second);
		}

		public static ValuePair<T1, T2, T3> New<T1, T2, T3>(T1 first, T2 second, T3 third)
		{
			return new ValuePair<T1, T2, T3>(first, second, third);
		}

		public static ValuePair<T1, T2, T3, T4> New<T1, T2, T3, T4>(T1 first, T2 second, T3 third, T4 fourth)
		{
			return new ValuePair<T1, T2, T3, T4>(first, second, third, fourth);
		}
	}
}
