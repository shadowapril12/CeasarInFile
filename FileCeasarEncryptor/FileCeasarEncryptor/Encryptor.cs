using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FileCeasarEncryptor
{
    class Encryptor
    {
        //Дирректория в которой находится текстовый файл для шифрования
        private string directory = @"task.txt";

        //Переменная для хранения считанного из файла текста
        private string strOrigin;

        //Переменная для хранения зашифрованного слова
        private string encryptedWord = "";

        //Количество букв в русском алфавите
        private int maxRuAlpha = 32;

        //Количество букв в английском алфавите
        private int maxEnAlpha = 26;

        //Строка, включающщая в себя русский алфавит
        private string ruAlpha = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        //Строка включающая в себя английский алфавит
        private string enAlpha = "abcdefghijklmnopqrstuvwxyz";

        //Переменная для хранения ключа шифра
        private int key;

        /// <summary>
        /// Меиод GetTextOfFile считывает текст из файла в заданной директории
        /// </summary>
        public void GetTextOfFile()
        {
            //Используем класс StreamReader для считывания текста из директории directory
            using (StreamReader sr = new StreamReader(directory, Encoding.Default))
            {
                strOrigin = sr.ReadToEnd().ToLower();
            }

            //Выводим считанное слово
            Console.WriteLine(strOrigin);

            //Запрос ключа
            GetKey();

            //Шифрование полученного слова
            Encrypt();

            //Запись зашиффрованного слова в файл
            WriteEncryptedWord();
        }

        /// <summary>
        /// Метод Encrypt зашифровывает считанное из файла слово по заданному ключу
        /// </summary>
        private void Encrypt()
        {
            //Для кажддого символа в цикле
            for(int i = 0; i < strOrigin.Length; i++)
            {
                ///Проверяется, соответствует ли i-ый символ английскому или русскому алфавиту
                ///Если символ соответствует английскому алфавиту
                if(enAlpha.Contains(strOrigin[i].ToString()))
                {
                    ///Если после прибавления ключа, к символу соответсвующему i-му символу в алфавите, полученный индекс превышает
                    ///максимальное количество символов в английском алфавите
                    if(enAlpha.IndexOf(strOrigin[i]) + key > maxEnAlpha)
                    {
                        ///То из полученного после сложения индекса вычитается максимальное количество символов в алфавите, и в
                        ///переменную encryptedWord добавляется полученный символ
                        encryptedWord += enAlpha[(enAlpha.IndexOf(strOrigin[i]) + key) - maxEnAlpha - 1];
                    }
                    else
                    {
                        ///В обратном случае в переменную просто записывается полученный после сложения символ
                        encryptedWord += enAlpha[enAlpha.IndexOf(strOrigin[i]) + key];
                    }                 
                }              
                ///Если символ соответствует русскому алфавиту
                else if (ruAlpha.Contains(strOrigin[i].ToString()))
                {
                    ///Если после прибавления ключа, к символу соответсвующему i-му символу в алфавите, полученный индекс превышает
                    ///максимальное количество символов в русском алфавите
                    if (ruAlpha.IndexOf(strOrigin[i]) + key > maxRuAlpha)
                    {
                        ///То из полученного после сложения индекса вычитается максимальное количество символов в алфавите, и в
                        ///переменную encryptedWord добавляется полученный символ
                        encryptedWord += ruAlpha[(ruAlpha.IndexOf(strOrigin[i]) + key) - maxRuAlpha - 1];
                    }
                    else
                    {
                        ///В обратном случае в переменную просто записывается полученный после сложения символ
                        encryptedWord += ruAlpha[ruAlpha.IndexOf(strOrigin[i]) + key];
                    }
                }
                else
                {
                    ///Если символ не принадлежит ни оддному из алфавитов, то он просто записывается в переменную
                    ///encryptedWord и выводится сообщение о том, что данный символ не будет зашифрован
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Символ {strOrigin[i]} зашифрован не будет");
                    Console.ResetColor();

                    encryptedWord += strOrigin[i];
                }
            }

            Console.WriteLine(encryptedWord);
        }

        /// <summary>
        /// Метод GetKey запрашивает ключ к шифру
        /// </summary>
        private void GetKey()
        {
            Console.WriteLine("Введите ключ для шифрования");

            try
            {
                ///Приеобразование к целочисленному типу введеной строки
                key = Convert.ToInt32(Console.ReadLine());
            }
            catch(ArgumentException ex)
            {
                //При неудачном преобразовании выбрасывается исключение и выводится сообщение об ошибке
                Console.WriteLine($"Невертный формат ключа. {ex.Message}");

                //Метод GetKey перезапускается заново
                GetKey();
            }
        }

        /// <summary>
        /// Метод WriteEncryptedWord записывает в файл зашифрованное слово
        /// </summary>
        private void WriteEncryptedWord()
        {

            ///Используется класс StreamWriter  для записи слова
            using (StreamWriter sw = new StreamWriter(directory, false, System.Text.Encoding.Default))
            {
                ///Запись слова в файл
                sw.Write(encryptedWord);
            }

            Console.WriteLine("Слово зашифровано");
        }       
    }
}
