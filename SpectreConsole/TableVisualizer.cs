
using FlashCardManager.DTO_s;
using Spectre.Console;


namespace FlashCardManager.SpectreConsole
{


    internal class TableVisualizer
    {

        internal static void stackTable(List<StackDTO> tableData)
        {
            var table = new Table();

            table.AddColumn("Title");
            table.AddColumn("Size");

            foreach(var entry in tableData)
            {
                table.AddRow(entry.nameDTO ?? string.Empty, entry.sizeDTO ?? "0");
            }

            table.ShowRowSeparators();

            AnsiConsole.Write(table);

        }


        internal static void flashCardTable(List<FlashcardDTO> tableData)
        {
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Front Side");
            table.AddColumn("Back Side");
            foreach(var entry in tableData)
            {
                table.AddRow(entry.idDTO ?? string.Empty, entry.frontDTO ?? string.Empty, entry.backDTO ?? string.Empty);
            }
            table.ShowRowSeparators();
            AnsiConsole.Write(table);
        }

    }













}
