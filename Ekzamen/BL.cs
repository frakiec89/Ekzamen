using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekzamen
{
    public static class Algoritm
    {
        /// <summary>
        /// получаем список вопросов 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public  static List<Question> GetQuestions (string path)
        {
            string content = string.Empty;

            try
            {
                using (StreamReader sr = new StreamReader(path ,Encoding.Default ))
                {
                    content = sr.ReadToEnd();
                }
            }
            catch 
            {
                throw new Exception("Ошибка чтения файла " + path);
            }

            return GetList(content);

        }

        private static List<Question> GetList(string content)
        {
            string[] vs = content.Split(';');

            List<Question> questions = new List<Question>();

            foreach (var item in vs)
            {
                if (! string.IsNullOrWhiteSpace(item))
                {
                    if (GetQuestion(item) !=  null)
                    questions.Add(GetQuestion(item));
                }
            }

            if (questions.Count > 0) return questions;

            return default;
        }

        private static Question GetQuestion(string item)
        {
           
            string [] vs = item.Split('.');
            try
            {
                var s = new Question(Convert.ToInt32(vs[0]), vs[1]);
                return s;
            }
            catch
            {
                return null;
            }
            
        }

        internal static void SaveVar(string path, int count , int  qCount , List<Question> questions)
        {

            for (int i = 0; i < count; i++)
            {
                CreateVarFile(path, i + 1 ,  qCount ,  questions);
            }
        }

        private static void CreateVarFile(string path, int count, int qCount ,  List<Question> questions)
        {
            string uri = path + @"\\Вариант" + count + ".txt";

            File.WriteAllText(uri, GetRandomContent(questions , qCount , count));


        }

        private static string GetRandomContent(List<Question> questions ,   int qCount , int count)
        {
            List<Question> newQuestions = new List<Question>();

            Random random = new Random(DateTime.Now.Millisecond);

            while(newQuestions.Count<=qCount)
            {
                int index = random.Next(0, questions.Count);

               if (!  newQuestions.Any(x=>x.Id == questions[index].Id))
               {
                    newQuestions.Add(questions[index]);
               }
            }

            string content = "Вариант " +count +"." +"\n" ;

            for (int i = 0; i < newQuestions.Count; i++)
            {
                content += i+1 +" - " +  newQuestions[i].Content.TrimEnd().TrimStart() + "." + "\n";
            }

            return content;
        }
    }

    /// <summary>
    /// Вопрос
    /// </summary>
    public class Question
    {
        public Question(int id, string content)
        {
            Id = id;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public int  Id { get; private set; }
        public  string Content { get; private set; }
    }
}
