using System.IO;
using System;
using System.Collections.Generic;
using A8_MediaSearch.Models;
using ConsoleTables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace A8_MediaSearch.Data
{
    public class JSONRepository : IRepository
    {
        private static readonly string moviesPath = Path.Combine(Environment.CurrentDirectory, "MediaData", "movies.JSON");
        private static readonly string showsPath = Path.Combine(Environment.CurrentDirectory, "MediaData", "shows.JSON");
        private static readonly string videosPath = Path.Combine(Environment.CurrentDirectory, "MediaData", "videos.JSON");

        private static string repositoryName = "JSON Repository";

        private List<Media> mediaList;

        public string getName()
        {
            return repositoryName;
        }

        public int getLineNum(int mediaCode)
        {
            return 0;
        }

        public List<Media> getMediaList(int mediaCode)
        {
            mediaList = new List<Media>();
            if(mediaCode.Equals(1))
            {
                string moviesJSON = File.ReadAllText(moviesPath);
                List<Movie> moviesDeserialized = JsonConvert.DeserializeObject<List<Movie>>(moviesJSON);
                if(moviesDeserialized == null)
                {
                    return mediaList = new List<Media>();
                }
                else
                {
                    foreach(Movie movie in moviesDeserialized)
                    {
                        mediaList.Add(movie);
                    }
                    return mediaList;
                }
            }
            else if(mediaCode.Equals(2))
            {
                string showsJSON = File.ReadAllText(showsPath);
                List<Show> showsDeserialized = JsonConvert.DeserializeObject<List<Show>>(showsJSON);
                if(showsDeserialized == null)
                {
                    return mediaList = new List<Media>();
                }
                else
                {
                    foreach(Show show in showsDeserialized)
                    {
                        mediaList.Add(show);
                    }
                    return mediaList;
                }
            }
            else
            {
                string videosJSON = File.ReadAllText(videosPath);
                List<Video> videosDeserialized = JsonConvert.DeserializeObject<List<Video>>(videosJSON);
                if(videosDeserialized == null)
                {
                    return mediaList = new List<Media>();
                }
                else
                {
                    foreach(Video video in videosDeserialized)
                    {
                        mediaList.Add(video);
                    }
                    return mediaList;
                }
            }
        }

        public string getPath(string strPath)
        {
            switch(strPath)
            {
                case "movie":
                    return moviesPath;
                case "show":
                    return showsPath;
                default:
                    return videosPath;
            }
        }

        public void consoleTableOut(List<Media> media, int mediaCode)
        {
            switch(mediaCode)
            {
                case 1:
                    var moviesTable = new ConsoleTable("ID", "Title", "Genres");
                    foreach (Movie movie in media)
                    {
                        moviesTable.AddRow(movie.ID, movie.Title, String.Join(",", movie.genres));
                    }
                    moviesTable.Write();
                    break;
                case 2:
                    var showsTable = new ConsoleTable("ID", "Title", "Season", "Episode", "Writers", "Genres");
                    foreach (Show show in media)
                    {
                        showsTable.AddRow(show.ID, show.Title, show.season, show.episode, String.Join(",", show.writers), String.Join(",", show.genres));
                    }
                    showsTable.Write();
                    break;
                case 3:
                    var videosTable = new ConsoleTable("ID", "Title", "Format", "Length (Minutes)", "Regions", "Genres");
                    foreach (Video video in media)
                    {
                        videosTable.AddRow(video.ID, video.Title, video.format, video.length, String.Join(",", video.regions), String.Join(",", video.genres));
                    }
                    videosTable.Write();
                    break;
            }
        }

        public void viewAll(int mediaCode)
        {
            mediaList = getMediaList(mediaCode);
            if(mediaCode.Equals(1))
            {
                var moviesTable = new ConsoleTable("ID", "Title", "Genres");
                foreach (Movie movie in mediaList)
                {
                    moviesTable.AddRow(movie.ID, movie.Title, String.Join(",", movie.genres));
                }
                moviesTable.Write();   
            }
            else if(mediaCode.Equals(2))
            {
                var showsTable = new ConsoleTable("ID", "Title", "Season", "Episode", "Writers", "Genres");
                foreach (Show show in mediaList)
                {
                    showsTable.AddRow(show.ID, show.Title, show.season, show.episode, String.Join(",", show.writers), String.Join(",", show.genres));
                }
                showsTable.Write();
            }
            else
            {
                var videosTable = new ConsoleTable("ID", "Title", "Format", "Length (Minutes)", "Regions", "Genres");
                foreach (Video video in mediaList)
                {
                    videosTable.AddRow(video.ID, video.Title, video.format, video.length, String.Join(",", video.regions), String.Join(",", video.genres));
                }
                videosTable.Write();
            }
        }

        public void searchById(int mediaCode)
        {
            mediaList = getMediaList(mediaCode);
            Console.Write("Enter ID for Search: ");
            string userInputStr = Console.ReadLine();
            int userInputInt;
            try
            {
                userInputInt = Convert.ToInt32(userInputStr);
            }
            catch (FormatException fe)
            {
                Console.Clear();
                Log.log($"{userInputStr} is not a valid ID! Try again...", fe);
                searchById(mediaCode);
            }
            List<Media> searchList = new List<Media>();
            Media media;
            switch (mediaCode)
            {
                case 1:
                    searchList = getMediaList(1);
                    media = new Movie();
                    break;
                case 2:
                    searchList = getMediaList(2);
                    media = new Show();
                    break;
                case 3:
                    searchList = getMediaList(3);
                    media = new Video();
                    break;
            }
            bool foundMatch = false;
            foreach (Media m in searchList)
            {
                if (m.ID == Convert.ToInt32(userInputStr))
                {
                    foundMatch = true;
                    media = m;
                    Console.WriteLine(media.displayConfirmation());
                    break;
                }
            }
            if(foundMatch == false)
            {
                Console.Clear();
                Log.logX($"{userInputStr} was not a valid ID.");
            }
        }

        public void searchByTitle(int mediaCode)
        {
            mediaList = getMediaList(mediaCode);
            Console.Write("Enter a title keyword or year: ");
            string userInputStr = Console.ReadLine();
            List<Media> foundMatches = new List<Media>();
            foreach(Media media in mediaList)
            {
                if(media.Title.ToUpper().Contains(userInputStr.ToUpper()))
                {
                    foundMatches.Add(media);
                }
            }
            if(foundMatches.Count < 0)
            {
                Log.logX($"Cound not find matches for: '{userInputStr}'");
            }else
            {
                consoleTableOut(foundMatches, mediaCode);
            }
        }

        public void searchByGenre(int mediaCode)
        {
            mediaList = getMediaList(mediaCode);
            string userInputStr;
            List<Media> foundMatches = new List<Media>();
            Console.Write("Enter a genre: ");
            userInputStr = Console.ReadLine();
            foreach(Media media in mediaList)
            {
                foreach(string genre in media.genres)
                {
                    if(userInputStr.Contains(genre))
                    {
                        foundMatches.Add(media);
                    }
                }
            }
            if(foundMatches.Count < 0)
            {
                Log.logX($"Cound not find matches for: '{userInputStr}'");
            }else
            {
                consoleTableOut(foundMatches, mediaCode);
            }
        }

        public void writeData(List<string> media, string strPath)
        {
            if(strPath.Equals("movie"))
            {
                Movie movie = new Movie();
                movie.ID = Convert.ToInt32(media[0]);
                movie.Title = media[1];
                movie.genres = media[2].Split("|");
                //Convert movie to JSON HERE
                mediaList = getMediaList(1);
                mediaList.Add(movie);
                var mediaListSerialized = JsonConvert.SerializeObject(mediaList, Formatting.None);
                File.WriteAllText(moviesPath, mediaListSerialized);
            }else if(strPath.Equals("show"))
            {
                Show show = new Show();
                show.ID = Convert.ToInt32(media[0]);
                show.Title = media[1];
                show.season = Convert.ToInt32(media[2]);
                show.episode = Convert.ToInt32(media[3]);
                show.writers = media[4].Split("|");
                try
                {
                    show.genres = media[5].Split("|");
                }catch(ArgumentNullException)
                {
                    show.genres = new string[] {"Unknown"};
                }
                //Convert show to JSON HERE
                mediaList = getMediaList(2);
                mediaList.Add(show);
                var mediaListSerialized = JsonConvert.SerializeObject(mediaList, Formatting.None);
                File.WriteAllText(showsPath, mediaListSerialized);
            }else if(strPath.Equals("video"))
            {
                Video video = new Video();
                video.ID = Convert.ToInt32(media[0]);
                video.Title = media[1];
                video.format = media[2];
                video.length = Convert.ToInt32(media[3]);
                video.regions = Array.ConvertAll(media[4].Split("|"), s => int.Parse(s));
                try
                {
                    video.genres = media[5].Split("|");
                }catch(ArgumentNullException)
                {
                    video.genres = new string[] {"Unknown"};
                }
                //Convert video to JSON HERE
                mediaList = getMediaList(3);
                mediaList.Add(video);
                var mediaListSerialized = JsonConvert.SerializeObject(mediaList, Formatting.None);
                File.WriteAllText(videosPath, mediaListSerialized);
            }
            Console.WriteLine("JSON Repository - Media added Successfully!");
        }
    }
}