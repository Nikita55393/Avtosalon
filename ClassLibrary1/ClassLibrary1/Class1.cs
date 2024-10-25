using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
        public static bool CheckMail(string mail)
        {
            // Простая проверка, что строка содержит символ @ и .
            return !string.IsNullOrWhiteSpace(mail) && Regex.IsMatch(mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Метод для проверки сложности пароля
        public static bool CheckPassword(string password)
        {
            // Пароль не должен быть простым, должен содержать хотя бы одну букву,
            // хотя бы один символ и иметь длину минимум 8 символов.
            return password.Any(char.IsDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Length >= 8;
        }
        public static bool Checklogin(string login)
        {
            // Пароль не должен быть простым, должен содержать хотя бы одну букву,
            // хотя бы один символ и иметь длину минимум 6 символов.
            return login.Any(char.IsDigit) && login.Any(char.IsUpper) && login.Any(char.IsLower) && login.Length >= 6;
        }
    }
}
