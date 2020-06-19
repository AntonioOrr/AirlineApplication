using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//@Author: Antonio Orr
//NOTE: Fragile program. Not following right inputs can result in error
namespace Airline
{

    class Program
    {
        static void Main(string[] args)
        {
            //AirlineService object used to show seating of airplane
            AirlineService airplane = new AirlineService();
            Console.WriteLine("Thank you for supporting Fly-By-Night Airlines. Here is our list of available seats: ");
            AirlineService.showSeating(airplane);
            int command = 0;
            //while seats are available and user does not type 4 to quit
            while (AirlineService.availableSeats && command != 4)
            {
                //outputs list of commands to choose from
                AirlineService.commandList();
                command = Convert.ToInt32(Console.ReadLine());
                //add passengers
                if (command == 1)
                {
                    Console.WriteLine("You chose to add passenger(s).");
                    AirlineService.addPassengers();
                }
                //show all airplane seatings
                else if (command == 2)
                {
                    Console.WriteLine("You chose to view seating.");
                    AirlineService.showSeating(airplane);
                }
                //collect all passengers info and write them to a single text file (One ticket per passenger).
                //note: text file deletes a previous text file with the same name
                else if (command == 3)
                {
                    Console.WriteLine("You chose to print out the tickets.");
                    int counter = AirlineService.ticketsPrinted;
                    //does not create text file if number of tickets printed match the current number of assigned seats
                    if (counter == AirlineService.tickets.Count)
                        Console.WriteLine("There are no tickets to print.");
                    else
                    {
                        if (counter == 0)
                            AirlineService.TicketPrinting(0);
                        else
                            AirlineService.TicketPrinting(counter);
                    }
                }
                else
                    Console.WriteLine("Please input an integer ranging from 1 to 4.");
            }
            Console.WriteLine("There are either no more seats available or you chose to quit program. Goodbye!");
            Console.ReadLine();
        }
    }
    //this class is filled with methods used for the program
    class AirlineService
    {
        //separate 3-dimensional arrays of first class and economy class areas
        public static string[,,] firstClass = new string[5, 2, 2];
        public static string[,,] econClass = new string[15, 2, 3];
        //static field that determines seat preference availability
        public static bool availableSeat = true;
        //static fields that determine whether or not any seats are available
        //NOTE: Since there were so many seats, this field has not been tested
        public static bool availableSeats = true;
        //list of tickets containing AssignedSeat objects
        public static List<AssignedSeat> tickets = new List<AssignedSeat>();
        //field that counts number of tickets printed
        public static int ticketsPrinted = 0;
        //arrays start out with "O" strings
        public AirlineService()
        {
            //string[,,] firstClass = new string[5,2,2];
            //string[,,] econClass = new string[15, 2, 3];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {

                        firstClass[i, j, k] = "O";
                    }
                }
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        econClass[i, j, k] = "O";
                    }
                }
            }
        }
        //method that adds passengers to ticket list (not printed yet)
        public static void addPassengers()
        {
            Console.WriteLine("Input number indicating class seating: ");
            Console.WriteLine("1. First-Class Seating");
            Console.WriteLine("2. Economy-Class Seating");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many passengers are travelling together? (NOTE: You can only have a maximum of 2 first-class passengers" +
                " and 3 economy-class passengers)");
            Console.WriteLine("ALSO NOTE: Please input only integers. Also a number of 0 or negative will not be accepted.");
            int passengerCount = Convert.ToInt32(Console.ReadLine());
            if (choice == 1 && passengerCount <= 2 && passengerCount > 0)
                firstClassSeating(passengerCount);
            else if (choice == 2 && passengerCount <= 3 && passengerCount > 0)
                econClassSeating(passengerCount);
            else
                Console.WriteLine("Unable to fulfill request. Returning to command list.");

        }
        //method for choosing first class
        public static void firstClassSeating(int numPassengers)
        {
            int pref;
            for (int i = 0; i < numPassengers; i++)
            {
                Console.WriteLine("Input number indicating seat preference: ");
                Console.WriteLine("1. Aisle");
                Console.WriteLine("2. Window");
                pref = Convert.ToInt32(Console.ReadLine());
                updateSeating(1, pref);
            }


        }
        //method for choosing economy class
        public static void econClassSeating(int numPassengers)
        {
            int pref;
            for (int i = 0; i < numPassengers; i++)
            {
                Console.WriteLine("Input number indicating seat preference: ");
                Console.WriteLine("1. Center");
                Console.WriteLine("2. Window");
                pref = Convert.ToInt32(Console.ReadLine());
                updateSeating(2, pref);
            }
        }
        //shows the current state of airplane seats
        public static void showSeating(AirlineService a)
        {
            //string[,,] firstClass = new string[5,2,2];
            //string[,,] econClass = new string[15, 2, 3];
            Console.WriteLine("O = Spot Available. X = Spot Taken/Unavailable");
            Console.WriteLine("First Class: ");
            Console.WriteLine("        A" + "   " + "C" + "        " + "D" + "   " + "F");
            for (int i = 0; i < 5; i++)
            {
                if (i == 0) Console.Write("1       ");
                else if (i == 1) Console.Write("2       ");
                else if (i == 2) Console.Write("3       ");
                else if (i == 3) Console.Write("4       "); else Console.Write("5       ");

                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {

                        Console.Write(firstClass[i, j, k] + "   ");
                    }
                    Console.Write("     ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Economy Class:");
            Console.WriteLine("      A" + "  B " + " C" + "        " + "D" + "  E  " + "F");
            for (int i = 0; i < 15; i++)
            {
                if (i == 0) Console.Write("6     ");
                else if (i == 1) Console.Write("7     ");
                else if (i == 2) Console.Write("8     ");
                else if (i == 3) Console.Write("9     ");
                else if (i == 4) Console.Write("10    ");
                else if (i == 5) Console.Write("11    ");
                else if (i == 6) Console.Write("12    ");
                else if (i == 7) Console.Write("13    ");
                else if (i == 8) Console.Write("14    ");
                else if (i == 9) Console.Write("15    ");
                else if (i == 10) Console.Write("16    ");
                else if (i == 11) Console.Write("17    ");
                else if (i == 12) Console.Write("18    "); else if (i == 13) Console.Write("19    "); else Console.Write("20    ");

                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Console.Write(econClass[i, j, k] + "  ");
                    }
                    Console.Write("      ");
                }
                Console.WriteLine("");
            }
        }
        //updates seating area based on number of passengers, class, and seat preference
        public static void updateSeating(int classType, int pref)
        {
            availableSeat = true;
            AssignedSeat ticket;
            AssignedSeat seat;
            int opening = 0;
            if (classType == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            if (pref == 1)
                            {
                                if (j == 0 && k == 1 && firstClass[i, j, k] == "O" || j == 1 && k == 0 && firstClass[i, j, k] == "O")
                                {
                                    firstClass[i, j, k] = "X";
                                    opening = 1;
                                    seat = new AssignedSeat(Name(), seatNumber(1, i, j, k));
                                    tickets.Add(seat);
                                    goto End;
                                }
                            }
                            else
                            {
                                if (j == 0 && k == 0 && firstClass[i, j, k] == "O" || j == 1 && k == 1 && firstClass[i, j, k] == "O")
                                {
                                    firstClass[i, j, k] = "X";
                                    opening = 1;
                                    seat = new AssignedSeat(Name(), seatNumber(1, i, j, k));
                                    tickets.Add(seat);
                                    goto End;
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            if (pref == 1)
                            {
                                if (j == 0 && k == 1 && econClass[i, j, k] == "O" || j == 1 && k == 1 && econClass[i, j, k] == "O")
                                {
                                    econClass[i, j, k] = "X";
                                    opening = 1;
                                    seat = new AssignedSeat(Name(), seatNumber(2, i, j, k));
                                    tickets.Add(seat);
                                    goto End;
                                }
                            }
                            else
                            {
                                if (j == 0 && k == 0 && econClass[i, j, k] == "O" || j == 1 && k == 2 && econClass[i, j, k] == "O")
                                {
                                    econClass[i, j, k] = "X";
                                    opening = 1;
                                    seat = new AssignedSeat(Name(), seatNumber(2, i, j, k));
                                    tickets.Add(seat);
                                    goto End;
                                }
                            }
                        }
                    }
                }
            }
        End:;
            if (opening == 0)
            {
                availableSeat = false;
                Console.WriteLine("Sorry, no match found. Assigning passenger to an available seat...");
                randomSeat(classType);
            }
            else
            {
                Console.WriteLine("Match has been found!");
                Console.WriteLine("Ticket has been created.");
                ticket = tickets[tickets.Count - 1];
                Console.WriteLine("Ticket #" + tickets.Count);
                ticket.TicketInfo();

            }
        }
        //method that is called if there aren't any seats that match the passenger's preference
        public static void randomSeat(int classType)
        {
            bool seatFound = false;
            AssignedSeat ticket;
            if (classType == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            if (firstClass[i, j, k] == "O")
                            {
                                firstClass[i, j, k] = "X";
                                AssignedSeat rand = new AssignedSeat(Name(), seatNumber(1, i, j, k));
                                tickets.Add(rand);
                                Console.WriteLine("Ticket has been created.");
                                ticket = tickets[tickets.Count - 1];
                                Console.WriteLine("Ticket #" + tickets.Count);
                                ticket.TicketInfo();
                                goto End;
                            }
                        }
                    }
                }
            }
            else if (classType == 2 || !seatFound)
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            if (econClass[i, j, k] == "O")
                            {
                                econClass[i, j, k] = "X";
                                AssignedSeat rand = new AssignedSeat(Name(), seatNumber(2, i, j, k));
                                tickets.Add(rand);
                                Console.WriteLine("Ticket has been created.");
                                ticket = tickets[tickets.Count - 1];
                                Console.WriteLine("Ticket #" + tickets.Count);
                                ticket.TicketInfo();
                                goto End;
                            }
                        }
                    }
                }
            }
            //if both classes have no available seats
            else
            {
                Console.WriteLine("Oh no! There are no more spots left!");
                availableSeats = false;
            }
        End:;
        }
        //method that asks for passenger name
        public static string Name()
        {
            string name;
            Console.WriteLine("Enter the name of this passenger: ");
            name = Console.ReadLine();
            return name;
        }
        //method that finds seat number based on seat map
        public static string seatNumber(int cla, int i, int j, int k)
        {
            string letter = "";
            string number = "";
            if (cla == 1)
            {
                if (i == 0) number = "1";
                else if (i == 1) number = "2";
                else if (i == 2) number = "3";
                else if (i == 3) number = "4";
                else number = "5";
                if (j == 0 && k == 0) letter = "A";
                else if (j == 0 && k == 1) letter = "C";
                else if (j == 1 && k == 0) letter = "D"; else letter = "F";
            }
            else
            {
                if (i == 0) number = "6";
                else if (i == 1) number = "7";
                else if (i == 2) number = "8";
                else if (i == 3) number = "9";
                else if (i == 4) number = "10";
                else if (i == 5) number = "11";
                else if (i == 6) number = "12";
                else if (i == 7) number = "13";
                else if (i == 8) number = "14";
                else if (i == 9) number = "15";
                else if (i == 10) number = "16";
                else if (i == 11) number = "17";
                else if (i == 12) number = "18";
                else if (i == 13) number = "19"; else number = "20";
                if (j == 0 && k == 0) letter = "A";
                else if (j == 0 && k == 1) letter = "B";
                else if (j == 0 && k == 2) letter = "C";
                else if (j == 1 && k == 0) letter = "D";
                else if (j == 1 && k == 1) letter = "E";
                else letter = "F";
            }
            string seatNum = letter + number;
            return seatNum;
        }
        //method that outputs directions for four main commands of the program
        public static void commandList()
        {
            Console.WriteLine("Input number indicating command: ");
            Console.WriteLine("1. Add Passengers");
            Console.WriteLine("2. Show Seating");
            Console.WriteLine("3. Create Ticket(s)");
            Console.WriteLine("4. Quit");
        }
        //method that creates the text file of tickets to be printed
        public static void TicketPrinting(int counter)
        {
            AssignedSeat ticket;
            string ticketInfo;
            Console.WriteLine("Printing tickets...");
            if (File.Exists("Tickets.txt"))
            {
                File.Delete("Tickets.txt");
            }
            for (int i = counter; i < tickets.Count; i++)
            {
                ticketsPrinted++;
                ticket = tickets[i];
                Console.WriteLine("Ticket #" + (ticketsPrinted));
                ticket.TicketInfo();
                ticketInfo = ticket.TicketI();
                Console.WriteLine("------------------------------------------------");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("Tickets.txt",true))
                {
                    file.WriteLine("------------------------------------------------");
                    file.WriteLine("Ticket # " + ticketsPrinted);
                    file.WriteLine(ticketInfo);
                    file.WriteLine();
                    file.WriteLine("FLY-BY-NIGHT AIRLINES");
                    file.WriteLine("------------------------------------------------");
                }
            }
        }
        //class for AssignedSeat objects
        public class AssignedSeat
        {
            //field for passenger name
            public string name;
            //field for seat number
            public string seatNum;
            public AssignedSeat(string n, string seatN)
            {
                name = n;
                seatNum = seatN;
            }
            //method that outputs a single ticket's information 
            public void TicketInfo()
            {
                Console.WriteLine("Name: " + name + "   Assigned Seat Number: " + seatNum);
            }
            //method that returns string of ticket info
            public string TicketI()
            {
                return "Name: " + name + "   Assigned Seat Number: " + seatNum;
            }
        }

    }
}
