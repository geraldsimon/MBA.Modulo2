namespace MBA.Modulo2.Data.Domain.Extensions;

public static class EnumExtensions
{
	public static string ToDescription(this Enum value)
	{
		var field = value.GetType().GetField(value.ToString());

		if (field == null) return value.ToString();

		return field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
			.FirstOrDefault() is System.ComponentModel.DescriptionAttribute attribute
			? attribute.Description
			: value.ToString();
	}
}