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
        private static int block;
        private static int[] spisok;
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
        private static string CreateText()
        {
            ColorMess.Yellow("\n Введите текст (разрешен русский алфавит + знаки препинания): ");
            string temp = Input.SymbolRu();
            return temp;
        }
        private static void Rules()
        {
            ColorMess.Yellow("\n Сколько в блоке символов (от 2 до 9): ");
            block = Input.Check(2, 9);
            Again:
            ColorMess.Yellow("\n Введите порядок чередования символов в блоке из "+block+" (числа вводить одним числом(пример для блока из 4 символов: 3241)): ");
            string temp = Convert.ToString(Input.Check(12, 987654321));
            char[] c = temp.ToCharArray();
            bool ok = true;
            for (int i = 0; i < c.Length - 1; i++)
            {
                if (c[i] >= 1 && c[i] <= block)
                {
                    ok = true;
                    spisok[i] = Convert.ToInt32(c[i]);
                }
                else
                {
                    ok = false;
                    return;
                }
            }
            if(!ok)
            {
                Console.Clear();
                ColorMess.Red("\n В ваших числах есть число которое не входит в блок, попробуйте еще раз!");
                goto Again;
            }
        }
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
    }
}
