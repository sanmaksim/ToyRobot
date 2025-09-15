Console.WriteLine("*************************");
Console.WriteLine("** TOY ROBOT SIMULATOR **");
Console.WriteLine("*************************");
Console.WriteLine();

Place? place = null;

static void Help()
{
    Console.WriteLine("PLACE X,Y,F \t- Put the toy robot on the table in position X,Y\r\n" +
                                "\t\t  facing NORTH, SOUTH, EAST or WEST.");
    Console.WriteLine(          "\t\t- The origin (0,0) can be considered to be the\r\n" +
                                "\t\t  SOUTH WEST most corner.");
    Console.WriteLine(          "\t\t- The first valid command to the robot is a PLACE\r\n" +
                                "\t\t  command, after that, any sequence of commands\r\n" +
                                "\t\t  may be issued, in any order, including another\r\n" +
                                "\t\t  PLACE command.");
    Console.WriteLine("MOVE        \t- Move the toy robot one unit forward in the direction\r\n" +
                                "\t\t  it is currently facing.");
    Console.WriteLine("LEFT        \t- Rotate the robot 90 degrees to the LEFT without\r\n" +
                                "\t\t  changing the position of the robot.");
    Console.WriteLine("RIGHT       \t- Rotate the robot 90 degrees to the RIGHT without\r\n" +
                                "\t\t  changing the position of the robot.");
    Console.WriteLine("REPORT      \t- Print the X,Y and F of the robot.");
    Console.WriteLine("HELP        \t- Display this help text.");
    Console.WriteLine("QUIT        \t- Exit the application.");
}

static Place? Place(string input, Place? place)
{
    string[] command = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (command.Length != 2)
    {
        Console.WriteLine("Invalid PLACE command. The correct format is: PLACE X,Y,F");
        return place;
    }

    string[] coord = command[1].Split(',');
    if (coord.Length != 3)
    {
        Console.WriteLine("Invalid position. The correct format is: X,Y,F");
        return place;
    }

    if (!int.TryParse(coord[0], out int x))
    {
        Console.WriteLine("X must be a number.");
        return place;
    }

    if (!int.TryParse(coord[1], out int y))
    {
        Console.WriteLine("Y must be a number.");
        return place;
    }

    string f = coord[2];
    if (!(f.ToLower().Trim() is "north" or "south" or "east" or "west"))
    {
        Console.WriteLine("F must be one of NORTH, SOUTH, EAST, WEST.");
        return place;
    }

    return new Place([x, y], f);
}

static Place? Move(Place? place)
{
    int lowerBound = 0;
    int upperBound = 5;

    if (place == null)
    {
        Console.WriteLine("You must PLACE the toy robot first.");
        return place;
    }
    
    switch (place.Dir.ToLower())
    {
        case "north":
            if ((place.Coord[0] + 1) <= upperBound)
                place.Coord[0] += 1;
            else
                Console.WriteLine("Cannot move any further NORTH.");
            break;
        case "south":
            if ((place.Coord[0] - 1) >= lowerBound)
                place.Coord[0] -= 1;
            else
                Console.WriteLine("Cannot move any further SOUTH.");
            break;
        case "east":
            if ((place.Coord[1] + 1) <= upperBound)
                place.Coord[1] += 1;
            else
                Console.WriteLine("Cannot move any further EAST.");
            break;
        case "west":
            if ((place.Coord[1] - 1) >= lowerBound)
                place.Coord[1] -= 1;
            else
                Console.WriteLine("Cannot move any further WEST.");
            break;
        default:
            Console.WriteLine("Invalid PLACE.");
            break;
    }

    return place;
}

static Place? Left(Place? place)
{
    var dir = "";

    if (place == null)
    {
        Console.WriteLine("You must PLACE the toy robot first.");
        return place;
    }

    switch (place.Dir.ToLower())
    {
        case "north":
            dir = "west";
            break;
        case "south":
            dir = "east";
            break;
        case "east":
            dir = "north";
            break;
        case "west":
            dir = "south";
            break;
        default:
            Console.WriteLine("Invalid direction.");
            break;
    }
    
    return place with { Dir = dir.ToUpper() };
}

static Place? Right(Place? place)
{
    var dir = "";

    if (place == null)
    {
        Console.WriteLine("You must PLACE the toy robot first.");
        return place;
    }

    switch (place.Dir.ToLower())
    {
        case "north":
            dir = "east";
            break;
        case "south":
            dir = "west";
            break;
        case "east":
            dir = "south";
            break;
        case "west":
            dir = "north";
            break;
        default:
            Console.WriteLine("Invalid direction.");
            break;
    }

    return place with { Dir = dir.ToUpper() };
}

static void Report(Place? place)
{
    if (place == null)
        Console.WriteLine("You must PLACE the toy robot first.");
    else
        Console.WriteLine($"{string.Join(",", place.Coord)},{place.Dir.ToUpper()}");
}

static Place? ToyRobotControl(string input, Place? place)
{
    switch (input.ToLower().Trim())
    {
        case string s when s.Contains("place"):
            place = Place(input, place);
            break;
        case "move":
            place = Move(place);
            break;
        case "left":
            place = Left(place);
            break;
        case "right":
            place = Right(place);
            break;
        case "report":
            Report(place);
            break;
        case "help":
            Help();
            break;
        case "quit":
            break;
        default:
            Console.WriteLine($"Invalid command. Run HELP for more information.");
            break;
    }
    return place;
}

if (args.Length == 0)
{
    while (true)
    {
        Console.Write("> ");
        var command = Console.ReadLine();

        if (!string.IsNullOrEmpty(command))
        {
            place = ToyRobotControl(command, place);
            if (command.ToLower() == "quit")
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
            place = ToyRobotControl(line, place);
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

record Place(int[] Coord, string Dir);