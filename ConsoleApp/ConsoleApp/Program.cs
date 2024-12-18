using BuildingLibrary;
using FractionLibrary;
using System;

namespace BuildingLibrary
{
    public class Building
    {
        public string Address { get; set; }
        public string WallMaterial { get; set; } 
        public int Floors { get; set; }

        public Building() { }

        public Building(string address, string wallMaterial, int floors)
        {
            Address = address;
            WallMaterial = wallMaterial;
            Floors = floors;
        }

        public Building(Building building)
        {
            Address = building.Address;
            WallMaterial = building.WallMaterial;
            Floors = building.Floors;
        }

        public void ChangeWallMaterial(string material)
        {
            WallMaterial = material;
        }

        public void ChangeFloors(int floors)
        {
            Floors = floors;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Address: {Address}, Wall Material: {WallMaterial}, Floors: {Floors}");
        }
    }

    public class ApartmentBuilding : Building
    {
        public int Apartments { get; set; }

        public ApartmentBuilding() { }

        public ApartmentBuilding(string address, string wallMaterial, int floors, int apartments)
            : base(address, wallMaterial, floors)
        {
            Apartments = apartments;
        }

        public ApartmentBuilding(ApartmentBuilding apartmentBuilding) : base(apartmentBuilding)
        {
            Apartments = apartmentBuilding.Apartments;
        }

        public void ChangeApartments(int apartments)
        {
            Apartments = apartments;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Apartments: {Apartments}");
        }
    }

    public class Warehouse : Building
    {
        public string LayoutType { get; set; } 

        public Warehouse() { }

        public Warehouse(string address, string wallMaterial, int floors, string layoutType)
            : base(address, wallMaterial, floors)
        {
            LayoutType = layoutType;
        }

        public Warehouse(Warehouse warehouse) : base(warehouse)
        {
            LayoutType = warehouse.LayoutType;
        }

        public void ChangeLayoutType(string layoutType)
        {
            LayoutType = layoutType;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Layout Type: {LayoutType}");
        }
    }
}

namespace FractionLibrary
{
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator != 0 ? denominator : throw new ArgumentException("Denominator cannot be zero.");
        }

        public static Fraction operator +(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator).Reduce();

        public static Fraction operator -(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator).Reduce();

        public static Fraction operator *(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator).Reduce();

        public static Fraction operator /(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator).Reduce();

        public static bool operator >(Fraction a, Fraction b) => (double)a > (double)b;

        public static bool operator <(Fraction a, Fraction b) => (double)a < (double)b;

        public static bool operator >=(Fraction a, Fraction b) => (double)a >= (double)b;

        public static bool operator <=(Fraction a, Fraction b) => (double)a <= (double)b;

        public static bool operator ==(Fraction a, Fraction b) => a.Numerator * b.Denominator == b.Numerator * a.Denominator;

        public static bool operator !=(Fraction a, Fraction b) => !(a == b);

        public static implicit operator double(Fraction fraction) =>
            (double)fraction.Numerator / fraction.Denominator;

        public Fraction Reduce()
        {
            int gcd = GCD(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;
            return this;
        }

        private static int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);

        public override string ToString() => $"{Numerator}/{Denominator}";

        public override bool Equals(object obj) => obj is Fraction fraction && this == fraction;

        public override int GetHashCode()
        {
            return (Numerator * 17) ^ (Denominator * 31);
        }
    }
}



namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Робота з класами Building, ApartmentBuilding, Warehouse");
                Console.WriteLine("2. Робота з класом Fraction");
                Console.WriteLine("0. Вихід");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleBuildings();
                        break;
                    case "2":
                        HandleFractions();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неправильний вибір!");
                        break;
                }
            }
        }

        static void HandleBuildings()
        {
            Building[] buildings =
            {
                new ApartmentBuilding("Main St", "Brick", 5, 20),
                new Warehouse("Industrial Zone", "Concrete", 1, "Open")
            };

            foreach (var building in buildings)
            {
                building.ShowInfo();
                Console.WriteLine();
            }
        }

        static void HandleFractions()
        {
            Fraction a = new Fraction(1, 2);
            Fraction b = new Fraction(3, 4);

            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine($"a + b = {a + b}");
            Console.WriteLine($"a - b = {a - b}");
            Console.WriteLine($"a * b = {a * b}");
            Console.WriteLine($"a / b = {a / b}");
            Console.WriteLine($"a > b: {a > b}");
            Console.WriteLine($"a == b: {a == b}");
        }
    }
}
