using ElevatorChallenge;

Console.WriteLine("Hello!!!");

var howManyFloorsInTheBuilding = "How many floors in the building?";
Console.WriteLine(howManyFloorsInTheBuilding);
var floorsInput = Console.ReadLine();
var maxNumberOfFloors = GetNumberInput(floorsInput, howManyFloorsInTheBuilding);

var howManyElevators = "How many elevators?";
Console.WriteLine(howManyElevators);
var elevatorsInput = Console.ReadLine();
var maxNumberOfElevators = GetNumberInput(elevatorsInput, howManyElevators);

var elevators = GenerateElevators(maxNumberOfElevators, maxNumberOfFloors);

var stay = true;
while (stay)
{
    var pressWhichFloorYouWouldLikeToGoTo = "Please enter the floor number";
    Console.WriteLine(pressWhichFloorYouWouldLikeToGoTo);
    var floorNumberInput = Console.ReadLine();
    var floorNumber = GetNumberInput(floorNumberInput, pressWhichFloorYouWouldLikeToGoTo);

    var pleaseEnterNumberOfPeople = "Please enter number of people";
    Console.WriteLine(pleaseEnterNumberOfPeople);
    var peopleInput = Console.ReadLine();
    var peopleNumber = GetNumberInput(peopleInput, pleaseEnterNumberOfPeople);
    var availableElevator = AvailableElevator(elevators, floorNumber);
  
    availableElevator.FloorPress(floorNumber, peopleNumber);

    GetElevatorsStatus(elevators);
}

Console.ReadLine();

int GetNumberInput(string? value, string retryMessage)
{
    var valid = int.TryParse(value, out var number);
    while (!valid)
    {
        Console.WriteLine("Invalid Input. Try again...");
        Console.WriteLine(retryMessage);
        value = Console.ReadLine();
        valid = int.TryParse(value, out number);
    }

    return number;
}

List<Elevator> GenerateElevators(int maxNumberOfElevators1, int maxNumberOfFloors1)
{
    var start = 1;
    return Enumerable.Range(start, maxNumberOfElevators1).Select(i => new Elevator(maxNumberOfFloors1, i, start)).ToList();
}

void GetElevatorsStatus(List<Elevator> list)
{
    Console.WriteLine("-----------------Status-----------------");
    list.ForEach(elevator => elevator.GetStatus());
    Console.WriteLine("----------------------------------------");
}

Elevator? AvailableElevator(List<Elevator> elevators1, int floorNumber1)
{
    return elevators1.Where(elevator => elevator.Stopped()).OrderBy(elevator => floorNumber1 <= elevator.GetCurrentFloor()).FirstOrDefault();
}
