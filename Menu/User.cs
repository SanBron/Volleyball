using static System.Console;
namespace KeyboardMenuDemo
{

    class Game
    {
        Settings mySettings = new Settings();
        public static bool AreSettingsEmpty = true;
        public void Start()
        {

            string[] options = { "Играть", "Настройки", "Выход" };
            RunMenu(options);

        }
        private void RunMenu(string[] options)
        {
            bool k = true;
            while (k)
            {

                Menu mainmenu = new Menu(options);
                int selectedindex = mainmenu.Run();
                switch (selectedindex)
                {
                    case 0:
                        if (AreSettingsEmpty)
                        {
                            Console.WriteLine("Нужно выбрать настройки");
                            Thread.Sleep(2000);
                            string[] opt = new string[0];
                            MenuOptions2(ref opt);
                        }
                        else
                        {
                            k = false;
                            PlayGame();
                        }
                        break;
                    case 1:
                        string[] op = new string[0];
                        MenuOptions(out op);
                        break;
                    case 2:
                        k = false;
                        ExitGame();
                        break;
                }
            }
        }
        private void RunOptions(string[] options)
        {
            Menu mainmenu = new Menu(options);
            int selectedindex = mainmenu.Run();
            switch (selectedindex)
            {
                case 0:
                    mySettings.SetDefault();
                    break;
                case 1:
                    mySettings.SetNew();
                    break;

            }
        }
        public void MenuOptions(out string[] options)
        {
            string[] option = { "Использовать настройки по умолчанию ", "Использовать свои настройки" };
            options = option;
            RunOptions(options);
        }

        public void MenuOptions2(ref string[] options)
        {
            string[] option = { "Использовать настройки по умолчанию ", "Использовать свои настройки" };
            options = option;
            RunOptions(options);
        }
        private void ExitGame()
        {
            WriteLine("Мы рассчитывали что хы хотя бы попробуете поиграть(");
            Thread.Sleep(5000);
        }
        private void PlayGame()
        {
            Clear();
            Starter.GameProcess();

        }

    }
    class Menu
    {
        private int SelectedIndex;
        private string[] Options;

        public Menu(string[] options)
        {
            Options = options;
            SelectedIndex = 0;
        }

        public void DisplayOptions()
        {
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;


                if (i == SelectedIndex)
                {
                    prefix = " *";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                WriteLine($"{prefix} << {currentOption} >>");

            }
            ResetColor();
        }
        public int Run()
        {



            ConsoleKey keyPressed;
            do
            {

                Clear();
                DisplayOptions();


                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;

        }
    }
}
