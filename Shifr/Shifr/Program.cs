using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemanT_Library;

namespace Shifr
{
    class Program
    {
        #region Длинна блока и список для хранения ключа
        private static int block;
        private static int[] spisok;
        #endregion
        #region Печатает меню
        private static int Menu()
        {
            ColorMess.Yellow("\n Выберите пункт меню");
            ColorMess.Cyan("\n\n 1) Ввести текст" +
                             "\n 2) Зашифровать текст" +
                             "\n 3) Расшифровать текст" +
                             "\n 4) Напечатать текст" +
                             "\n 5) Выход");
            ColorMess.Green("\n\n Цифра: ");
            return Input.Check(1, 5);
        }
        #endregion
        #region Создает текст
        private static string CreateText()
        {
            ColorMess.Yellow("\n Введите текст (разрешен русский алфавит + знаки препинания): ");
            string temp = Input.SymbolRu();
            return temp;
        }
        #endregion
        #region Правила для шифрования или расшифровывания
        private static void Rules()
        {
            ColorMess.Yellow("\n Сколько в блоке символов (от 2 до 9): ");
            block = Input.Check(2, 9);
            spisok = new int[block];
            Again:
            ColorMess.Yellow("\n Введите порядок чередования символов в блоке из "+block+" (пример для блока из 4 символов: 3241): ");
            string temp = Console.ReadLine();
            char[] c = temp.ToCharArray();
            bool ok = true;
            for (int i = 0; i < block; i++)
            {
                try
                {
                    string str = "";
                    str = str + c[i];
                    int num = Convert.ToInt32(str);
                    if (Convert.ToInt32(num) >= 1 && Convert.ToInt32(num) <= block)
                    {
                        ok = true;
                        spisok[i] = num;
                    }
                    else
                    {
                        ok = false;
                        break;
                    }
                }
                catch(FormatException)
                {
                    Console.Clear();
                    ColorMess.Red("\n Некорректный ввод, попробуйте еще раз!\n");
                    goto Again;
                }
                catch(IndexOutOfRangeException)
                {
                    Console.Clear();
                    ColorMess.Red("\n Некорректный ввод, попробуйте еще раз!\n");
                    goto Again;
                }
            }
            for(int i = 0; i<block; i++)
            {
                try
                {
                    if (spisok[i] == spisok[i + 1])
                    {
                        ok = false;
                        break;
                    }
                }
                catch (IndexOutOfRangeException) { }
            }
            if(!ok)
            {
                Console.Clear();
                ColorMess.Red("\n В ваших числах есть число которое не входит в блок или есть повторяющиеся числа, попробуйте еще раз!");
                goto Again;
            }
        }
        #endregion
        #region Для зашифровки текста. Принимает строку для обработки
        private static string Encrypt(string stroka)
        {
            Rules();
            Console.Clear();
            char[] c = stroka.ToCharArray();
            int kol;
            if (c.Length % block == 0)
            {
                kol = c.Length / block;
            }
            else
            {
                kol = c.Length / block + 1;
            }
            string[] temp = new string[kol];
            int z = 0;
            for (int i = 0; i < kol; i++)
            {
                for (int j = 0; j < block; j++)
                {
                    try
                    {
                        temp[i] = temp[i] + c[z];
                        z++;
                    }
                    catch(IndexOutOfRangeException)
                    {
                        temp[i] = temp[i] + " ";
                        z++;
                    }
                }
            }
            stroka = null;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < block; i++)
            {
                dic.Add(spisok[i],(i+1));
            }
            int[] key = new int[block];
            for (int i = 0; i < block; i++)
            {
                key[i] = dic[i+1];
            }
            for (int i = 0; i<kol; i++)
            {
                char[] temp2 = temp[i].ToCharArray();
                for (int j = 0; j < key.Length; j++)
                    stroka = stroka + temp2[key[j]-1];
            }
            return stroka;
        }
        #endregion
        #region Для расшифровки текста. Принимает строку для расшифровки
        private static string Decrypt(string stroka)
        {
            Rules();
            Console.Clear();
            char[] c = stroka.ToCharArray();
            int kol;
            if (c.Length % block == 0)
            {
                kol = c.Length / block;
            }
            else
            {
                kol = c.Length / block + 1;
            }
            string[] temp = new string[kol];
            int z = 0;
            for (int i = 0; i < kol; i++)
            {
                for (int j = 0; j < block; j++)
                {
                    try
                    {
                        temp[i] = temp[i] + c[z];
                        z++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        temp[i] = temp[i] + " ";
                        z++;
                    }
                }
            }
            stroka = null;
            for (int i = 0; i < kol; i++)
            {
                char[] temp2 = temp[i].ToCharArray();
                for (int j = 0; j < spisok.Length; j++)
                    stroka = stroka + temp2[spisok[j] - 1];
            }
            return stroka;
        }
        #endregion
        #region Запрашивает у пользователя данные, печатает меню с выбором операции, выводит результат обработки
        static void Main()
        {
            string stroka = null;
            Again:
            Console.Clear();
            switch(Menu())
            {
                case 1:
                    Console.Clear();
                    stroka = CreateText();
                    ColorMess.Green("\n Текст успешно введен\n");
                    Message.GoToBack();
                    goto Again;
                case 2:
                    if(stroka != null)
                    {
                        Console.Clear();
                        stroka = Encrypt(stroka);
                        ColorMess.Green("\n Зашифровано\n");
                    }
                    else
                    {
                        ColorMess.Red("\n Сперва введите текст\n");
                    }
                    Message.GoToBack();
                    goto Again;
                case 3:
                    if (stroka != null)
                    {
                        Console.Clear();
                        stroka = Decrypt(stroka);
                        ColorMess.Green("\n Расшифровано\n");
                    }
                    else
                    {
                        ColorMess.Red("\n Сперва введите текст\n");
                    }
                    Message.GoToBack();
                    goto Again;
                case 4:
                    if (stroka != null)
                    {
                        Console.Clear();
                        ColorMess.Green("\n Ваша текст выглядит так: " + stroka + "\n");
                    }
                    else
                    {
                        ColorMess.Red("\n Сперва введите текст\n");
                    }
                    Message.GoToBack();
                    goto Again;
                case 5:
                    break;
            }
        }
        #endregion
    }
}
