﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GeneratedPassConsole
{
    class Program
    {
        public static readonly char[] passSymb =
        {
            'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М',
            'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ',
            'Ы','Ь', 'Э', 'Ю', 'Я', 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з',
            'и', 'й','к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х',
            'ц', 'ч', 'ш','щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я','/','.','-',',','+',
            '*',')','(','&','%','$','#','"','!','?','=','>','<',';',':','@','\\',
            '[',']','^','_','`','}','{','|', 'A','B','C','D','E','F','G','H','I',
            'J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q',
            'r','s','t','u','v','w','x','y','z','1','2','3','4','5','6','7','8',
            '9'
        };
        static void Main(string[] args)
        {
            Random rnd = new Random();
            string pass = "";
            Console.Write("Введите кол-во символов: ");
            int passLenght = int.Parse(Console.ReadLine());
            for (int i = 0; pass.Length < passLenght; i++)
            {
                pass = pass.Insert(0, passSymb[rnd.Next(0, passSymb.Length)].ToString());

            }
            Console.WriteLine($"Сгенерированный пароль: {pass} | Длина: {pass.Length}");
            Console.WriteLine($"Надёжность пароля: {PasswdUtils.GetPasswordStrengthRate(PasswdUtils.CheckPasswordStrength(pass))}.");
        }
    }
    public static class PasswdUtils
    {
        public static string[] badPasswords = new string[]
                {
            "123456", "123456789", "qwerty", "111111", "1234567", "666666", "12345678", "7777777", "123321", "0", "654321", "1234567890", "123123", "555555", "vkontakte", "gfhjkm", "159753", "777777", "TempPassWord", "qazwsx", "1q2w3e", "1234", "112233", "121212", "qwertyuiop", "qq18ww899", "987654321", "12345", "zxcvbn", "zxcvbnm", "999999", "samsung", "ghbdtn", "1q2w3e4r", "1111111", "123654", "159357", "131313", "qazwsxedc", "123qwe", "222222", "asdfgh", "333333", "9379992", "asdfghjkl", "4815162342", "12344321", "любовь", "88888888", "11111111", "knopka", "пароль", "789456", "qwertyu", "1q2w3e4r5t", "iloveyou", "vfhbyf", "marina", "password", "qweasdzxc", "10203", "987654", "yfnfif", "cjkysirj", "nikita", "888888", "йцукен", "vfrcbv", "k.,jdm", "qwertyuiop[]", "qwe123", "qweasd", "natasha", "123123123", "fylhtq", "q1w2e3", "stalker", "1111111111", "q1w2e3r4", "nastya", "147258369", "147258", "fyfcnfcbz", "1234554321", "1qaz2wsx", "andrey", "111222", "147852", "genius", "sergey", "7654321", "232323", "123789", "fktrcfylh", "spartak", "admin", "test", "123", "azerty", "abc123", "lol123", "easytocrack1", "hello", "saravn", "holysh!t", "1", "Test123", "tundra_cool2", "456", "dragon", "thomas", "killer", "root", "1111", "pass", "master", "aaaaaa", "a", "monkey", "daniel", "asdasd", "e10adc3949ba59abbe56e057f20f883e", "changeme", "computer", "jessica", "letmein", "mirage", "loulou", "lol", "superman", "shadow", "admin123", "secret", "administrator", "sophie", "kikugalanetroot", "doudou", "liverpool", "hallo", "sunshine", "charlie", "parola", "100827092", "/", "michael", "andrew", "password1", "****you", "matrix", "cjmasterinf", "internet", "hallo123", "eminem", "demo", "gewinner", "pokemon", "abcd1234", "guest", "ngockhoa", "martin", "sandra", "asdf", "hejsan", "george", "qweqwe", "lollipop", "lovers", "q1q1q1", "tecktonik", "naruto", "12", "password12", "password123", "password1234", "password12345", "password123456", "password1234567", "password12345678", "password123456789", "000000", "maximius", "123abc", "baseball1", "football1", "soccer", "princess", "slipknot", "11111", "nokia", "super", "star", "666999", "12341234", "1234321", "135790", "159951", "212121", "zzzzzz", "121314", "134679", "142536", "19921992", "753951", "7007", "1111114", "124578", "19951995", "258456", "qwaszx", "zaqwsx", "55555", "77777", "54321", "qwert", "22222", "33333", "99999", "88888", "66666",
            "iloveu", "пароль"
                };
        public static Random rnd = new Random();

        public enum PasswordStrength
        {
            ОченьТяжелый, Тяжелый, Нормальный, Слабый
        }

        public static PasswordStrength GetPasswordStrengthRate(int strength)
        {
            if (strength > 300)
                return PasswordStrength.ОченьТяжелый;
            else
                if (strength > 200)
                return PasswordStrength.Тяжелый;
            else
                if (strength > 100)
                return PasswordStrength.Нормальный;
            else
                return PasswordStrength.Слабый;
        }

        public static int CheckPasswordStrength(string password)
        {
            double multi0 = 1.0;
            double multi1 = 1.0;
            double multi2 = 1.0;
            double multi3 = 0;
            int score = 0;
            foreach (var bp in badPasswords)
            {
                if (password.ToLower().Contains(bp.ToLower()))
                    multi0 = 0.75;
                else
                    if (bp.ToLower() == password.ToLower())
                    multi0 = 0.125;
            }
            List<char> usedChars = new List<char>();
            foreach (var chr in password)
            {
                if (!usedChars.Contains(chr))
                    usedChars.Add(chr);
            }
            multi1 = FrequencyFactor(password.ToLower());

            score += password.Length * 15;

            Dictionary<string, double> patterns = new Dictionary<string, double> { { @"1234567890", 0.0 }, //включает цифры
                                                                         { @"[a-z]", 0.1 }, //буквы низ. регистра
                                                                         { @"[ёа-я]", 0.2 }, //буквы низ. регистра
                                                                         { @"[A-Z]", 0.2 }, //буквы выс. регистра
                                                                         { @"[ЁА-Я]", 0.3 }, //буквы выс. регистра
                                                                         {  "[!,@#\\$%\\^&\\*?_~=;:'\"<>[]()~`\\\\|/]", 0.4 },  //символы
                                                                         { @"[¶©]", 0.5} }; //Спецсимволы

            foreach (var pattern in patterns)
                if (Regex.Matches(password, pattern.Key).Count > 0)
                    multi2 += pattern.Value;

            if (password.Length > 2)
                multi3 += 0;
            if (password.Length > 4)
                multi3 += 0.25;
            if (password.Length > 6)
                multi3 += 0.75;
            if (password.Length > 8)
                multi3 += 1.0;


            return (int)(score * multi0 * multi1 * multi2);
        }

        static double FrequencyFactor(string password)
        {
            HashSet<char> differentSymbols = new HashSet<char>(password.ToCharArray());

            return Map(differentSymbols.Count, 1.0, password.Length, 0.1, 1);
        }

        static double Map(double value, double fromLower, double fromUpper, double toLower, double toUpper)
        {
            return toLower + (value - fromLower) / (fromUpper - fromLower) * (toUpper - toLower);
        }
    }
}
