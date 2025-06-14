namespace BestArchitecture.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string ToFlag<TEnum>(this TEnum value) where TEnum : Enum => value.ToString();
    }

}
