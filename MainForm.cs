using System;
using System.Drawing;
using System.Windows.Forms;
using QuadraticEquationTrainer.Models;
using QuadraticEquationTrainer.Services;

namespace QuadraticEquationTrainer
{
    public partial class MainForm : Form
    {
        // Поля (сервисы и данные)
        private readonly EquationGenerator _generator = new EquationGenerator();
        private readonly StatisticsService _statistics = new StatisticsService();
        private QuadraticEquation _currentEquation;
        private double _root1;
        private double _root2;

        public MainForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Начальная настройка формы
        /// </summary>
        private void InitializeForm()
        {
            // Устанавливаем начальное состояние
            lblEquation.Text = "Нажмите \"Новое уравнение\" для начала";
            txtSolution.Clear();
            UpdateStatisticsDisplay();

            // Поля ввода скрыты и заблокированы
            txtX1.Visible = false;
            txtX2.Visible = false;
            lblX1Label.Visible = false;
            lblX2Label.Visible = false;
            btnCheck.Visible = false;
            btnShowSolution.Visible = false;
            txtSolution.Visible = false;
            lblStats.Visible = false;

            // Кнопка "Новое уравнение" активна
            btnNew.Enabled = true;
        }

        /// <summary>
        /// Обработчик кнопки "Новое уравнение"
        /// </summary>
        private void btnNew_Click(object sender, EventArgs e)
        {
            // Генерируем новое уравнение
            _currentEquation = _generator.Generate();
            var roots = _currentEquation.GetRoots();
            _root1 = roots.x1 ?? 0;
            _root2 = roots.x2 ?? 0;

            // Показываем уравнение на форме
            lblEquation.Text = _currentEquation.ToString();

            // Показываем все элементы управления
            lblX1Label.Visible = true;
            lblX2Label.Visible = true;
            txtX1.Visible = true;
            txtX2.Visible = true;
            btnCheck.Visible = true;
            btnShowSolution.Visible = true;
            txtSolution.Visible = true;
            lblStats.Visible = true;

            // Очищаем поля ввода и решение
            txtX1.Clear();
            txtX2.Clear();
            txtSolution.Clear();

            // Активируем поля ввода и кнопки
            txtX1.Enabled = true;
            txtX2.Enabled = true;
            btnCheck.Enabled = true;
            btnShowSolution.Enabled = true;

            // Блокируем кнопку "Новое уравнение" пока не решат или не посмотрят решение
            btnNew.Enabled = false;

            // Устанавливаем фокус на первое поле
            txtX1.Focus();

            // Сбрасываем цвет полей (если были подсвечены)
            txtX1.BackColor = SystemColors.Window;
            txtX2.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Обработчик кнопки "Проверить"
        /// </summary>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            // Проверяем, что поля не пустые
            if (string.IsNullOrWhiteSpace(txtX1.Text) || string.IsNullOrWhiteSpace(txtX2.Text))
            {
                MessageBox.Show("Введите оба корня!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Пытаемся преобразовать введённые значения в числа
            if (!double.TryParse(txtX1.Text, out double userX1) || !double.TryParse(txtX2.Text, out double userX2))
            {
                MessageBox.Show("Введите корректные значения (числа)!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверяем правильность ответа (с учётом погрешности 0.001 и порядка корней)
            bool isCorrect = (Math.Abs(userX1 - _root1) < 0.001 && Math.Abs(userX2 - _root2) < 0.001) ||
                             (Math.Abs(userX1 - _root2) < 0.001 && Math.Abs(userX2 - _root1) < 0.001);

            if (isCorrect)
            {
                // Правильный ответ
                _statistics.RegisterSuccess();
                MessageBox.Show("Правильный ответ!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Подсвечиваем поля зелёным
                txtX1.BackColor = Color.LightGreen;
                txtX2.BackColor = Color.LightGreen;

                // Блокируем поля и кнопки
                txtX1.Enabled = false;
                txtX2.Enabled = false;
                btnCheck.Enabled = false;
                btnShowSolution.Enabled = false;

                // Разблокируем кнопку "Новое уравнение"
                btnNew.Enabled = true;
            }
            else
            {
                // Неправильный ответ
                string correctAnswer = FormatRoot(_root1, _root2);
                string userAnswer = $"x₁ = {userX1:F2}, x₂ = {userX2:F2}";
                _statistics.RegisterFail(_currentEquation.ToString(), userAnswer, correctAnswer);

                MessageBox.Show($"Неправильно. Попробуйте ещё раз!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Подсвечиваем поля красным
                txtX1.BackColor = Color.LightCoral;
                txtX2.BackColor = Color.LightCoral;

                // Поля остаются активными для исправления
                txtX1.Enabled = true;
                txtX2.Enabled = true;
                btnCheck.Enabled = true;
                btnShowSolution.Enabled = true;

                // Кнопка "Новое уравнение" остаётся заблокированной
                btnNew.Enabled = false;
            }

            // Обновляем статистику
            UpdateStatisticsDisplay();
        }

        /// <summary>
        /// Обработчик кнопки "Показать решение"
        /// </summary>
        private void btnShowSolution_Click(object sender, EventArgs e)
        {
            if (_currentEquation == null)
            {
                MessageBox.Show("Сначала сгенерируйте уравнение!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Показываем пошаговое решение
            txtSolution.Text = _currentEquation.GetStepByStepSolution();

            // Блокируем поля ввода
            txtX1.Enabled = false;
            txtX2.Enabled = false;

            // Отключаем кнопки проверки и решения
            btnCheck.Enabled = false;
            btnShowSolution.Enabled = false;

            // Разблокируем кнопку "Новое уравнение"
            btnNew.Enabled = true;
        }

        /// <summary>
        /// Обновляет отображение статистики
        /// </summary>
        private void UpdateStatisticsDisplay()
        {
            lblStats.Text = $"Всего уравнений: {_statistics.TotalAttempts}" +
                $" (верно: {_statistics.CorrectAnswers}, неверно: {_statistics.WrongAnswers})";
        }

        private string FormatRoot(double root1, double root2)
        {
            string x1Str = FormatSingleRoot(root1);
            string x2Str = FormatSingleRoot(root2);
            return $"x₁ = {x1Str}, x₂ = {x2Str}";
        }

        private string FormatSingleRoot(double value)
        {
            if (Math.Abs(value - Math.Round(value)) < 0.0001)
            {
                return Math.Round(value).ToString();
            }
            else
            {
                return value.ToString("0.##");
            }
        }
    }
}