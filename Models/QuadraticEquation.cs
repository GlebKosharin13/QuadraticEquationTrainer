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
            string result = $"Дано уравнение: {this}{Environment.NewLine}{Environment.NewLine}";

            // Шаг 1: Дискриминант
            result += "1. Находим дискриминант:" + Environment.NewLine;
            result += FormatDiscriminantStep() + Environment.NewLine + Environment.NewLine;

            if (d < 0)
            {
                result += "2. D < 0, следовательно, уравнение не имеет действительных корней." + Environment.NewLine;
                result += "Ответ: корней нет.";
                return result;
            }

            if (Math.Abs(d) < 0.0001) // D == 0
            {
                double x = -_b / (2 * _a);
                result += "2. D = 0, следовательно, уравнение имеет один корень (кратный):" + Environment.NewLine;
                result += $"   x = -b / (2a) = {FormatNumberForDisplay(-_b)} / ({2 * _a}) = {FormatNumberForDisplay(x)}" + Environment.NewLine + Environment.NewLine;
                result += $"Ответ: x = {FormatNumberForDisplay(x)}";
                return result;
            }

            // D > 0
            double sqrtD = Math.Sqrt(d);
            double x1 = (-_b - sqrtD) / (2 * _a);
            double x2 = (-_b + sqrtD) / (2 * _a);

            string negB = FormatNumberForDisplay(-_b);
            string sqrtDStr = FormatNumberForDisplay(sqrtD);
            string x1Str = FormatNumberForDisplay(x1);
            string x2Str = FormatNumberForDisplay(x2);

            result += "2. D > 0, следовательно, уравнение имеет два корня:" + Environment.NewLine;
            result += $"   √D = {sqrtDStr}" + Environment.NewLine + Environment.NewLine;
            result += "3. Находим корни:" + Environment.NewLine;
            result += $"   x₁ = (-b - √D) / (2a) = ({negB} - {sqrtDStr}) / {2 * _a} = {x1Str}" + Environment.NewLine;
            result += $"   x₂ = (-b + √D) / (2a) = ({negB} + {sqrtDStr}) / {2 * _a} = {x2Str}" + Environment.NewLine + Environment.NewLine;
            result += $"Ответ: x₁ = {x1Str}, x₂ = {x2Str}";

            return result;
        }

        /// <summary>
        /// Форматирует шаг с дискриминантом, чтобы не было двух знаков подряд
        /// </summary>
        private string FormatDiscriminantStep()
        {
            double d = GetDiscriminant();

            string bSquared = FormatNumberForDisplay(_b * _b);
            string fourAC = FormatNumberForDisplay(4 * _a * _c);
            string dStr = FormatNumberForDisplay(d);

            // Если c отрицательное, показываем в скобках
            string cTerm = _c < 0 ? $"({FormatNumberForDisplay(_c)})" : FormatNumberForDisplay(_c);

            // Если b отрицательное, показываем в скобках
            string bTerm = _b < 0 ? $"({FormatNumberForDisplay(_b)})" : FormatNumberForDisplay(_b);

            // Формируем выражение для 4ac с учётом знака
            string fourACTerm;
            if (_c < 0)
            {
                // Если c отрицательное, то 4ac уже отрицательное, показываем в скобках
                fourACTerm = $"({fourAC})";
            }
            else
            {
                fourACTerm = fourAC;
            }

            // Формируем итоговую строку
            string result = $"   D = b² - 4ac = {bTerm}² - 4 * {_a} * {cTerm} = {bSquared} - {fourACTerm} = {dStr}";

            return result;
        }

        /// <summary>
        /// Форматирует число для отображения (убирает -0, лишние нули)
        /// </summary>
        private string FormatNumberForDisplay(double value)
        {
            // Если число очень близко к нулю (с учётом погрешности) — показываем 0
            if (Math.Abs(value) < 0.0001)
            {
                return "0";
            }

            // Если число целое (или очень близко к целому) — показываем без десятичной части
            if (Math.Abs(value - Math.Round(value)) < 0.0001)
            {
                return Math.Round(value).ToString();
            }

            // Иначе показываем с одним знаком после запятой (максимум)
            return value.ToString("0.##");
        }
    }
}
