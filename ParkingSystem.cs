using System;
using System.Collections.Generic;

namespace ParkingSystem {
    public class ParkingLot {
        private int totalSlots;
        private Dictionary<int, Vehicle> slots;

        public ParkingLot(int totalSlots) {
            this.totalSlots = totalSlots;
            this.slots = new Dictionary<int, Vehicle>();
        }

        public void ParkVehicle(string registrationNumber, string color, VehicleType type) {
            if (IsFull())
            {
                Console.WriteLine("Sorry, parking lot is full");
                return;
            }

            int slotNumber = GetNextAvailableSlot();
            slots.Add(slotNumber, new Vehicle(registrationNumber, color, type));
            Console.WriteLine($"Allocated slot number: {slotNumber}");
        }

        public void Leave(int slotNumber) {
            if (slots.ContainsKey(slotNumber))
            {
                slots.Remove(slotNumber);
                Console.WriteLine($"Slot number {slotNumber} is free");
            }
            else
            {
                Console.WriteLine($"Slot number {slotNumber} not found");
            }
        }

        public void PrintStatus() {
            Console.WriteLine("Slot No. Type Registration No Colour");
            foreach (var slot in slots)
            {
                Console.WriteLine($"{slot.Key} {slot.Value.Type} {slot.Value.RegistrationNumber} {slot.Value.Color}");
            }
        }

        public int GetTotalSlots() {
            return totalSlots;
        }

        public int GetAvailableSlots() {
            return totalSlots - slots.Count;
        }

        private bool IsFull() {
            return slots.Count == totalSlots;
        }

        private int GetNextAvailableSlot() {
            for (int i = 1; i <= totalSlots; i++)
            {
                if (!slots.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1; // Parking lot is full
        }
    }

    public class Vehicle
    {
        public string RegistrationNumber { get; }
        public string Color { get; }
        public VehicleType Type { get; }

        public Vehicle(string registrationNumber, string color, VehicleType type)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            Type = type;
        }
    }

    public enum VehicleType
    {
        Car,
        Motorcycle
    }

    class Program
    {
        static void Main(string[] args)
        {
            ParkingLot parkingLot = null;

            while (true)
            {
                string command = Console.ReadLine();
                string[] parts = command.Split(' ');

                if (parts[0] == "create_parking_lot")
                {
                    int totalSlots = int.Parse(parts[1]);
                    parkingLot = new ParkingLot(totalSlots);
                    Console.WriteLine($"Created a parking lot with {totalSlots} slots");
                }
                else if (parts[0] == "park")
                {
                    string registrationNumber = parts[1];
                    string color = parts[3];
                    VehicleType type = parts[2].ToLower() == "mobil" ? VehicleType.Car : VehicleType.Motorcycle;
                    parkingLot.ParkVehicle(registrationNumber, color, type);
                }
                else if (parts[0] == "leave")
                {
                    int slotNumber = int.Parse(parts[1]);
                    parkingLot.Leave(slotNumber);
                }
                else if (parts[0] == "status")
                {
                    parkingLot.PrintStatus();
                }
                else if (parts[0] == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }
    }
}
