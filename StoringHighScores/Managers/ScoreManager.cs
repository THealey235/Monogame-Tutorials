﻿using StoringHighScores.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoringHighScores.Managers
{
    public class ScoreManager
    {
        private static string _filename = "scores.xaml"; //since we dont give a apth this'll go to the bin folder

        public List<Score> Highscores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager() : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Value).ToList();

            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            if (!File.Exists(_filename))
                return new ScoreManager(); //create new instance if it doesn't exist

            using(var reader = new StreamReader(new FileStream(_filename, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serializer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList(); ;
        }

        public static void Save(ScoreManager manager)
        {
            //Overrides the file if it already exists
            using (var writer = new StreamWriter(new FileStream(_filename, FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                serializer.Serialize(writer, manager.Scores);
            }
        }

    }
}
