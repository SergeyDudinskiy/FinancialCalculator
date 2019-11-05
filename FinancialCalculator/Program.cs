using System;

namespace FinancialCalculator
{
    class Program
    {
        static string message = ""; //будет хранить собщение об ошибке
        static int countCallMain = 0; //кол-во вызовов Main()

        static void Main()
        {
            Console.Title = "Financial calculator";
            Converter.FlagConversionUSD = false; //флаг показывающий проводилась ли конвертация валюты
            Converter.FlagConversionEUR = false;
            Converter.FlagConversionRUB = false;
            Converter.FlagConversionItselfIntoItself = false; //флаг показывающий проводилась ли конвертация валюты саму в себя

            if (countCallMain == 0) //чтобы не грузились данные из файла по несколько раз
            {
                countCallMain++;
                message = XmlData.GetRate(); //загружаем курсы валют
            }

            if (message == "") //если нет ошибок при чтении курса валют из файла
            {
                Console.Write("Входное выражение:");
                string input = Console.ReadLine().Replace(" ", ""); //входное выражение без пробелов

                if (input != "")
                {
                    Converter.Result = Converter.USD = Converter.EUR = Converter.RUB = 0; //начальные значения валют и суммы 
                    string currentNumber = ""; //текущее число
                    char currentSign = '+'; //текущий знак текущего числа
                    bool flag = true; //флаг для определения ошибок
                    int index = 0; //индекс начального символа конечной операции

                    try
                    {
                        for (int i = 0; i < input.Length; i++)
                        {
                            if (input[i] == '+' || input[i] == '-')
                            {
                                currentSign = input[i];
                                currentNumber = input[i].ToString();
                            }
                            else if (char.IsDigit(input[i]) == true)
                            {
                                currentNumber += input[i];
                            }
                            else if (i != 0 && input[i] == ',' && char.IsDigit(input[i - 1]) == true)
                            {
                                currentNumber += input[i];
                            }
                            else if (input.Length >= 3 && input.Substring(i, 3) == "eur")
                            {
                                Converter.EUR += Convert.ToDouble(currentNumber);
                                Converter.Currency = "EUR";
                                i += 2;
                            }
                            else if (input[i] == '$')
                            {
                                Converter.USD += Convert.ToDouble(currentNumber);
                                Converter.Currency = "USD";
                            }
                            else if (input[i] == 'r')
                            {
                                Converter.RUB += Convert.ToDouble(currentNumber);
                                Converter.Currency = "RUB";
                            }
                            else if (input.Length >= i + 6 && input.Substring(i, 6) == ":ToRub")
                            {
                                Converter.Convert(":" + Converter.Currency + "ToRUB", Convert.ToDouble(currentNumber));

                                if (Converter.FlagConversionItselfIntoItself == true)
                                {
                                    break;
                                }

                                i += 5;
                            }
                            else if (input.Length >= i + 7 && input.Substring(i, 7) == ":ToEuro")
                            {
                                Converter.Convert(":" + Converter.Currency + "ToEUR", Convert.ToDouble(currentNumber));

                                if (Converter.FlagConversionItselfIntoItself == true)
                                {
                                    break;
                                }

                                i += 6;
                            }
                            else if (input.Length >= i + 9 && input.Substring(i, 9) == ":ToDollar")
                            {
                                Converter.Convert(":" + Converter.Currency + "ToUSD", Convert.ToDouble(currentNumber));

                                if (Converter.FlagConversionItselfIntoItself == true)
                                {
                                    break;
                                }

                                i += 8;
                            }
                            else if (i + 1 != input.Length && input[i] == ',' && input[i + 1] == 'T')
                            {
                                index = i + 1;
                                break;
                            }
                        }
                    }                    
                    catch
                    {
                        flag = false;
                    }                    

                    string currencySign = "";
                    double temp = 0; //на случай если будет только 1 валюта

                    if (index != 0 && flag == true)
                    {
                        if (input.Length >= index + 5 && input.Substring(index, 5) == "ToRub")
                        {
                            temp = Converter.RUB; //на случай если будет только 1 валюта
                            Converter.Convert("USDToRUB", Converter.USD);
                            Converter.Convert("EURToRUB", Converter.EUR);
                            Converter.Result = Converter.RUB;
                            currencySign = "r";
                        }
                        else if (input.Length >= index + 6 && input.Substring(index, 6) == "ToEuro")
                        {
                            temp = Converter.EUR; //на случай если будет только 1 валюта
                            Converter.Convert("USDToEUR", Converter.USD);
                            Converter.Convert("RUBToEUR", Converter.RUB);
                            Converter.Result = Converter.EUR;
                            currencySign = "eur";
                        }
                        else if (input.Length >= index + 8 && input.Substring(index, 8) == "ToDollar")
                        {
                            temp = Converter.USD; //на случай если будет только 1 валюта
                            Converter.Convert("RUBToUSD", Converter.RUB);
                            Converter.Convert("EURToUSD", Converter.EUR);
                            Converter.Result = Converter.USD;
                            currencySign = "$";
                        }
                        else
                        {
                            flag = false;
                        }
                    }                   

                    if (flag == false)
                    {
                        Console.WriteLine("Ошибка во введённом выражении!");
                    }
                    else if (temp == Converter.Result || Converter.FlagConversionItselfIntoItself == true)
                    {
                        Console.WriteLine("Конвертация валюты саму в себя недопустимо!");
                    }
                    else if (Converter.FlagConversionItselfIntoItself == false )
                    {
                        Console.WriteLine("Результат: " + Converter.Result + currencySign);
                        message = XmlData.SaveData(input, Converter.Result.ToString() + currencySign);

                        if (message != "")
                        {
                            Console.WriteLine(message);
                        }
                    }                                       
                }
                else
                {
                    Console.WriteLine("Вы не ввели выражение!");
                }

                Console.WriteLine("\nДля продолжения работы программы нажмите ENTER. Для выхода из программы нажмите любую другую клавишу.\n");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Main();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine(message);
                Console.Read();
            }
        }
    }
}

