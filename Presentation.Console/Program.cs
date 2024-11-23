using System;
using System.Threading.Tasks;
using Business.Services; 
using MySql.Data.MySqlClient;

namespace Presentation.Console
{
    class Program
    {
        private static readonly ILaundryService _laundryService;
        private static readonly string _connectionString = "server=localhost;user=root;database=laundry;port=3306;password=";

        static Program()
        {
            _laundryService = new LaundryService(_connectionString);
        }

        static async Task Main(string[] args)
        {
            while (true)
            {
                try
                {
                    await ShowMainMenu();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"An error occurred: {ex.Message}");
                    System.Console.WriteLine("Press any key to continue...");
                    System.Console.ReadKey();
                }
            }
        }

        static async Task ShowMainMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Laundry Management System ===");
            System.Console.WriteLine("1. Select Owner");
            System.Console.WriteLine("0. Exit");

            System.Console.Write("\nEnter your choice: ");
            string choice = System.Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await SelectOwner();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    System.Console.WriteLine("Invalid choice. Press any key to continue...");
                    System.Console.ReadKey();
                    break;
            }
        }

        static async Task SelectOwner()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Select Owner ===");

            var owners = await _laundryService.GetAllOwnersAsync();
            foreach (var owner in owners)
            {
                System.Console.WriteLine($"{owner.Id}. {owner.Name}");
            }

            System.Console.Write("\nEnter owner ID (0 to go back): ");
            if (int.TryParse(System.Console.ReadLine(), out int ownerId) && ownerId != 0)
            {
                await SelectLaundry(ownerId);
            }
        }

        static async Task SelectLaundry(int ownerId)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Select Laundry ===");

            var laundries = await _laundryService.GetLaundriesByOwnerIdAsync(ownerId);
            foreach (var laundry in laundries)
            {
                System.Console.WriteLine($"{laundry.Id}. {laundry.Name}");
            }

            System.Console.Write("\nEnter laundry ID (0 to go back): ");
            if (int.TryParse(System.Console.ReadLine(), out int laundryId) && laundryId != 0)
            {
                await SelectMachine(laundryId);
            }
        }

        static async Task SelectMachine(int laundryId)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Select Machine ===");

            var machines = await _laundryService.GetMachinesByLaundryIdAsync(laundryId);
            foreach (var machine in machines)
            {
                System.Console.WriteLine($"{machine.Id}. Machine {machine.Id} - Status: {machine.Status}");
            }

            System.Console.Write("\nEnter machine ID (0 to go back): ");
            if (int.TryParse(System.Console.ReadLine(), out int machineId) && machineId != 0)
            {
                await UpdateMachineStatus(machineId);
            }
        }

        static async Task UpdateMachineStatus(int machineId)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== Update Machine Status ===");
            System.Console.WriteLine("1. Set to Available");
            System.Console.WriteLine("2. Set to In Use");
            System.Console.WriteLine("3. Set to Maintenance");

            System.Console.Write("\nEnter your choice: ");
            string choice = System.Console.ReadLine();

            string status = choice switch
            {
                "1" => "Available",
                "2" => "In Use",
                "3" => "Maintenance",
                _ => null
            };

            if (status != null)
            {
                bool success = await _laundryService.UpdateMachineStatusAsync(machineId, status);
                System.Console.WriteLine(success ? "Status updated successfully!" : "Failed to update status.");
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }
    }
}