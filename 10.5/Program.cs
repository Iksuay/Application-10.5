using System.Xml.Serialization;
using System;

namespace MiniCalculator
{
    interface IAddition
    {
        double Add(double a, double b);
    }

    interface ILogger
    {
        void LogError(string message);
        void LogEvent(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void LogEvent(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    class Calculator : IAddition
    {
        private ILogger _logger;

        public Calculator(ILogger logger)
        {
            _logger = logger;
        }

        public double Add(double a, double b)
        {
            return a + b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            Calculator calculator = new Calculator(logger);
            double num1, num2, result;

            try
            {
                Console.Write("Введите первое число: ");
                num1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Введите второе число: ");
                num2 = Convert.ToDouble(Console.ReadLine());

                result = calculator.Add(num1, num2);
                Console.WriteLine("Сумма чисел: " + result);
            }
            catch (FormatException)
            {
                logger.LogError("Ошибка: некорректное число.");
            }
            catch (Exception ex)
            {
                logger.LogError("Ошибка: " + ex.Message);
            }
            finally
            {
                logger.LogEvent("Калькулятор завершил работу.");
            }

            Console.ReadKey();
        }
    }
}