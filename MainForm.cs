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
        private bool _isAnswered = false;
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
            btnNoRoots.Visible = false;
            btnNoRoots.Enabled = false;
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

            // === ВСЕ ЭЛЕМЕНТЫ ВСЕГДА ВИДИМЫ И АКТИВНЫ ===
            // Поля ввода
            txtX1.Visible = true;
            txtX2.Visible = true;
            lblX1Label.Visible = true;
            lblX2Label.Visible = true;
            txtX1.Enabled = true;
            txtX2.Enabled = true;

            // Кнопки проверки и решения
            btnCheck.Visible = true;
            btnShowSolution.Visible = true;
            btnCheck.Enabled = true;
            btnShowSolution.Enabled = true;

            // Кнопка "Нет корней!" — всегда видна и активна
            btnNoRoots.Visible = true;
            btnNoRoots.Enabled = true;

            // Поле для решения и статистика
            txtSolution.Visible = true;
            txtSolution.Clear();
            lblStats.Visible = true;

            // === ОБЩИЕ ДЕЙСТВИЯ ===
            // Очищаем поля ввода
            txtX1.Clear();
            txtX2.Clear();
            txtSolution.Clear();

            // Сбрасываем цвет полей
            txtX1.BackColor = SystemColors.Window;
            txtX2.BackColor = SystemColors.Window;

            // Сбрасываем флаг ответа
            _isAnswered = false;

            // === НОВОЕ УРАВНЕНИЕ — РЕШЕНИЕ НЕДОСТУПНО ===
            btnShowSolution.Enabled = false;

            // Блокируем кнопку "Новое уравнение" (пока не решат или не посмотрят решение)
            btnNew.Enabled = false;

            // Устанавливаем фокус на первое поле
            txtX1.Focus();

            // Обновляем статистику
            UpdateStatisticsDisplay();
        }

        /// <summary>
        /// Обработчик кнопки "Проверить"
        /// </summary>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (_isAnswered)
            {
                MessageBox.Show("Это уравнение уже решено! Нажмите «Новое уравнение» для продолжения.",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Проверяем, что поля не пустые
            if (string.IsNullOrWhiteSpace(txtX1.Text) || string.IsNullOrWhiteSpace(txtX2.Text))
            {
                MessageBox.Show("Введите оба корня!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Пытаемся преобразовать введённые значения в числа
            if (!double.TryParse(txtX1.Text, out double userX1) || !double.TryParse(txtX2.Text, out double userX2))
            {
                MessageBox.Show("Введите корректные значения!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверяем правильность ответа (с учётом погрешности 0.001 и порядка корней)
            bool isCorrect = (Math.Abs(userX1 - _root1) < 0.001 && Math.Abs(userX2 - _root2) < 0.001) ||
                             (Math.Abs(userX1 - _root2) < 0.001 && Math.Abs(userX2 - _root1) < 0.001);

            // === ПОСЛЕ ПРОВЕРКИ — ФИКСИРУЕМ, ЧТО ОТВЕТ ДАН ===
            _isAnswered = true;

            if (isCorrect)
            {
                // Правильный ответ
                _statistics.RegisterSuccess();
                MessageBox.Show("Правильный ответ!", "Верно!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Подсвечиваем поля зелёным
                txtX1.BackColor = Color.LightGreen;
                txtX2.BackColor = Color.LightGreen;
                
            }
            else
            {
                // Неправильный ответ
                string correctAnswer = FormatRoot(_root1, _root2);
                string userAnswer = $"x₁ = {userX1:F2}, x₂ = {userX2:F2}";
                _statistics.RegisterFail(_currentEquation.ToString(), userAnswer, correctAnswer);

                MessageBox.Show($"Неверный ответ!",
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Подсвечиваем поля красным
                txtX1.BackColor = Color.LightCoral;
                txtX2.BackColor = Color.LightCoral;
            }

            txtX1.Enabled = false;
            txtX2.Enabled = false;
            btnCheck.Enabled = false;
            btnNew.Enabled = true;
            btnShowSolution.Enabled = true;
            btnNoRoots.Enabled = false;

            // Обновляем статистику
            UpdateStatisticsDisplay();
        }

        private void btnNoRoots_Click(object sender, EventArgs e)
        {
            // === ПРОВЕРКА: уже решено? ===
            if (_isAnswered)
            {
                MessageBox.Show("Это уравнение уже решено! Нажмите «Новое уравнение» для продолжения.",
                    "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // === ПРОВЕРКА: есть ли уравнение? ===
            if (_currentEquation == null)
            {
                MessageBox.Show("Сначала сгенерируйте уравнение!", "Внимание!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // === ПРОВЕРКА: действительно ли нет корней? ===
            var roots = _currentEquation.GetRoots();
            bool hasRoots = roots.x1.HasValue;

            // === ФИКСИРУЕМ, ЧТО ОТВЕТ ДАН ===
            _isAnswered = true;

            if (!hasRoots)
            {
                // ✅ Правильно! Корней действительно нет
                _statistics.RegisterSuccess();
                MessageBox.Show("Правильно! Корней действительно нет!",
                    "Верно!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Блокируем кнопку "Нет корней!"
                btnNoRoots.Enabled = false;
                btnNoRoots.BackColor = SystemColors.Control;

                // Блокируем поля ввода и кнопки
                txtX1.Enabled = false;
                txtX2.Enabled = false;
                btnCheck.Enabled = false;
                btnShowSolution.Enabled = true;

                // Разблокируем кнопку "Новое уравнение"
                btnNew.Enabled = true;
            }
            else
            {
                // неверно, корни есть
                _statistics.RegisterFail(
                    _currentEquation.ToString(),
                    "Нет корней (неправильно)",
                    FormatRoot(_root1, _root2)
                );

                MessageBox.Show($"Неверно, у этого уравнения есть корни!",
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Блокируем кнопку "Нет корней!" (чтобы нельзя было нажать повторно)
                btnNoRoots.Enabled = false;
                btnNoRoots.BackColor = SystemColors.Control;

                // Поля ввода и кнопки блокируются
                txtX1.Enabled = false;
                txtX2.Enabled = false;
                btnCheck.Enabled = false;
                btnShowSolution.Enabled = true;

                // Разблокируем кнопку "Новое уравнение"
                btnNew.Enabled = true;
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
                MessageBox.Show("Сначала сгенерируйте уравнение!", "Внимание!",
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
            btnNoRoots.Enabled= false;

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