// Eternal Quest Program
using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public virtual string GetDetailsString()
    {
        return $"[ ] {_name} ({_description})";
    }
    public abstract string GetStringRepresentation();
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetailsString()
    {
        return ($"[{(_isComplete ? "X" : " ")}] {_name} ({_description})");
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) {}

    public override int RecordEvent() => _points;

    public override bool IsComplete() => false;

    public override string GetDetailsString()
    {
        return $"[∞] {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_name}|{_description}|{_points}";
    }
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _completedCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _completedCount = 0;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        _completedCount++;
        if (_completedCount == _targetCount)
        {
            return _points + _bonus;
        }
        return _points;
    }

    public override bool IsComplete() => _completedCount >= _targetCount;

    public override string GetDetailsString()
    {
        return ($"[{(_completedCount >= _targetCount ? "X" : " ")}] {_name} ({_description}) -- Completed {_completedCount}/{_targetCount}");
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_bonus}|{_targetCount}|{_completedCount}";
    }
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreateGoal()
    {
        Console.WriteLine("Select goal type: 1) Simple 2) Eternal 3) Checklist");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string description = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        Goal goal = null;

        switch (type)
        {
            case "1":
                goal = new SimpleGoal(name, description, points);
                break;
            case "2":
                goal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("Target Count: ");
                int count = int.Parse(Console.ReadLine());
                Console.Write("Bonus: ");
                int bonus = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(name, description, points, count, bonus);
                break;
        }

        if (goal != null)
            _goals.Add(goal);
    }

    public void ListGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void RecordEvent()
    {
        ListGoals();
        Console.Write("Select goal to record: ");
        int choice = int.Parse(Console.ReadLine());
        if (choice > 0 && choice <= _goals.Count)
        {
            _score += _goals[choice - 1].RecordEvent();
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Current Score: {_score}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            foreach (Goal g in _goals)
            {
                writer.WriteLine(g.GetStringRepresentation());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        _goals.Clear();
        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            string type = parts[0];

            switch (type)
            {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])));
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                    break;
                case "ChecklistGoal":
                    var g = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[4]));
                    typeof(ChecklistGoal).GetField("_completedCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(g, int.Parse(parts[6]));
                    _goals.Add(g);
                    break;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        string input;

        do
        {
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Create Goal\n2. List Goals\n3. Record Event\n4. Show Score\n5. Save Goals\n6. Load Goals\n7. Exit");
            Console.Write("Select an option: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1": manager.CreateGoal(); break;
                case "2": manager.ListGoals(); break;
                case "3": manager.RecordEvent(); break;
                case "4": manager.DisplayScore(); break;
                case "5":
                    Console.Write("Filename to save: ");
                    manager.SaveGoals(Console.ReadLine());
                    break;
                case "6":
                    Console.Write("Filename to load: ");
                    manager.LoadGoals(Console.ReadLine());
                    break;
            }
        } while (input != "7");
    }
}
