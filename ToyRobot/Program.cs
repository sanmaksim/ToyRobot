using ToyRobot;

Console.WriteLine("*************************");
Console.WriteLine("** TOY ROBOT SIMULATOR **");
Console.WriteLine("*************************");
Console.WriteLine();

Place? place = null;

if (args.Length == 0)
{
    while (true)
    {
        Console.Write("> ");
        var command = Console.ReadLine();

        if (!string.IsNullOrEmpty(command))
        {
            place = RobotController.Control(command, place);
            if (command.Equals("quit"))
                break;
        }
    }
}
else if (args.Length == 1)
{
    if (File.Exists(args[0]))
    {
        foreach (string line in File.ReadLines(args[0]))
        {
            place = RobotController.Control(line, place);
        }
    }
    else
    {
        Console.WriteLine("File not found.");
    }
}
else
{
    Console.WriteLine("Invalid number of arguments.");
}
