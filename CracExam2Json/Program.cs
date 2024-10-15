// See https://aka.ms/new-console-template for more information

using System.Text;
using Newtonsoft.Json;

namespace CracExam2Json;

internal class Program
{
    public static void Main(string?[] args)
    {
        switch (args.Length)
        {
            case 0:
                Console.WriteLine("Usage: CracExam2Json <path> [output]");
                return;
            case 1:
                Console.WriteLine("Output file not specified, using default name: questions.json");
                args = new[] {args[0], "questions.json"};
                break;
        }

        var is2312 = args.Any(x => x is "--gb2312" or "-g");

        var path = args[0] ?? "questions.txt";

        var encoding = GB2312Encoding.Instance;
        var rawFile = File.ReadAllLines(path, is2312 ? encoding : Encoding.UTF8);

        var currentQuestionId = "";
        var questions = new List<Question>();

        foreach (var line in rawFile)
        {
            if (line.StartsWith("[I]"))
            {
                currentQuestionId = line[3..];
                Console.WriteLine($"\"{currentQuestionId}\": {{");
                // 创建新问题
                if (questions.All(q => q.Id != currentQuestionId))
                {
                    questions.Add(new Question
                    {
                        Id = currentQuestionId,
                        Choices = new List<string>()
                    });
                }
            }
            else if (line.StartsWith("[Q]"))
            {
                // 添加问题文本
                var question = line[3..];
                Console.WriteLine($"\"{currentQuestionId}\": \"{question}\",");
                questions.First(q => q.Id == currentQuestionId).Text = question;
            }
            else if (line.StartsWith("[A]"))
            {
                // 添加答案
                var answer = line[3..];
                Console.WriteLine($"\"{currentQuestionId}A\": \"{answer}\",");
                questions.First(q => q.Id == currentQuestionId).Choices.Insert(0, answer);
            }
            else if (line.StartsWith("[B]"))
            {
                // 添加选项
                var choice = line[3..];
                Console.WriteLine($"\"{currentQuestionId}B\": \"{choice}\",");
                questions.First(q => q.Id == currentQuestionId).Choices.Add(choice);
        
            }
            else if (line.StartsWith("[C]"))
            {
                // 添加选项
                var choice = line[3..];
                Console.WriteLine($"\"{currentQuestionId}C\": \"{choice}\",");
                questions.First(q => q.Id == currentQuestionId).Choices.Add(choice);
            }
            else if (line.StartsWith("[D]"))
            {
                // 添加选项
                var choice = line[3..];
                Console.WriteLine($"\"{currentQuestionId}D\": \"{choice}\",");
                questions.First(q => q.Id == currentQuestionId).Choices.Add(choice);
            }
            else if (line.StartsWith("[P]"))
            {
                currentQuestionId = "";
                Console.WriteLine("====================");
            }
        }

        var json = JsonConvert.SerializeObject(questions);
        File.WriteAllText(args[1] ?? "questions.json", json);
    }
}