using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;



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
        List<char> wrongGuess = new List<char>();

        string word;
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
                Console.WriteLine("File does not exists\n");
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

        Console.WriteLine("====================================\n");
        Console.WriteLine("\nNew Game Started!\n");

        while (lives > 0 && new string(guessed) != word)
        {   Console.WriteLine("----------------------------------");
            Console.WriteLine(hangmanStages[6-lives]);
            Console.WriteLine($"Word: {new string(guessed)}");
            Console.WriteLine($"Lives remaining: {lives}");
            Console.WriteLine("Enter your guess (one letter):");

            string input = Console.ReadLine() ?? "";
            if (input.Length != 1 || !char.IsLetter(input[0]))
            {
                Console.WriteLine("\nPlease enter a single letter.\n");
                continue;
            }

            char guess = char.ToLower(input[0]);

            if (triedLetters.Contains(guess))
            {
                Console.WriteLine("\nYou already tried that letter!\n");
                wrongGuess.Add(guess);
                continue;
            }

            triedLetters.Add(guess);

            if (word.ToLower().Contains(guess))
            {
                Console.WriteLine("\nGood guess!\n");
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
                Console.WriteLine("\nWrong guess!\n");
                lives--;
                wrongGuess.Add(guess);
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

        printResult(wrongGuess);
        
    }

    private static void printResult(List<char> wrongGuess){
        Console.WriteLine("====================================\n");
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

    public static void suggestWords(){
        Console.WriteLine("******* WORD SUGGESTION *******\n");
        
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