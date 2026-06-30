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

        public int TotalAttempts => _totalAttempts;
        public int CorrectAnswers => _correctAnswers;
        public int WrongAnswers => _wrongAnswers;

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
        public void RegisterFail()
        {
            _totalAttempts++;
            _wrongAnswers++;
        }
    }
}