/*
 * File: Game.cs
 * Description: Contains the game logic for Hangman, including:
 *              - Word selection (single-player and multiplayer)
 *              - Main gameplay loop with input handling and hangman display
 *              - Hint system
 *              - Time limit mode
 *              - Word suggestion feature with classification (easy/intermediate/hard)
 *              - Post-game stats summary
 * 
 * Author: Daniel Reynaldo, Juwon Lee, Mustafa Alnidawi, Noah Belara Reyes
 * Course: CSc 372 - Final Project, Spring 2025
 */

using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;


public class Game{


    /*
     * Funtion: hasNonLetter
     * Purpose: checks whether the input contains non-alphabet chars
     * Input: string word
     * Output:  if an input contains non-alphabet char, returns true, otherwise false.
     */
    private static bool hasNonLetter(string word){
        foreach (char c in word){
            if (!char.IsLetter(c)){
                return true;
            }
        }

        return false;
    }

    /*
     * Funtion:
     * Purpose:
     * Input:
     * Output:
     */
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

    /*
     * Funtion:
     * Purpose:
     * Input:
     * Output:
     */
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

    /*
     * Funtion: playGame
     * Purpose: main logic of the hangman game.
     * Input: String mode
     * Output: none.
     */
    public static void playGame(string mode){

        List<char> wrongGuess = new List<char>();
        string word;
        bool isTimed = false;
        DateTime startTime = DateTime.MinValue;
        TimeSpan timeLimit = TimeSpan.Zero; 

        if (mode == "multiplayer"){
            word = chooseWord();
        }
        else{
            String filePath = mode + ".txt";

            List<string> wordList;

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

            Console.WriteLine("Do you want you attempt to be timed?");
            Console.WriteLine("Enter y/n");
            string time = Console.ReadLine() ?? "";

            if(time == "y"){
                isTimed = true;
                startTime = DateTime.Now;
                timeLimit = TimeSpan.FromSeconds(30);
            }
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

        Console.WriteLine("\n************************************");
        Console.WriteLine("New Game Started!\n");
        int turn = 1;

        while (lives > 0 && new string(guessed) != word)
        {
            Console.WriteLine("\n====================================");
            Console.WriteLine($"            TURN {turn}");
            Console.WriteLine("====================================\n");
            if(isTimed && DateTime.Now - startTime >= timeLimit){
                Console.WriteLine("Time's up!");
                break;
            }

            Console.WriteLine(hangmanStages[6-lives]);
            Console.WriteLine($"Word: {new string(guessed)}");
            Console.WriteLine($"Lives remaining: {lives}");

            if(isTimed){
                TimeSpan remaining = timeLimit - (DateTime.Now - startTime);
                Console.WriteLine($"Time remaining: {remaining.Seconds} seconds");
            }
            Console.WriteLine("Enter your guess (one letter) or type 'hint' to get a hint:");
            string input = Console.ReadLine() ?? "";

            if (isTimed && DateTime.Now - startTime >= timeLimit)
            {
                Console.WriteLine("Time's up!");
                break;
            }


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
                wrongGuess.Add(guess);
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
                wrongGuess.Add(guess);
            }
            turn ++;

        }

        Console.WriteLine(hangmanStages[6-lives]);
        if (new string(guessed) == word)
        {
            Console.WriteLine($"Congratulations! You guessed the word: {word}\n");
        }
        else if (isTimed && DateTime.Now - startTime >= timeLimit)
        {
            Console.WriteLine("You lost due to time running out!\n");
            Console.WriteLine($"The word was: {word}\n");
        }
        else
        {
            Console.WriteLine($"You lost! The word was: {word}\n");
        }
        printResult(wrongGuess);
    }

    /*
     * Funtion: printResult
     * Purpose: prints out stats of wrong guesses
     * Input: List<char> wrongGuess -   list of wrong guesses that user attempted
              during playing game.
     * Output: non.
     */
    private static void printResult(List<char> wrongGuess){
         Console.WriteLine("====================================");
         var stats = wrongGuess
             .GroupBy(c => c)
             .Select(g => new { Letter = g.Key, Count = g.Count() })
             .OrderByDescending(g => g.Count);
 
         Console.WriteLine("Stats For Wrong Guesses:");
         foreach (var stat in stats)
         {
             Console.WriteLine($"{stat.Letter}: {stat.Count} times wrong");
         }
         Console.WriteLine("====================================\n");
     }

     /*
     * Funtion: suggestWords
     * Purpose: Provides a loop where the player can suggest new words to add.
     *          Validates input and classifies the word based on its length.
     * Input: none
     * Output: none
     */
     public static void suggestWords(){
         Console.WriteLine("\n******* WORD SUGGESTION *******\n");
         
         while (true){
             Console.WriteLine("-------------------------------\n");
             Console.WriteLine("Enter Option:\n");
             Console.WriteLine("If you want to suggest more words: CONTINUE\n");
             Console.WriteLine("If you want to exit: EXIT\n");
 
             string input = Console.ReadLine() ?? "";
             Console.WriteLine("-------------------------------\n");
             switch(input.ToLower()){
                 case "continue":
                     Console.WriteLine("Enter one word at a time without whitespaces\n");
                     string suggest = Console.ReadLine() ?? "";
                     if (hasNonLetter(suggest)){
                         Console.WriteLine("The input contains non-alphabet character.\n");
                         Console.WriteLine("Try again.\n");
                     }else{
                         clasifyWord(suggest.ToLower());
                         Console.WriteLine("The word is added.\n");
                     }
                     break;
 
                 case "exit":
                     Console.WriteLine("Thank you for your suggestions!\n");
                     return;
                 
                 default:
                     Console.WriteLine("Wrong Option. Enter again\n");
                     break;
                 
             }
         }
     }

     /*
     * Funtion: clasifyWord
     * Purpose: appends the given word to a different files based on word length.
     * Input: string word   -   a lower case with no non-alphabet chars
     * Output: none.
     */
     private static void clasifyWord(string word){
         if (word.Length <= 4){
             File.AppendAllText("easy.txt", $"{word}\n");
         }else if (word.Length <= 8){
             File.AppendAllText("intermediate.txt", $"{word}\n");
         }else{
             File.AppendAllText("hard.txt", $"{word}\n");
         }
     }

}