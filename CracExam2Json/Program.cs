// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;

namespace CracExam2Json;

internal class Program
{
    public static void Main(string[] args)
    {
        var path = args[0];

        var rawFile = File.ReadAllLines(path);

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
        File.WriteAllText("questions.json", json);
    }
}