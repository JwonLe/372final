/*
File: hangman.cs
Purpose: Incldues main
*/

using System;

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
                        while (true){
                            Console.WriteLine("Do you want to play one more game?\n");
                            Console.WriteLine("Enter y/n\n");
                            string newGame = Console.ReadLine() ?? "";

                            switch(newGame.ToLower()){
                                case "y":
                                    break;

                                case "n":
                                    Console.WriteLine("Good bye!\n");
                                    return;
                                
                                default:
                                    Console.WriteLine("Wrong Option. Enter again\n");
                                    break;

                            }
                        }

                    case "exit":
                        Console.WriteLine("Good bye!\n");
                        return;

                    default:
                        Console.WriteLine("Wrong Option. Enter again\n");
                        break;
                }
                
        }
    }
}
