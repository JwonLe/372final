using System.IO;
using System;
using System.Collections.Generic;


public class Game{


    private static bool hasNonLetter(string word){
        foreach (char c in word){
            if (!char.IsLetter(c)){
                return true;
            }
        }

        return false;
    }
    private static String chooseWord(){

            Console.WriteLine("\nEnter a word for the other player to guess!\n");
            string word;
            bool reattempt = false;     // used to print error message if incompatible word entered


            do {
                
                if (reattempt){
                    Console.WriteLine("You must only use letters!!!\n");
                }

                word = Console.ReadLine().Trim() ?? "";

                reattempt = true;
            } while (hasNonLetter(word));


            return word;
    }
    public static void playGame(string mode){

        string word;
        if (mode == "multiplayer"){
            word = chooseWord();
        }
        else{
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

            word = wordList[rand.Next(wordList.Count)].Trim();
        }


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

            if (word.ToLower().Contains(guess))
            {
                Console.WriteLine("Good guess!\n");
                for (int i = 0; i < word.Length; i++)
                {
                    if (word.ToLower()[i] == guess)
                    {
                        guessed[i] = word[i];
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