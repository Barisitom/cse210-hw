// Program.cs
using System;
using System.Collections.Generic;

// Represents a comment on a YouTube video
public class Comment
{
    public string CommenterName { get; private set; }
    public string CommentText { get; private set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

// Represents a YouTube video
public class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int LengthInSeconds { get; private set; }

    private List<Comment> comments = new List<Comment>();

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of videos
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("Exploring the Amazon Rainforest", "NatureWorld", 540);
        video1.AddComment(new Comment("Alice", "Amazing visuals and camera work!"));
        video1.AddComment(new Comment("John", "I learned so much, thank you."));
        video1.AddComment(new Comment("Maria", "This is so relaxing to watch."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Top 10 Programming Tips", "CodeWithChris", 720);
        video2.AddComment(new Comment("DevGuy", "Tip #4 was a game-changer!"));
        video2.AddComment(new Comment("Lucy", "Thanks for the clear explanation."));
        video2.AddComment(new Comment("Ben", "Loved the practical examples."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("How to Bake the Perfect Cake", "BakingHeaven", 480);
        video3.AddComment(new Comment("ChefEmma", "I tried it and it came out perfect!"));
        video3.AddComment(new Comment("Toby", "Can you make a vegan version?"));
        video3.AddComment(new Comment("Rita", "Loved this recipe, so easy to follow."));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("SpaceX Starship Launch Explained", "ScienceToday", 600);
        video4.AddComment(new Comment("AstroNerd", "This was so informative."));
        video4.AddComment(new Comment("Sally", "SpaceX is really pushing boundaries."));
        video4.AddComment(new Comment("Neo", "The animation made it easy to understand."));
        videos.Add(video4);

        // Display video info and comments
        foreach (Video video in videos)
        {
            Console.WriteLine("Title: " + video.Title);
            Console.WriteLine("Author: " + video.Author);
            Console.WriteLine("Length: " + video.LengthInSeconds + " seconds");
            Console.WriteLine("Number of Comments: " + video.GetNumberOfComments());
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment.CommenterName}: {comment.CommentText}");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}
