/*
 * File: Test.cs
 * Description: class for testing. 
 * 
 * Author: Daniel Reynaldo, Juwon Lee, Mustafa Alnidawi, Noah Belara Reyes
 * Course: CSc 372 - Final Project, Spring 2025
 */
using System;
using System.IO;

public class Test
{
    public static void RunAllTests()
    {
        string testDir = "Tests";
        if (!Directory.Exists(testDir))
        {
            Console.WriteLine($"Test directory '{testDir}' not found.");
            return;
        }

        string[] testFiles = Directory.GetFiles(testDir, "*.txt");

        if (testFiles.Length == 0)
        {
            Console.WriteLine("No test files found in 'Tests' directory.");
            return;
        }

        foreach (string testFile in testFiles)
        {
            Console.WriteLine($"\n==============================");
            Console.WriteLine($"Running test: {Path.GetFileName(testFile)}");

            try
            {
                using (StreamReader sr = new StreamReader(testFile))
                {
                    Console.SetIn(sr);

                    Hangman.run();
                }

                Console.WriteLine($"{Path.GetFileName(testFile)} test finished.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to run test {Path.GetFileName(testFile)}");
                Console.WriteLine($"   Reason: {ex.Message}\n");
                continue;
            }
        }

        Console.WriteLine("All tests completed.");
    }
}
