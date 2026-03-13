using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibrebooksRazor.Helpers;

public class FormHelpers
{
	public static string GetValidationStatus (ModelStateDictionary ModelState, string PropertyName)
	{
		if (!ModelState.IsValid && ModelState.ContainsKey(PropertyName))
		{
			var state = ModelState[PropertyName]!;

			if (state.Errors.Count > 0)
			{
				return "intent-danger";
			}
		}

		return "";
	}
}
