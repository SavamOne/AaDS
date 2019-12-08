using System;

namespace CourseWork
{
    class Program
    {
        public static Tuple<string, string, int> Parse(string line)
        {
            string[] objects = line.Split(' ');
            if (int.TryParse(objects[2], out int cost))
                return Tuple.Create(objects[0], objects[1], cost);
            else
                throw new Exception("Третий объект был не числом");
        }

        static void Main(string[] args)
        {
            var MST = new KruskalMST();

            while (true)
            {
                var objects = Parse(Console.ReadLine());

                MST.AddEdge(objects.Item1, objects.Item2, objects.Item3);
                MST.FindMST();

                Console.WriteLine("-----");
                int sum = 0;
                foreach (MST.Edge edge in MST)
                {
                    Console.WriteLine(edge);
                    sum += edge.Сost;
                }
                Console.WriteLine(sum);
                Console.WriteLine("-----");
            }
        }
    }
}
