using ToyRobot;

namespace ToyRobotTests;

public class RobotControllerTests
{
    [Fact]
    public void RunHelp()
    {
        // Arrange
        
        // Act
        RobotController.Help();

        // Assert
        
    }

    [Fact]
    public void PlaceRobot_ValidInput_ReturnsPlace()
    {
        // Arrange
        string input = "PLACE 0,0,NORTH";
        Place? place = null;

        // Act
        place = RobotController.Place(input, place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(0, place.Coord[0]);
        Assert.Equal(0, place.Coord[1]);
        Assert.Equal("NORTH", place.Dir);
    }

    [Fact]
    public void PlaceRobot_InvalidInput_ReturnsNull()
    {
        // Arrange
        string input = "PLACE";
        Place? place = null;

        // Act
        place = RobotController.Place(input, place);

        // Assert
        Assert.Null(place);
    }

    [Fact]
    public void MoveRobot_ValidMove()
    {
        // Arrange
        Place? place = new Place([0, 0], "EAST");

        // Act
        place = RobotController.Move(place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(1, place.Coord[0]);
        Assert.Equal(0, place.Coord[1]);
        Assert.Equal("EAST", place.Dir);
    }

    [Fact]
    public void MoveRobot_InvalidMove_OutOfBounds()
    {
        // Arrange
        Place? place = new Place([5, 5], "NORTH");

        // Act
        place = RobotController.Move(place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(5, place.Coord[0]);
        Assert.Equal(5, place.Coord[1]);
        Assert.Equal("NORTH", place.Dir);
    }

    [Fact]
    public void MoveRobot_InvalidMove_NullPlace()
    {
        // Arrange
        Place? place = null;

        // Act
        place = RobotController.Move(place);

        // Assert
        Assert.Null(place);
    }

    [Fact]
    public void TurnLeft_ValidTurn()
    {
        // Arrange
        Place? place = new Place([0, 0], "NORTH");

        // Act
        place = RobotController.Left(place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(0, place.Coord[0]);
        Assert.Equal(0, place.Coord[1]);
        Assert.Equal("WEST", place.Dir);
    }

    [Fact]
    public void TurnLeft_InvalidTurn_InvalidDirection()
    {
        // Arrange
        Place? place = new Place([0, 0], "");

        // Act
        place = RobotController.Left(place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(0, place.Coord[0]);
        Assert.Equal(0, place.Coord[1]);
        Assert.Equal("", place.Dir);
    }

    [Fact]
    public void TurnLeft_InvalidTurn_NullPlace()
    {
        // Arrange
        Place? place = null;

        // Act
        place = RobotController.Left(place);

        // Assert
        Assert.Null(place);
    }

    [Fact]
    public void TurnRight_ValidTurn()
    {
        // Arrange
        Place? place = new Place([0, 0], "NORTH");

        // Act
        place = RobotController.Right(place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(0, place.Coord[0]);
        Assert.Equal(0, place.Coord[1]);
        Assert.Equal("EAST", place.Dir);
    }

    [Fact]
    public void TurnRight_InvalidTurn_InvalidDirection()
    {
        // Arrange
        Place? place = new Place([0, 0], "");

        // Act
        place = RobotController.Right(place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(0, place.Coord[0]);
        Assert.Equal(0, place.Coord[1]);
        Assert.Equal("", place.Dir);
    }

    [Fact]
    public void TurnRight_InvalidTurn_NullPlace()
    {
        // Arrange
        Place? place = null;

        // Act
        place = RobotController.Right(place);

        // Assert
        Assert.Null(place);
    }

    [Fact]
    public void ReportPlace_ValidPlace()
    {
        // Arrange
        Place? place = new Place([1, 1], "SOUTH");
        var expected = "1,1,SOUTH";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        RobotController.Report(place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.NotNull(place);
        Assert.Equal(expected, output);
    }

    [Fact]
    public void ReportPlace_InvalidPlace_NullPlace()
    {
        // Arrange
        Place? place = null;
        var expected = "You must PLACE the toy robot first.";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        RobotController.Report(place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Null(place);
        Assert.Equal(expected, output);
    }

    [Fact]
    public void ControlRobot_ValidPlace_ValidInput()
    {
        // Arrange
        var input = "PLACE 0,0,NORTH";
        Place? place = null;

        // Act
        place = RobotController.Control(input, place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(0, place.Coord[0]);
        Assert.Equal(0, place.Coord[1]);
        Assert.Equal("NORTH", place.Dir);
    }

    [Fact]
    public void ControlRobot_InvalidPlace_InvalidInput_NullPlace()
    {
        // Arrange
        var input = "PLACE";
        Place? place = null;

        // Act
        place = RobotController.Control(input, place);

        // Assert
        Assert.Null(place);
    }

    [Fact]
    public void ControlRobot_ValidMove_ValidInput_ValidPlace()
    {
        // Arrange
        var input = "MOVE";
        Place? place = new Place([1, 1], "NORTH");

        // Act
        place = RobotController.Control(input, place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(1, place.Coord[0]);
        Assert.Equal(2, place.Coord[1]);
        Assert.Equal("NORTH", place.Dir);
    }

    [Fact]
    public void ControlRobot_ValidMove_ValidInput_NullPlace()
    {
        // Arrange
        var input = "MOVE";
        Place? place = null;
        var expected = "You must PLACE the toy robot first.";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        place = RobotController.Control(input, place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Null(place);
        Assert.Equal(expected, output);
    }

    [Fact]
    public void ControlRobot_ValidLeft_ValidInput_ValidPlace()
    {
        // Arrange
        var input = "LEFT";
        Place? place = new Place([1, 1], "NORTH");

        // Act
        place = RobotController.Control(input, place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(1, place.Coord[0]);
        Assert.Equal(1, place.Coord[1]);
        Assert.Equal("WEST", place.Dir);
    }

    [Fact]
    public void ControlRobot_ValidLeft_ValidInput_NullPlace()
    {
        // Arrange
        var input = "LEFT";
        Place? place = null;
        var expected = "You must PLACE the toy robot first.";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        place = RobotController.Control(input, place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Null(place);
        Assert.Equal(expected, output);
    }

    [Fact]
    public void ControlRobot_ValidRight_ValidInput_ValidPlace()
    {
        // Arrange
        var input = "RIGHT";
        Place? place = new Place([1, 1], "NORTH");

        // Act
        place = RobotController.Control(input, place);

        // Assert
        Assert.NotNull(place);
        Assert.Equal(1, place.Coord[0]);
        Assert.Equal(1, place.Coord[1]);
        Assert.Equal("EAST", place.Dir);
    }

    [Fact]
    public void ControlRobot_ValidRight_ValidInput_NullPlace()
    {
        // Arrange
        var input = "RIGHT";
        Place? place = null;
        var expected = "You must PLACE the toy robot first.";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        place = RobotController.Control(input, place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Null(place);
        Assert.Equal(expected, output);
    }

    [Fact]
    public void ControlRobot_ValidReport_ValidInput_ValidPlace()
    {
        // Arrange
        var input = "REPORT";
        Place? place = new Place([1, 1], "NORTH");
        var expected = "1,1,NORTH";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        place = RobotController.Control(input, place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.NotNull(place);
        Assert.Equal(expected, output);
    }

    [Fact]
    public void ControlRobot_ValidReport_ValidInput_NullPlace()
    {
        // Arrange
        var input = "REPORT";
        Place? place = null;
        var expected = "You must PLACE the toy robot first.";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        place = RobotController.Control(input, place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Null(place);
        Assert.Equal(expected, output);
    }

    [Fact]
    public void ControlRobot_ValidHelp_ValidInput_NullPlace()
    {
        // Arrange
        var input = "HELP";
        Place? place = null;

        // Act
        place = RobotController.Control(input, place);

        // Assert
        Assert.Null(place);
    }

    [Fact]
    public void ControlRobot_ValidQuit_ValidInput_NullPlace()
    {
        // Arrange
        var input = "QUIT";
        Place? place = null;

        // Act
        place = RobotController.Control(input, place);

        // Assert
        Assert.Null(place);
    }

    [Fact]
    public void ControlRobot_InvalidInput()
    {
        // Arrange
        var input = "xyz";
        Place? place = null;
        var expected = "Invalid command. Run HELP for more information.";
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        place = RobotController.Control(input, place);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Null(place);
        Assert.Equal(expected, output);
    }

}
