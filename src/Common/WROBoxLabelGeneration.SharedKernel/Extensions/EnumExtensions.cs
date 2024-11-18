using System.Reflection;

namespace WROBoxLabelGeneration.SharedKernel.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets a display friendly enum name.
        /// </summary>
        /// <param name="value">The object for which a type name will be returned.</param>
        /// <returns>The name of the given enum.</returns>
        public static string GetDisplayName(this Enum value)
        {
            string displayName = "Not Found";

            string propertyName = value.ToString();

            CustomAttributeData displayAttribute = value.GetType().GetField(propertyName).CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "DisplayAttribute");

            if (displayAttribute != null)
            {
                displayName = displayAttribute.NamedArguments.FirstOrDefault().TypedValue.Value.ToString();
            }

            return displayName;
        }
    }
}
