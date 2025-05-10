using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int number = Convert.ToInt32(Console.ReadLine());

            if (number == 0)
                break;

            numbers.Add(number);
        }

        // Calculate the sum
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }

        // Calculate the average
        double average = (numbers.Count > 0) ? (double)sum / numbers.Count : 0;

        // Find the largest number
        int maxNumber = (numbers.Count > 0) ? numbers[0] : 0;
        foreach (int num in numbers)
        {
            if (num > maxNumber)
                maxNumber = num;
        }

        // Display the results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNumber}");
    
    }
}