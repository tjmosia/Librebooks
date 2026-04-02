
namespace Librebooks.CoreLib.Operations
{
	/// <summary>
	/// Returns a summary result of a transaction associated with <typeparamref name="TModel"/>.
	/// </summary>
	/// <typeparam name="TModel">Is the class type of the model instance associated with he transaction.</typeparam>
	public class Result<TModel>
		where TModel : class
	{
		public readonly bool Succeeded;
		public readonly TModel? Model;
		public readonly Error[] Errors;

		private Result (bool succeeded, Error[] errors, TModel? model)
		{
			Succeeded = succeeded;
			Model = model;
			Errors = errors;
		}

		public static Result<TModel> Success (TModel? Model = null)
			=> new(true, [], Model);

		public static Result<TModel> Failure (params Error[] errors)
			=> new(false, errors, null);

		internal void Deconstruct (out object Confirmed, out object Request)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Returns a summary result of a transaction.
	/// </summary>
	public class Result
	{
		public readonly bool Succeeded;
		public readonly Error[] Errors;
		public static readonly Result Success = new(true, []);

		private Result (bool succeeded, Error[] errors)
		{
			Succeeded = succeeded;
			Errors = errors;
		}

		public static Result Failure (params Error[] errors)
			=> new(false, errors);
	}
}
