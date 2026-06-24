using System;
using QuadraticEquationTrainer.Models;

namespace QuadraticEquationTrainer.Services
{
    /// <summary>
    /// Генератор случайных квадратных уравнений
    /// </summary>
    public class EquationGenerator
    {
        private readonly Random _random = new Random();

        // Вероятность генерации уравнения без корней (20%)
        private const double NoRootsProbability = 0.2;

        /// <summary>
        /// Генерирует случайное квадратное уравнение
        /// </summary>
        /// <returns>Объект QuadraticEquation</returns>
        public QuadraticEquation Generate()
        {
            // Сначала решаем: будут корни или нет?
            bool shouldHaveRoots = _random.NextDouble() >= NoRootsProbability;

            while (true)
            {
                // Генерируем целые коэффициенты
                int a = _random.Next(1, 6);      // a от 1 до 5
                int b = _random.Next(-10, 11);   // b от -10 до 10
                int c = _random.Next(-10, 11);   // c от -10 до 10

                double discriminant = b * b - 4 * a * c;

                if (shouldHaveRoots)
                {
                    // === НУЖНЫ КОРНИ ===
                    // Проверяем, что дискриминант >= 0
                    if (discriminant < 0)
                        continue;

                    // Проверяем, что дискриминант — полный квадрат
                    double sqrtD = Math.Sqrt(discriminant);
                    if (Math.Abs(sqrtD - Math.Round(sqrtD)) > 0.00001)
                        continue;

                    // Проверяем, что корни — конечные десятичные дроби (не более 1 знака)
                    double x1 = (-b - sqrtD) / (2 * a);
                    double x2 = (-b + sqrtD) / (2 * a);

                    if (IsFiniteDecimal(x1) && IsFiniteDecimal(x2))
                    {
                        return new QuadraticEquation(a, b, c);
                    }
                }
                else
                {
                    // === НУЖНЫ УРАВНЕНИЯ БЕЗ КОРНЕЙ ===
                    if (discriminant < 0)
                    {
                        return new QuadraticEquation(a, b, c);
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет, является ли число конечной десятичной дробью (не более 1 знака после запятой)
        /// </summary>
        private bool IsFiniteDecimal(double value)
        {
            // Округляем до 1 знака
            double rounded = Math.Round(value, 1);
            // Проверяем, что разница меньше погрешности
            return Math.Abs(value - rounded) < 0.0001;
        }
    }
}
