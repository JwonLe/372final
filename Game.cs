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

    private static int GetHint(string word, char[] guessed, HashSet<char> triedLetters)
    {
        // Create a random number generator to pick a random hint letter
        Random rand = new Random();

        // Create a list to hold the indexes of the unguessed letters
        List<int> unguessedIndexes = new List<int>();

        // Loop through each character in the word
        for (int i = 0; i < word.Length; i++)
        {
            // If the letter at the index is still a '_', it means it hasn't been guessed yet
            if (guessed[i] == '_')
            {
                unguessedIndexes.Add(i); // Add the index of this unguessed letter to the list
            }
        }

        if (unguessedIndexes.Count == 0) return -1; // No hint available if all letters are guessed

        // Pick a random index from unguessed letters
        int randomIndex = unguessedIndexes[rand.Next(unguessedIndexes.Count)];
        return randomIndex; // Return the index of the unguessed letter
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
            Console.WriteLine("Enter your guess (one letter) or type 'hint' to get a hint:");

            string input = Console.ReadLine() ?? "";

            if (input.ToLower() == "hint") {
                int hintIndex = GetHint(word, guessed, triedLetters);

                if (hintIndex == -1)
                {
                    Console.WriteLine("No hints left. You've guessed all the letters!");
                }
                else
                {
                    Console.WriteLine($"Here's a hint: The letter at position {hintIndex + 1} is '{word[hintIndex]}'.");
                }
                continue;
            }

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