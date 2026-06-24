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
    }
}