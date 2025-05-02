using System;
using System.IO;

public class Test
{
    public static void RunAllTests()
    {
        string testDir = "Tests";
        if (!Directory.Exists(testDir))
        {
            Console.WriteLine($"❌ Test directory '{testDir}' not found.");
            return;
        }

        string[] testFiles = Directory.GetFiles(testDir, "*.txt");

        if (testFiles.Length == 0)
        {
            Console.WriteLine("⚠️ No test files found in 'Tests' directory.");
            return;
        }

        foreach (string testFile in testFiles)
        {
            Console.WriteLine($"\n==============================");
            Console.WriteLine($"🔍 Running test: {Path.GetFileName(testFile)}");

            try
            {
                using (StreamReader sr = new StreamReader(testFile))
                {
                    Console.SetIn(sr);

                    // 게임 로직 실행 — 반드시 Hangman에 Run() 함수 정의되어 있어야 함
                    Hangman.run();
                }

                Console.WriteLine($"✅ {Path.GetFileName(testFile)} test finished.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to run test {Path.GetFileName(testFile)}");
                Console.WriteLine($"   Reason: {ex.Message}\n");
                continue;
            }
        }

        Console.WriteLine("🧪 All tests completed.");
    }
}
