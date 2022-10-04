using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge
{
    public class Elevator
    {
        private readonly int _maxFloor;
        private readonly int _elevatorNumber;
        private int _currentFloor;
        private int _currentNumberOfPeople;
        private ElevatorDirection _direction;
        private bool[] _floorStatuses;
        public Elevator(int maxFloor, int elevatorNumber, int startingFloor)
        {
            _elevatorNumber = elevatorNumber;
            _maxFloor = maxFloor;
            _currentFloor = startingFloor;
            _floorStatuses = new bool[_maxFloor + 1];
        }

        private void Stop(int floor)
        {
            _direction = ElevatorDirection.Stopped;
            _currentFloor = floor;
            _floorStatuses[floor] = false;
            Console.WriteLine("Stopped at floor {0}", floor);
        }

        private void Descend(int floor)
        {
            for (var i = _currentFloor; i >= 1; i--)
            {
                if (_floorStatuses[i])
                    Stop(floor);
                else
                    continue;
            }

            _direction = ElevatorDirection.Stopped;
            Console.WriteLine("Waiting..");
        }

        private void Ascend(int floor)
        {
            for (int i = _currentFloor; i <= _maxFloor; i++)
            {
                if (_floorStatuses[i])
                    Stop(floor);
                else
                    continue;
            }

            _direction = ElevatorDirection.Stopped;
            Console.WriteLine("Waiting..");
        }

        void FloorReached()
        {
            _currentNumberOfPeople = 0;
            Console.WriteLine("Floor Reached");
        }

        public void FloorPress(int floor,int numberOfPeople)
        {
            _currentNumberOfPeople = numberOfPeople;
            if (floor > _maxFloor)
            {
                Console.WriteLine("There are only {0} floors", _maxFloor);
                return;
            }

            _floorStatuses[floor] = true;

            if (_direction == ElevatorDirection.Down)
            {
                Descend(floor);
            }
            else if (_direction == ElevatorDirection.Stopped)
            {
                if (_currentFloor < floor)
                    Ascend(floor);
                else if (_currentFloor == floor)
                    FloorReached();
                else
                    Descend(floor);
            }
            else if (_direction == ElevatorDirection.Up)
            {
                Ascend(floor);
            }
        }

        public int GetCurrentFloor()
        {
            return _currentFloor; 
        }

        public bool Stopped()
        {
            return _direction == ElevatorDirection.Stopped; 
        }

        public void GetStatus()
        {
            Console.WriteLine($"Elevator: {_elevatorNumber}. Current Floor: {_currentFloor}. Direction: {_direction}. Number Of People: {_currentNumberOfPeople} ");
        }
    }

    public enum ElevatorDirection
    {
        Stopped,
        Up,
        Down,
    }
}
