using System;
using System.Collections.Generic;
using System.Text;

namespace QuadraticEquationTrainer.Services
{
    /// <summary>
    /// Сервис для учёта статистики решений
    /// </summary>
    public class StatisticsService
    {
        private int _totalAttempts = 0;
        private int _correctAnswers = 0;
        private int _wrongAnswers = 0;
        private readonly List<string> _errors = new List<string>();

        public int TotalAttempts => _totalAttempts;
        public int CorrectAnswers => _correctAnswers;
        public int WrongAnswers => _wrongAnswers;
        public IReadOnlyList<string> Errors => _errors.AsReadOnly();

        /// <summary>
        /// Регистрирует правильный ответ
        /// </summary>
        public void RegisterSuccess()
        {
            _totalAttempts++;
            _correctAnswers++;
        }

        /// <summary>
        /// Регистрирует неправильный ответ
        /// </summary>
        /// <param name="equation">Уравнение</param>
        /// <param name="userAnswer">Ответ пользователя</param>
        /// <param name="correctAnswer">Правильный ответ</param>
        public void RegisterFail(string equation, string userAnswer, string correctAnswer)
        {
            _totalAttempts++;
            _wrongAnswers++;
            _errors.Add($"Уравнение: {equation} | Ваш ответ: {userAnswer} | Правильный: {correctAnswer}");
        }

        /// <summary>
        /// Возвращает отчёт о статистике в виде строки
        /// </summary>
        public string GetStatisticsReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("📊 Статистика:");
            sb.AppendLine($"Всего попыток: {_totalAttempts}");
            sb.AppendLine($"✅ Верно: {_correctAnswers}");
            sb.AppendLine($"❌ Ошибок: {_wrongAnswers}");

            if (_totalAttempts > 0)
            {
                double percent = (double)_correctAnswers / _totalAttempts * 100;
                sb.AppendLine($"Процент верных ответов: {percent:F1}%");
            }

            if (_errors.Count > 0)
            {
                sb.AppendLine($"\nПоследние ошибки (последние 5):");
                int start = Math.Max(0, _errors.Count - 5);
                for (int i = start; i < _errors.Count; i++)
                {
                    sb.AppendLine($"  - {_errors[i]}");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Сбрасывает статистику
        /// </summary>
        public void Reset()
        {
            _totalAttempts = 0;
            _correctAnswers = 0;
            _wrongAnswers = 0;
            _errors.Clear();
        }
    }
}