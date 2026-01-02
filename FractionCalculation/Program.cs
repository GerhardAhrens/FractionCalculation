//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2025
// </copyright>
// <Template>
// 	Version 2.0.2025.2, 28.4.2025
// </Template>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>02.01.2026</date>
//
// <summary>
// Konsolen Applikation mit Menü
// </summary>
//-----------------------------------------------------------------------

/* Imports from NET Framework */
using System;

namespace FractionCalculation
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ConsoleMenu.Add("1", "Demo Bruchrechnen", () => MenuPoint1());
            ConsoleMenu.Add("X", "Beenden", () => ApplicationExit());

            do
            {
                _ = ConsoleMenu.SelectKey(2, 2);
            }
            while (true);
        }

        private static void ApplicationExit()
        {
            Environment.Exit(0);
        }

        private static void MenuPoint1()
        {
            Console.Clear();

            Bruch a = new Bruch(1, 2);
            Bruch b = new Bruch(3, 4);

            Console.WriteLine($"Bruch A = {a.ToString()}");
            Console.WriteLine($"Bruch B = {b.ToString()}");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"1/2 + 3/4 = {a + b}"); // 5/4
            Console.WriteLine($"1/2 - 3/4 = {a - b}"); // -1/4
            Console.WriteLine($"1/2 * 3/4 = {a * b}"); // 3/8
            Console.WriteLine($"1/2 + 3/4 = {a / b}"); // 2/3
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"1/2.ToString() = {a.ToString()}");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"1/2.GetHashCode() = {a.GetHashCode()}");
            Console.WriteLine($"3/4.GetHashCode() = {b.GetHashCode()}");

            bool isEqual = new Bruch(2, 4) == new Bruch(1, 2);
            Console.WriteLine($"2/4 == 1/2 = {isEqual}");

            ConsoleMenu.Wait();
        }

    }

    public struct Bruch
    {
        public Bruch(int zaehler, int nenner)
        {
            if (nenner == 0)
            {
                throw new ArgumentException("Nenner darf nicht 0 sein.");
            }

            // Vorzeichen vereinheitlichen
            if (nenner < 0)
            {
                zaehler = -zaehler;
                nenner = -nenner;
            }

            int ggt = Ggt(Math.Abs(zaehler), nenner);

            this.Zaehler = zaehler / ggt;
            this.Nenner = nenner / ggt;
        }

        public int Zaehler { get; }
        public int Nenner { get; }

        public static Bruch operator +(Bruch a, Bruch b)
        {
            return new Bruch(a.Zaehler * b.Nenner + b.Zaehler * a.Nenner, a.Nenner * b.Nenner);
        }

        public static Bruch operator -(Bruch a, Bruch b)
        {
            return new Bruch(a.Zaehler * b.Nenner - b.Zaehler * a.Nenner, a.Nenner * b.Nenner);
        }

        public static Bruch operator *(Bruch a, Bruch b)
        {
            return new Bruch(a.Zaehler * b.Zaehler, a.Nenner * b.Nenner);
        }

        public static Bruch operator /(Bruch a, Bruch b)
        {
            if (b.Zaehler == 0)
            {
                throw new DivideByZeroException("Division durch 0 ist nicht erlaubt.");
            }

            return new Bruch(a.Zaehler * b.Nenner, a.Nenner * b.Zaehler);
        }

        public static bool operator ==(Bruch a, Bruch b)
        {
            if (ReferenceEquals(a, b) == false)
            {
                return true;
            }

            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Zaehler == b.Zaehler && a.Nenner == b.Nenner;
        }

        public static bool operator !=(Bruch a, Bruch b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is Bruch b && this == b;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Zaehler, this.Nenner);
        }

        public override string ToString()
        {
            return $"{this.Zaehler}/{this.Nenner}";
        }

        private static int Ggt(int a, int b)
        {
            while (b != 0)
            {
                int rest = a % b;
                a = b;
                b = rest;
            }

            return a;
        }
    }
}
