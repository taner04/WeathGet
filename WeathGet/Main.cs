using Weather_Console_Application.Class;
using Weather_Console_Application.Class_Weather;
using Weather_Console_Application.Class_Weather.Main;

string newRun;
string selector = "\u001b[32m[*] \u001b[0m";

Console.Clear();
RenderIntroBox();
var start = Console.ReadKey(true);

do
{
    Console.CursorVisible = false;
    newRun = "";
    bool enter = false;
    int option = 1;

    Header();
    (var left, var top) = Console.GetCursorPosition();

    while (!enter)
    {
        Console.SetCursorPosition(left, top);
        Console.WriteLine($"\n\t{(option == 1 ? selector : "[ ] ")}Look weather from City");
        Console.WriteLine($"\n\t{(option == 2 ? selector : "[ ] ")}Exit");

        var key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                option++;
                option = option == 3 ? 1 : option;
                break;
            case ConsoleKey.DownArrow:
                option--;
                option = option == 0 ? 2 : option;
                break;
            case ConsoleKey.Enter:
                enter = true;
                break;
        }
    }

    switch (option)
    {
        case 1:
            GenerateWeatherData();
            break;
        case 2:
            Environment.Exit(0);
            break;
    }

    Console.WriteLine("\n\t    Press Enter");
    var enterPressed = Console.ReadKey(true);
    while (enterPressed.Key != ConsoleKey.Enter)
    {
        enterPressed = Console.ReadKey(true);
    }
} while (newRun == "");

void Header()
{
    Console.Clear();
    Console.WriteLine("\n\t    Console Weather Application");
    Console.WriteLine("\t    ===========================");
}

void GenerateWeatherData()
{
    Header();
    Console.CursorVisible = true;
    Console.Write("\n\t    Enter your city: ");
    string city = Console.ReadLine();

    if (city == string.Empty)
    {
        bool invalid = true;
        while (invalid)
        {
            Header();
            Console.WriteLine("\n\t\u001b[31m    You input was empty!\u001b[0m");
            Console.Write("\t    Enter your city: ");
            city = Console.ReadLine();
            if (city != string.Empty)
                invalid = false;
        }
    }

    (string lat, string lon) = WebRequest.GetCityLocation(city);
    if (lat != string.Empty && lon != string.Empty)
    {
        Root root = WebRequest.GetCityWeather(lat, lon);
        Console.WriteLine($"\n\t \u001b[32m   Temp\t->\t{root.main.temp}°C");
        Console.WriteLine($"\t    Description\t->\t{root.weather[0].description}");
        Console.WriteLine($"\t    Humidity\t->\t{root.main.humidity}\u001b[0m");
    }
    else
        Console.WriteLine("\n\t    Invalid City");
    Console.CursorVisible = false;
}

//Const for the Intro Box
const int InstructionBoxWidth = 80;
const int TextIndent = 6;

void RenderIntroBox()
{
    string text1 = "        For navigating the program, use the up and down arrow keys,";
    string text2 = "                       enter to select a menu item";
    string startText = "Press enter to start the program";
    int width = InstructionBoxWidth - 2;

    Console.WriteLine($"\n\t     ╔{new string('═', width)}╗");
    Console.WriteLine($"\t     ║ {text1, -(InstructionBoxWidth - 4)} ║");
    Console.WriteLine($"\t     ║ {text2, -(InstructionBoxWidth - 4)} ║");
    Console.WriteLine($"\t     ╚{new string('═', width)}╝");

    Console.WriteLine($"\n\t\t\t\t    ╔{new string('═', startText.Length + 2)}╗");
    Console.WriteLine($"\t\t\t\t    ║ {startText} ║");
    Console.WriteLine($"\t\t\t\t    ╚{new string('═', startText.Length + 2)}╝");
}
