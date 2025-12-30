using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCardManager.Models;

namespace FlashCardManager.Helpers
{
    internal class DisplayMethods
    {

        internal static void SpecificClear(int startLine, int lineCount)
        {

            Console.SetCursorPosition(0, startLine);

            for (int i = 0; i < lineCount; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, startLine);

        }

        internal static void TitleCard()
        {
            Console.Clear();

            Console.WriteLine(@"
 ________ ___       ________  ________  ___  ___  ________  ________  ________  ________
|\  _____\\  \     |\   __  \|\   ____\|\  \|\  \|\   ____\|\   __  \|\   __  \|\   ___ \
\ \  \__/\ \  \    \ \  \|\  \ \  \___|\ \  \\\  \ \  \___|\ \  \|\  \ \  \|\  \ \  \_|\ \
 \ \   __\\ \  \    \ \   __  \ \_____  \ \   __  \ \  \    \ \   __  \ \   _  _\ \  \ \\ \
  \ \  \_| \ \  \____\ \  \ \  \|____|\  \ \  \ \  \ \  \____\ \  \ \  \ \  \\  \\ \  \_\\ \
   \ \__\   \ \_______\ \__\ \__\____\_\  \ \__\ \__\ \_______\ \__\ \__\ \__\\ _\\ \_______\
    \|__|    \|_______|\|__|\|__|\_________\|__|\|__|\|_______|\|__|\|__|\|__|\|__|\|_______|
                                \|_________|
 _____ ______   ________  ________   ________  ________  _______   ________                  
|\   _ \  _   \|\   __  \|\   ___  \|\   __  \|\   ____\|\  ___ \ |\   __  \
\ \  \\\__\ \  \ \  \|\  \ \  \\ \  \ \  \|\  \ \  \___|\ \   __/|\ \  \|\  \
 \ \  \\|__| \  \ \   __  \ \  \\ \  \ \   __  \ \  \  __\ \  \_|/_\ \   _  _\
  \ \  \    \ \  \ \  \ \  \ \  \\ \  \ \  \ \  \ \  \|\  \ \  \_|\ \ \  \\  \|
   \ \__\    \ \__\ \__\ \__\ \__\\ \__\ \__\ \__\ \_______\ \_______\ \__\\ _\         
    \|__|     \|__|\|__|\|__|\|__| \|__|\|__|\|__|\|_______|\|_______|\|__|\|__|
                ");

            Console.WriteLine("==================================================================================================\n");

        }


        internal static string FlashcardToDisplay(FlashCards card)
        {

            const int columnWidth = 25;


            string front = ColumnFormat(card.front, columnWidth);
            string back = ColumnFormat(card.back, columnWidth);

            return front + back;
 
        }



        internal static string ColumnFormat(string? text, int columnWidth)
        {
            if (string.IsNullOrEmpty(text))
                text = "[Empty]";

            return text.Length >  columnWidth
                ? string.Concat(text.AsSpan(0, columnWidth - 1), "…")
                : text.PadRight(columnWidth);    

            //dont even know what asSpan is the IDE just recommended it lmao

        }  


    }
}
