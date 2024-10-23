namespace FraudSys.Application.Abstractions
{
	public class Result<T> : ResultBase
	{
		public T Value { get; }

		private Result(bool isSuccess, T value, string error) : base(isSuccess, error)
		{
			Value = value;
		}

		public static new Result<T> Ok(T value) => new Result<T>(true, value, null);
		public static new Result<T> Fail(string error) => new Result<T>(false, default, error);
	}
}
