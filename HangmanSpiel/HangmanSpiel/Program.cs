using System;
using System.Collections.Generic;
using System.IO;

namespace HangmanSpiel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Herzlich Willkommen bei Hangman!");
            bool continuePlaying = true;
            while (continuePlaying)
            {
                List<string> wordList = LoadWordsFromFile("wordlist.txt");

                if(wordList.Count == 0) 
                {
                    Console.WriteLine("Die Wörterliste ist leer. Bitte füge Wörter zur 'wordlist.txt' -Datei hinzu.");
                    break;
                }

                Random randomWord = new Random();
                int randomIndex = randomWord.Next(0, wordList.Count);
                string randomizedWord = wordList[randomIndex].ToLower();


                Console.Write("Wähle die Anzahl der Versuche aus, die du haben möchtest: ");
                string choosedTries = Console.ReadLine();

                if (IsNumber(choosedTries))
                {


                    Console.WriteLine("Versuche, das Wort zu erraten.");

                    char[] currentRound = new char[randomizedWord.Length];
                    for (int i = 0; i < currentRound.Length; i++)
                    {
                        currentRound[i] = '_';
                    }

                    int tries = 0;
                    int maxtries = Convert.ToInt32(choosedTries);

                    while (true)
                    {
                        Console.WriteLine("\nAktueller Stand: " + new string(currentRound));
                        Console.Write("Gib eine Buchstaben ein: ");
                        char guessedLetter = char.ToLower(Console.ReadKey().KeyChar);

                        bool letterFound = false;

                        for (int i = 0; i < currentRound.Length; i++)
                        {
                            if (randomizedWord[i] == guessedLetter)
                            {
                                currentRound[i] = guessedLetter;
                                letterFound = true;
                            }
                        }

                        if (!letterFound)
                        {
                            tries++;
                            Console.WriteLine("\nDer Buchstabe {0} ist nicht im Wort enthalten.", guessedLetter);
                        }

                        if (tries >= maxtries)
                        {
                            Console.WriteLine("\nDu hast zu viele Versuche gebraucht. Das Wort war: " + randomizedWord);
                            break;
                        }

                        if (new string(currentRound) == randomizedWord)
                        {
                            Console.WriteLine("\nHerzlichen Glückwunsch, du ast das Wort erraten: " + randomizedWord);
                            break;
                        }
                    }

                    Console.Write("Möchtest du eine neue Runde spielen? (J/N): ");
                    string playAgain = Console.ReadLine();
                    if (playAgain.ToLower() != "j")
                    {
                        continuePlaying = false;
                    }
                }
                else
                {
                    Console.WriteLine("Die Eingabe entsprach keiner Zahl!");
                }
            }
            Console.ReadKey();
        }

        static List<string> LoadWordsFromFile(string filepath)
        {
            List<string> wordList = new List<string>();
            try
            {
                if (File.Exists(filepath))
                {
                    wordList.AddRange(File.ReadAllLines(filepath));
                }
                else
                {
                    throw new FileNotFoundException(filepath);
                }
                return wordList;
            }
            catch(Exception e) 
            {
                Console.WriteLine("Fehler beim Laden der Wörter: " + e.Message);
                return wordList;
            }
        }

        static bool IsNumber(string eingabe)
        {
            if (int.TryParse(eingabe, out _))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
