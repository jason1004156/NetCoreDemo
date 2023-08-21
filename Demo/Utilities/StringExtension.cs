using System;
using Newtonsoft.Json;
namespace Demo.Utilities
{
	public static class StringExtension
	{
		public static bool TryParseJson<T>(this string @this, out T result)
		{
			bool success = true;
			var setting = new JsonSerializerSettings
			{
				Error = (sender, args) => {
					success = false; args.ErrorContext.Handled = true;
				},
				MissingMemberHandling = MissingMemberHandling.Error
			};
			result = JsonConvert.DeserializeObject<T>(@this, setting);

			return success;
		}
	}
}

