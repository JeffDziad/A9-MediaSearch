using System.IO;
using System;
using System.Collections.Generic;
using A8_MediaSearch.Data;

namespace A8_MediaSearch
{

    public class Menu
    {
        private static List<IRepository> validRepositories = new List<IRepository>();

        //Add New Repository Class to list of Interfaced Classes.
        public static void grabRepositories()
        {
            validRepositories.Add(new FileRepository());
            validRepositories.Add(new JSONRepository());
        }

        public static int getBaseRepositoryLineNum(int mediaCode)
        {
            return validRepositories[0].getLineNum(mediaCode);
        }

        public static Boolean endProgram {get; set;} = false;

        public static void runMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. View Media from File");
            Console.WriteLine("2. Add Media to File");
            Console.WriteLine("3. Exit");
            Console.Write("> ");
            string userInputStr = Console.ReadLine();
            int userInputInt;
            try
            {
                userInputInt = Convert.ToInt32(userInputStr);
                handleMenuInput(userInputInt);
            }catch(FormatException fe)
            {
                Console.Clear();
                Log.log($"\"{userInputStr}\" is not a valid menu option.", fe);
            }
        }

        public static void handleMenuInput(int input)
        {
            switch(input)
            {
                case 1:
                    Console.Clear();
                    viewMedia();
                    break;
                case 2:
                    Console.Clear();
                    addMedia();
                    break;
                case 3:
                    endProgram = true;
                    break;
                default:
                    Console.Clear();
                    Log.log($"{input} is not a valid menu option.", new FormatException());
                    break;
            }
        }

        public static void viewMedia()
        {
            int repositorySelection = getRepo(); 
            if(repositorySelection == -1)
            {
                return;
            }
            Console.WriteLine("Choose Media Type to view: ");
            Console.WriteLine("1. Movies");
            Console.WriteLine("2. Shows");
            Console.WriteLine("3. Videos");
            Console.WriteLine("4. Return to Main Menu");
            Console.Write("> ");
            string userInputStr = Console.ReadLine();
            try
            {
                int userInputInt = Convert.ToInt32(userInputStr);
                switch(userInputInt)
                {
                    case 1:
                        Console.Clear();
                        displayMedia(1, repositorySelection);
                        break;
                    case 2:
                        Console.Clear();
                        displayMedia(2, repositorySelection);
                        break;
                    case 3:
                        Console.Clear();
                        displayMedia(3, repositorySelection);
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Log.log($"\"{userInputInt}\" is not a valid menu option.", new FormatException());
                        break;
                }
            }catch(FormatException fe)
            {
                Console.Clear();
                Log.log($"\"{userInputStr}\" is not a valid menu option.", fe);
            }
        }

        public static int getRepo()
        {
            Console.WriteLine("Which Repository would you like to read from?");
            int count = 1;
            foreach(IRepository repo in validRepositories)
            {
                Console.WriteLine($"{count}. {repo.getName()}");
                count++;
            }
            Console.Write("> ");
            string userInputStr = Console.ReadLine();
            int userInputInt;
            try
            {
                userInputInt = Convert.ToInt32(userInputStr);
                {
                    if(validRepositories[userInputInt - 1] is IRepository)
                    {
                        Console.Clear();
                        return userInputInt - 1;
                    }else
                    {
                        Console.Clear();
                        Log.logX($"\"{userInputStr}\" is not a valid menu option.");
                        return -1;
                    }
                }
            }catch(Exception e)
            {
                Console.Clear();
                Log.log($"\"{userInputStr}\" is not a valid menu option.", e);
            }
            Console.Clear();
            return -1;
        }

        public static void displayMedia(int mediaCode, int repository)
        {
            Console.WriteLine("View Methods:");
            Console.WriteLine("1. View All");
            Console.WriteLine("2. Search by ID");
            Console.WriteLine("3. Search by Title");
            Console.WriteLine("4. Search by Genre");
            Console.WriteLine("5. Return to Main Menu");
            Console.Write("> ");
            string userInputStr = Console.ReadLine();
            try
            {
                int userInputInt = Convert.ToInt32(userInputStr);
                switch(userInputInt)
                {
                    case 1:
                        Console.Clear();
                        validRepositories[repository].viewAll(mediaCode);
                        break;
                    case 2:
                        Console.Clear();
                        validRepositories[repository].searchById(mediaCode);
                        break;
                    case 3:
                        Console.Clear();
                        validRepositories[repository].searchByTitle(mediaCode);
                        break;
                    case 4:
                        Console.Clear();
                        validRepositories[repository].searchByGenre(mediaCode);
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Log.log($"\"{userInputInt}\" is not a valid menu option.", new FormatException());
                        break;
                }
            }catch(FormatException fe)
            {
                Console.Clear();
                Log.log($"\"{userInputStr}\" is not a valid menu option.", fe);
            }
        }

        public static void addMedia()
        {
            Console.WriteLine("Choose Media Type to Create:");
            Console.WriteLine("1. Movie");
            Console.WriteLine("2. Show");
            Console.WriteLine("3. Video");
            Console.WriteLine("4. Return to Main Menu");
            Console.Write("> ");
            string userInputStr = Console.ReadLine();
            List<string> newMedia;
            try
            {
                int userInputInt = Convert.ToInt32(userInputStr);
                switch(userInputInt)
                {
                    case 1:
                        Console.Clear();
                        newMedia = MediaManipulator.createMovie();
                        foreach(IRepository repo in validRepositories)
                        {
                            repo.writeData(newMedia, "movie");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        newMedia = MediaManipulator.createShow();
                        foreach(IRepository repo in validRepositories)
                        {
                            repo.writeData(newMedia, "show");
                        }
                        break;
                    case 3:
                        Console.Clear();
                        newMedia = MediaManipulator.createVideo();
                        foreach(IRepository repo in validRepositories)
                        {
                            repo.writeData(newMedia, "video");
                        }
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Log.log($"\"{userInputInt}\" is not a valid menu option.", new FormatException());
                        break;
                }
            }catch(FormatException fe)
            {
                Console.Clear();
                Log.log($"\"{userInputStr}\" is not a valid menu option.", fe);
            }
        }

    }

}