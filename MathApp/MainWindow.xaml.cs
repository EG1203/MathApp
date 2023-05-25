using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MathApp
{
    public partial class MainWindow : Window
    {
        // Генератор случайных чисел
        private Random random = new Random();

        // Текущие числа для задания
        private int number1;
        private int number2;

        // Текущий ответ пользователя
        private int answer;

        // Текущий уровень сложности
        private int level;

        // Количество попыток ввода ответа
        private int attempts;

        public MainWindow()
        {
            InitializeComponent();
            // Выбор уровня сложности по умолчанию
            level = 1;
            // Генерация нового задания
            GenerateTask();
        }

        // Метод для генерации нового задания в зависимости от уровня сложности
        private void GenerateTask()
        {
            // Сброс количества попыток
            attempts = 0;
            // Очистка поля ввода ответа
            answerBox.Clear();
            // Очистка сообщения об ошибке
            errorLabel.Text = "";
            // Генерация двух случайных отрицательных чисел в заданном диапазоне
            switch (level)
            {
                case 1:
                    number1 = random.Next(-10, 0);
                    number2 = random.Next(-10, 0);
                    break;
                case 2:
                    number1 = random.Next(-100, 0);
                    number2 = random.Next(-100, 0);
                    break;
                case 3:
                    number1 = random.Next(-1000, 0);
                    number2 = random.Next(-1000, 0);
                    break;
                default:
                    break;
            }
            // Выбор операции сложения или вычитания с равной вероятностью
            if (random.Next(0, 2) == 0)
            {
                // Сложение
                taskLabel.Text = $"{number1} + {number2} = ?";
                answer = number1 + number2;
            }
            else
            {
                // Вычитание
                taskLabel.Text = $"{number1} - {number2} = ?";
                answer = number1 - number2;
            }
        }

        // Метод для проверки ответа пользователя при нажатии на кнопку "Проверить"
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что поле ввода не пустое и содержит целое число
            if (int.TryParse(answerBox.Text, out int userAnswer))
            {
                // Проверка, что ответ пользователя совпадает с правильным ответом
                if (userAnswer == answer)
                {
                    // Поздравление с правильным ответом и генерация нового задания
                    MessageBox.Show("Правильно! Молодец!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    GenerateTask();
                }
                else
                {
                    // Увеличение количества попыток на одну
                    attempts++;
                    // Проверка, что количество попыток не превысило лимит
                    if (attempts < 3)
                    {
                        // Сообщение об ошибке и предложение попробовать ещё раз
                        errorLabel.Text = "Неправильно. Попробуй ещё раз.";
                        answerBox.Clear();
                        answerBox.Focus();
                    }
                    else
                    {
                        // Сообщение об исчерпании попыток и правильном ответе. Генерация нового задания.
                        MessageBox.Show($"Неправильно. Правильный ответ: {answer}. Начни заново.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        GenerateTask();
                    }
                }
            }
            else
            {
                NewMethod();
                answerBox.Clear();
                answerBox.Focus();
            }
        }

        private void NewMethod()
        {
            // Сообщение о неверном формате ввода и очистка поля ввода
            errorLabel.Text = "Введите целое число.";
        }

        // Метод для изменения уровня сложности при выборе одного из радиокнопок
        private void LevelRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Получение выбранной радиокнопки и её тега (номера уровня)
            RadioButton radioButton = sender as RadioButton;
            int tag = int.Parse(radioButton.Tag.ToString());
            // Установка уровня сложности в соответствии с тегом радиокнопки
            level = tag;
            // Генерация нового задания с новым уровнем сложности
            GenerateTask();
        }
    }
}
