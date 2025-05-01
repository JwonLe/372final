/*
 * File: Hangman.cs
 * Description: Entry point for the Hangman game.
 *              Provides the main game menu and handles user interaction flow.
 *              Supports starting a game, suggesting new words, and exiting.
 * 
 * Author: Daniel Reynaldo, Juwon Lee, Mustafa Alnidawi, Noah Belara Reyes
 * Course: CSc 372 - Final Project, Spring 2025
 */


using System;
using System.Collections.Generic;

public class Hangman{
    
    static void Main(string[] args) {

        Console.WriteLine("*************HANG MAN*************\n");
            while(true){
                
                int score = 0;
                // Read playerScore file to get saved score
                String filePath = "playerScore.txt";
                if (File.Exists(filePath))
                {
                    String scoreStr = File.ReadLines(filePath).First();
                    Console.WriteLine("Your Score: " + scoreStr);
                    

                    if (int.TryParse(scoreStr, out int number)) {
                        score = number;
                    } 
                    else {
                        Console.WriteLine("Error: Couldn't read number\n");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Error: playerScore.txt does not exists");
                }


                Console.WriteLine("************************************");
                Console.WriteLine("ENTER AN OPTION\n");
                Console.WriteLine("IF YOU WANT TO START A GAME: START\n");
                Console.WriteLine("IF YOU WANT TO SUGGEST WORDS: SUGGEST\n");
                Console.WriteLine("IF YOU WANT TO EXIT: EXIT");
                Console.WriteLine("************************************");
                string input = Console.ReadLine() ?? "";

            
                switch(input.ToLower()){
                    case "start":
                        //start game;
                        bool keepPlaying = true;
                        while (keepPlaying){

                            string mode = askMode();

                            score = Game.playGame(mode.ToLower(), score);


                            Console.WriteLine("\nDo you want to play one more game?\n");
                            Console.WriteLine("Enter y/n\n");
                            string newGame = Console.ReadLine() ?? "";

                            switch(newGame.ToLower()){
                                case "y":
                                    break;

                                case "n":
                                    Console.WriteLine("\nGood bye!\n");
                                    keepPlaying = false;
                                    return;
                                
                                default:
                                    Console.WriteLine("\nWrong Option. Enter again\n");
                                    break;

                            }
                        }
                        break;
                    case "exit":
                        Console.WriteLine("\nGood bye!\n");
                        return;

                    case "suggest":
                        Game.suggestWords();
                        break;

                    default:
                        Console.WriteLine("- Wrong Option. Enter again\n");
                        break;
                }
                
        }
    }

    /*
     * Funtion: askMode
     * Purpose: asks which mode the user is going to play before starting a game.
     * Input:   None
     * Output:  String  -   a string indicatring mode
     */
    private static String askMode(){
        while (true){
            Console.WriteLine("\n*************Choose Mode*************");
            Console.WriteLine("Enter: Easy / Intermediate / Hard / Multiplayer");
            string mode = Console.ReadLine() ?? "";
            switch (mode.ToLower()){
                case "easy":
                    return "easy";
                
                case "intermediate":
                    return "intermediate";
                
                case "hard":
                    return "hard";
                    
                case "multiplayer":
                    return "multiplayer";

                default:
                    Console.WriteLine("Wrong Option. Enter again\n");
                        break;
            }
        }
    }

}
