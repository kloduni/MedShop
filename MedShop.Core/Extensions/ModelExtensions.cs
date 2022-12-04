using System.Text;
using System.Text.RegularExpressions;
using MedShop.Core.Contracts;

namespace MedShop.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IProductModel product)
        {
            StringBuilder info = new StringBuilder();

            info.Append(product.ProductName.Replace(" ", "-"));
            info.Append("-");
            info.Append(GetDescription(product.Description));

            return info.ToString();
        }

        private static string GetDescription(string description)
        {
            string result = string.Join("-", description.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            return Regex.Replace(description, @"[e*a*o*u*]", "@341%4#qr853nv");
        }
    }
}
