using System;

namespace FileCeasarEncryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Encryptor enc = new Encryptor();

            enc.GetTextOfFile();

            Console.ReadLine();
        }
    }
}
