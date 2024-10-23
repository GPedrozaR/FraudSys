using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudSys.Application.Abstractions
{
	public class ResultBase
	{
		public bool IsSuccess { get; }
		public string Error { get; }

		protected ResultBase(bool isSuccess, string error)
		{
			IsSuccess = isSuccess;
			Error = error;
		}

		public static ResultBase Ok() => new ResultBase(true, null);
		public static ResultBase Fail(string error) => new ResultBase(false, error);
	}

}
