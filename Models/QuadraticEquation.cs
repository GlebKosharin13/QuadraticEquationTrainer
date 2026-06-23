using System;


namespace QuadraticEquationTrainer.Models
{
    /// <summary>
    /// Модель квадратного уравнения вида ax² + bx + c = 0
    /// </summary>
    public class QuadraticEquation
    {
        // Коэффициенты уравнения
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        // Свойства для доступа к коэффициентам (только для чтения)
        public double A => _a;
        public double B => _b;
        public double C => _c;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="a">Коэффициент при x²</param>
        /// <param name="b">Коэффициент при x</param>
        /// <param name="c">Свободный член</param>
        public QuadraticEquation(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        /// <summary>
        /// Вычисляет дискриминант
        /// </summary>
        public double GetDiscriminant()
        {
            return _b * _b - 4 * _a * _c;
        }

        /// <summary>
        /// Вычисляет корни уравнения
        /// </summary>
        /// <returns>Кортеж (x1, x2). Если корней нет, оба null</returns>
        public (double? x1, double? x2) GetRoots()
        {
            double d = GetDiscriminant();

            if (d < 0)
                return (null, null);

            if (d == 0)
            {
                double x = -_b / (2 * _a);
                return (x, x); // оба корня одинаковые
            }

            double sqrtD = Math.Sqrt(d);
            double x1 = (-_b - sqrtD) / (2 * _a);
            double x2 = (-_b + sqrtD) / (2 * _a);
            return (x1, x2);
        }

        /// <summary>
        /// Возвращает строковое представление уравнения
        /// </summary>
        public override string ToString()
        {
            // Формируем красивую строку вида "2x² - 5x + 3 = 0"
            string result = "";

            // Член с x²
            if (_a == 1)
                result += "x²";
            else if (_a == -1)
                result += "-x²";
            else
                result += $"{_a}x²";

            // Член с x
            if (_b > 0)
                result += $" + {_b}x";
            else if (_b < 0)
                result += $" - {Math.Abs(_b)}x";

            // Свободный член
            if (_c > 0)
                result += $" + {_c}";
            else if (_c < 0)
                result += $" - {Math.Abs(_c)}";

            result += " = 0";
            return result;
        }

        /// <summary>
        /// Возвращает пошаговое решение уравнения
        /// </summary>
        public string GetStepByStepSolution()
        {
            double d = GetDiscriminant();
            string result = $"Дано уравнение: {this}\n\n";

            // Шаг 1: Дискриминант
            result += "1. Находим дискриминант:\n";
            result += $"   D = b² - 4ac = ({_b})² - 4·{_a}·{_c} = {_b * _b} - {4 * _a * _c} = {d:F2}\n\n";

            if (d < 0)
            {
                result += "2. D < 0, следовательно, уравнение не имеет действительных корней.\n";
                result += "Ответ: корней нет.";
                return result;
            }

            if (Math.Abs(d) < 0.0001) // D == 0
            {
                double x = -_b / (2 * _a);
                result += "2. D = 0 → уравнение имеет один корень (кратный):\n";
                result += $"   x = -b / (2a) = {-_b} / ({2 * _a}) = {x:F2}\n\n";
                result += $"Ответ: x = {x:F2}";
                return result;
            }

            // D > 0
            double sqrtD = Math.Sqrt(d);
            double x1 = (-_b - sqrtD) / (2 * _a);
            double x2 = (-_b + sqrtD) / (2 * _a);

            result += "2. D > 0 → уравнение имеет два корня:\n";
            result += $"   √D = {sqrtD:F2}\n\n";
            result += "3. Находим корни:\n";
            result += $"   x₁ = (-b - √D) / (2a) = ({-_b} - {sqrtD:F2}) / {2 * _a} = {x1:F2}\n";
            result += $"   x₂ = (-b + √D) / (2a) = ({-_b} + {sqrtD:F2}) / {2 * _a} = {x2:F2}\n\n";
            result += $"Ответ: x₁ = {x1:F2}, x₂ = {x2:F2}";

            return result;
        }
    }
}
