using BancoXYZ.Sockets.Client.Menu.Executor;
using System;
using System.Collections.Generic;
using System.Text;

namespace BancoXYZ.Sockets.Client.Menu
{
    public class OptionMenu
    {
        private const string BACK_COMMAND = "b";
        private const string QUIT_COMMAND = "x";

        public string Key { get; set; }
        public string Label { get; set; }
        public IExecutor Executor { get; set; }

        public int Level { get; internal set; }

        public OptionMenu Parent { get; internal set; } = null;
        public IDictionary<string, OptionMenu> Submenus { get; internal set; } = new Dictionary<string, OptionMenu>();

        public void AddSubmenu(OptionMenu optionMenu)
        {
            optionMenu.Parent = this;
            optionMenu.Level = Level + 1;
            optionMenu.Key = (Submenus.Count + 1).ToString();
            Submenus.Add(optionMenu.Key, optionMenu);
        }

        public void ExecuteMenu()
        {
            if (Executor != null)
            {
                Executor.Execute();
                Parent.PrintMenu();
            }
            else
            {
                PrintMenu();
            }
        }

        public void PrintMenu()
        {
            Console.Clear();
            Console.Write(ToString());
            Console.WriteLine(string.Empty);
            Console.WriteLine($"Oprime '{BACK_COMMAND}' para regresar al menú anterior.");
            Console.WriteLine($"Oprime '{QUIT_COMMAND}' para finalizar.");
            Console.WriteLine(string.Empty);
            Console.Write($"Seleccione una opción: ");

            string input = Console.ReadLine();
            SelectOption(input);
        }

        public virtual void SelectOption(string option)
        {
            if (option.ToLower().Equals(QUIT_COMMAND))
            {
                Console.WriteLine("Good bye!!");
                Environment.Exit(0);
            }

            if (Parent != null && option.ToLower().Equals(BACK_COMMAND))
            {
                Parent.PrintMenu();
                return;
            }

            if (Submenus.ContainsKey(option))
            {
                Submenus[option].ExecuteMenu();
                return;
            }

            Console.WriteLine("Opción inválida, inténtalo nuevamente.");
            Console.ReadKey();
            PrintMenu();
        }

        public override string ToString()
        {
            var lines = new StringBuilder();

            lines.AppendLine(string.Empty);
            lines.AppendLine($":: {Label} ::");
            lines.AppendLine(string.Empty);
            foreach(var submenu in Submenus)
            {
                lines.AppendLine($"  {submenu.Value.Key} - {submenu.Value.Label}");
            }
            return lines.ToString();
        }
    }
}
