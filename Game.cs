using System.IO;
using System;
using System.Collections.Generic;

public class Game{

    public static void playGame(string mode){

        if (mode.equals("multiplayer")){
            Console.WriteLine("BRUH");
        }
        String filePath = mode + ".txt";

        List<string> wordList = null;

        if (File.Exists(filePath))
        {
            wordList = new List<string>(File.ReadAllLines(filePath));
        }
        else
        {
            Console.WriteLine("File does not exists");
            return;
        }
    

        Random rand = new Random();

        string word = wordList[rand.Next(wordList.Count)];

        char[] guessed = new string('_', word.Length).ToCharArray();
        HashSet<char> triedLetters = new HashSet<char>();
        
        int lives = 6;

        string[] hangmanStages = new string[]
        {
@"
  +---+
  |   |
      |
      |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
      |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
========="
        };

        Console.WriteLine("\nNew Game Started!\n");

        while (lives > 0 && new string(guessed) != word)
        {
            Console.WriteLine(hangmanStages[6-lives]);
            Console.WriteLine($"Word: {new string(guessed)}");
            Console.WriteLine($"Lives remaining: {lives}");
            Console.WriteLine("Enter your guess (one letter):");

            string input = Console.ReadLine() ?? "";
            if (input.Length != 1 || !char.IsLetter(input[0]))
            {
                Console.WriteLine("Please enter a single letter.\n");
                continue;
            }

            char guess = char.ToLower(input[0]);

            if (triedLetters.Contains(guess))
            {
                Console.WriteLine("You already tried that letter!\n");
                continue;
            }

            triedLetters.Add(guess);

            if (word.Contains(guess))
            {
                Console.WriteLine("Good guess!\n");
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == guess)
                    {
                        guessed[i] = guess;
                    }
                }
            }
            else
            {
                Console.WriteLine("Wrong guess!\n");
                lives--;
            }
        }

        Console.WriteLine(hangmanStages[6-lives]);
        if (new string(guessed) == word)
        {
            Console.WriteLine($"Congratulations! You guessed the word: {word}\n");
        }
        else
        {
            Console.WriteLine($"You lost! The word was: {word}\n");
        }
    }

}