
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

    }













}
