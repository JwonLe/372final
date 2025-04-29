/*
File: hangman.cs
Purpose: Incldues main
*/

using System;
using System.Collections.Generic;

public class Hangman{
    
    static void Main(string[] args) {

        Console.WriteLine("*************HANG MAN*************\n");
            Console.WriteLine("\nENTER AN OPTION\n");
            while(true){
                Console.WriteLine("\nIF YOU WANT TO START A GAME: START\n");
                Console.WriteLine("IF YOU WANT TO EXIT: EXIT\n");
                string input = Console.ReadLine() ?? "";

            
                switch(input.ToLower()){
                    case "start":
                        //start game;
                        bool keepPlaying = true;
                        while (keepPlaying){

                            string mode = askMode();

                            Game.playGame(mode.ToLower());


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

                    default:
                        Console.WriteLine("Wrong Option. Enter again\n");
                        break;
                }
                
        }
    }

    private static String askMode(){
        while (true){
            Console.WriteLine("\nChoose Mode:\n");
            Console.WriteLine("Enter Easy / Intermediate / Hard / Multiplayer\n");
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
