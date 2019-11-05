using System;

namespace FinancialCalculator
{
    public static class Converter
    {
        public static double USD { get; set; }
        public static double EUR { get; set; }
        public static double RUB { get; set; }
        public static string Currency { get; set; }
        public static double Result { get; set; }

        public static bool FlagConversionUSD { get; set; }
        public static bool FlagConversionEUR { get; set; }
        public static bool FlagConversionRUB { get; set; }       
        public static bool FlagConversionItselfIntoItself { get; set; }

        public static double USDToEUR { get; set; }
        public static double EURToUSD { get; set; }
        public static double USDToRUB { get; set; }
        public static double RUBToUSD { get; set; }
        public static double EURToRUB { get; set; }
        public static double RUBToEUR { get; set; }

        public static void Convert(string operation, double value)
        {
            switch (operation)
            {
                case "USDToEUR":
                    EUR += Math.Round(value * USDToEUR, 2);
                    Currency = "USD";

                    if (FlagConversionEUR == true)
                    {
                        FlagConversionItselfIntoItself = true;
                    }

                    break;
                case "EURToUSD":
                    USD += Math.Round(value * EURToUSD, 2);
                    Currency = "EUR";

                    if (FlagConversionUSD == true)
                    {
                        FlagConversionItselfIntoItself = true;
                    }
                    break;
                case "USDToRUB":
                    RUB += Math.Round(value * USDToRUB, 2);
                    Currency = "USD";

                    if (FlagConversionRUB == true)
                    {
                        FlagConversionItselfIntoItself = true;
                    }

                    break;
                case "RUBToUSD":
                    USD += Math.Round(value * RUBToUSD, 2);
                    Currency = "RUB";

                    if (FlagConversionUSD == true)
                    {
                        FlagConversionItselfIntoItself = true;
                    }

                    break;
                case "EURToRUB":
                    RUB += Math.Round(value * EURToRUB, 2);
                    Currency = "EUR";

                    if (FlagConversionRUB == true)
                    {
                        FlagConversionItselfIntoItself = true;
                    }

                    break;
                case "RUBToEUR":
                    EUR += Math.Round(value * RUBToEUR, 2);
                    Currency = "RUB";

                    if (FlagConversionEUR == true)
                    {
                        FlagConversionItselfIntoItself = true;
                    }

                    break;
                case ":USDToEUR":
                    EUR += Math.Round(value * USDToEUR, 2);
                    FlagConversionUSD = true;
                    break;
                case ":EURToUSD":
                    USD += Math.Round(value * EURToUSD, 2);
                    FlagConversionEUR = true;
                    break;
                case ":USDToRUB":
                    RUB += Math.Round(value * USDToRUB, 2);
                    FlagConversionUSD = true;
                    break;
                case ":RUBToUSD":
                    USD += Math.Round(value * RUBToUSD, 2);
                    FlagConversionRUB = true;
                    break;
                case ":EURToRUB":
                    RUB += Math.Round(value * EURToRUB, 2);
                    FlagConversionEUR = true;
                    break;
                case ":RUBToEUR":
                    EUR += Math.Round(value * RUBToEUR, 2);
                    FlagConversionRUB = true;
                    break;
                default:
                    FlagConversionItselfIntoItself = true;
                    break;
            }

            switch (Currency)
            {
                case "USD":
                    USD -= value;
                    break;
                case "EUR":
                    EUR -= value;
                    break;
                case "RUB":
                    RUB -= value;
                    break;
            }

            //switch (operation)
            //{
            //    case ":USDToEUR":
            //        EUR += Math.Round(value * USDToEUR, 2);
            //        FlagConversionUSD = true;
            //        break;
            //    case ":EURToUSD":
            //        USD += Math.Round(value * EURToUSD, 2);
            //        FlagConversionEUR = true;
            //        break;
            //    case ":USDToRUB":
            //        RUB += Math.Round(value * USDToRUB, 2);
            //        FlagConversionUSD = true;
            //        break;
            //    case ":RUBToUSD":
            //        USD += Math.Round(value * RUBToUSD, 2);
            //        FlagConversionRUB = true;
            //        break;
            //    case ":EURToRUB":
            //        RUB += Math.Round(value * EURToRUB, 2);
            //        FlagConversionEUR = true;
            //        break;
            //    case ":RUBToEUR":
            //        EUR += Math.Round(value * RUBToEUR, 2);
            //        FlagConversionRUB = true;
            //        break;
            //    default:
            //        FlagConversionItselfIntoItself = true;
            //        break;                
            //}

            //Currency = operation.Substring(operation.Length - 3, 3);
        }
    }
}
