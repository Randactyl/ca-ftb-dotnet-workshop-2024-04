using FTB.Common;

/*
 * The first sample is a simple program that writes "Hello, World!" to the console.
 * The second sample reads a line of text from the console and writes it back to the console.
 */

// First Sample
//string hello = "Hello, World!";
//Console.WriteLine(hello);

// Second Sample
//string input = Console.ReadLine();
//Console.WriteLine(input);

// sample reserved word
//string @int = "9";
//Console.WriteLine(@int);

//string Hello = "hello there";
//Console.WriteLine(Hello);

//string a = "a";
//string b = "b";
//string c = a + b;
//Console.WriteLine(c);

//int x = 5;
//int y = 10;
//int z = x + y;
//Console.WriteLine(z);

//int a, b, c;
//a = 3;
//b = 10;

//a = a + 2;
//a += 2;

//c = a++;
//b = a + b * c;
//Console.Read();

//DateTime employeeStartDate = new(2019, 1, 1);
//DateTime today = DateTime.Today;
//DateTime twoDaysFromToday = today.AddDays(2);
//DayOfWeek dayOfWeek = twoDaysFromToday.DayOfWeek;
//bool isDST = today.IsDaylightSavingTime();
//DateOnly holiday = new(2024, 12, 25);
//Console.Read();

//int x = 5;
//long y = x;

//double z = 12345.0;
//int a = (int)z;
//Console.Read();

//Console.Write("Enter your age: ");
//int age = Convert.ToInt32(Console.ReadLine());

//switch (age)
//{
//    case < 18:
//        Console.WriteLine("You are a minor.");
//        break;
//    case > 65:
//        Console.WriteLine("You are a senior citizen.");
//        break;
//    case 42:
//        Console.WriteLine("You are the answer to the ultimate question of life, the universe, and everything.");
//        break;
//    default:
//        Console.WriteLine("You are an adult.");
//        break;
//}

//// while loop demo
//int i = 10;
//while (i < 10)
//{
//    Console.WriteLine(i);
//    i++;
//}

//// do-while loop demo
//int j = 10;
//do
//{
//    Console.WriteLine(j);
//    j++;
//} while (j < 10);

//// for loop demo
//for (int k = 0; k < 10; k++)
//{
//    Console.WriteLine(k);
//}

//int sumOfNumbers = AddNumbers(5, 10);
//Console.WriteLine(sumOfNumbers);

//sumOfNumbers = AddNumbers(b: 5, a: 10, c: 50);
//Console.WriteLine(sumOfNumbers);
//return;


//static int AddNumbers(int a, int b, int c = 100)
//{
//    return a + b + c;
//}

//string name = "Alice";
//int age = 42;

////string message = "Hello, " + name + ". You are " + age + " years old.";
////string message = string.Format("Hello, {0}. You are {1} years old.", name, age);
//string message = $"Hello, \"{name}\".\nYou are {age} years old.";
//Console.WriteLine(message);

//string text = "false";
//if (bool.TryParse(text, out bool parsedText))
//{
//    Console.WriteLine($"The value is {parsedText}");
//}
//else
//{
//    Console.WriteLine("The value is not a boolean.");
//}

//Console.Read();

// Test Elo Calculator
double winnerRating = 1000;
double loserRating = 1200;

Console.WriteLine($"Winner rating before Elo calculation: {winnerRating}");
Console.WriteLine($"Loser rating before Elo calculation: {loserRating}");

EloCalculationModel elo = EloCalculator.CalculateElo(winnerRating, loserRating);

Console.WriteLine();
Console.WriteLine($"New winner rating: {winnerRating + elo.WinnerRating}");
Console.WriteLine($"New loser rating: {loserRating + elo.LoserRating}");
