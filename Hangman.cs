/*
File: hangman.cs
Purpose: Incldues main
*/

using System;
using System.Collections.Generic;

public class Hangman{
    
    static void Main(string[] args) {
        Console.WriteLine("*************HANG MAN*************\n");
            Console.WriteLine("ENTER AN OPTION\n");
            while(true){
                Console.WriteLine("IF YOU WANT TO START A GAME: START\n");
                Console.WriteLine("IF YOU WANT TO EXIT: EXIT\n");
                string input = Console.ReadLine() ?? "";

            
                switch(input.ToLower()){
                    case "start":
                        //start game;
                        bool keepPlaying = true;
                        while (keepPlaying){

                            PlayGame();

                            Console.WriteLine("Do you want to play one more game?\n");
                            Console.WriteLine("Enter y/n\n");
                            string newGame = Console.ReadLine() ?? "";

                            switch(newGame.ToLower()){
                                case "y":
                                    break;

                                case "n":
                                    Console.WriteLine("Good bye!\n");
                                    keepPlaying = false;
                                    return;
                                
                                default:
                                    Console.WriteLine("Wrong Option. Enter again\n");
                                    break;

                            }
                        }
                        break;
                    case "exit":
                        Console.WriteLine("Good bye!\n");
                        return;

                    default:
                        Console.WriteLine("Wrong Option. Enter again\n");
                        break;
                }
                
        }
    }

    static void PlayGame(){
        string[] wordList = {"computer", "soccer", "pancake", "challenge", "butterfly"};
        Random rand = new Random();

        string word = wordList[rand.Next(wordList.Length)];
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
