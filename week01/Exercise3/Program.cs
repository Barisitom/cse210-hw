using System;

class Program
{
    static void Main(string[] args)
    {
        // Generate a random magic number between 1 and 100
        Random random = new Random();
        int magicNumber = random.Next(1, 101);

        int guess = 0;
        int attempts = 0;

        Console.WriteLine("Welcome to Guess My Number!");

        // Loop until the user guesses the magic number
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = Convert.ToInt32(Console.ReadLine());
            attempts++;

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine($"You guessed it! It took you {attempts} attempts.");
            }
        }
    }
}