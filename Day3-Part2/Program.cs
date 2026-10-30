using System;

namespace Day3_Part2
{
    // ==========================================
    // 1. تجربة الـ Partial Class
    // (قسمنا كلاس Car على جزأين لترتيب الكود)
    // ==========================================
    public partial class Car
    {
        public string Model { get; set; }
        public int Year { get; set; }

        public void Move()
        {
            Console.WriteLine("Car is moving...");
        }
    }

    // الجزء الثاني من نفس الكلاس Car في نفس الـ Namespace
    public partial class Car
    {
        public void Stop()
        {
            Console.WriteLine("Car has stopped.");
        }
    }

    // ==========================================
    // 2. كلاس Human لتجربة دوال Object (ToString, Equals, Clone)
    // ==========================================
    public class Human
    {
        string Name { get; set; }
        public int Age { get; set; }

        public Human(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // أ) Overriding لدالة ToString لتعطي وصفاً مفيداً عن الكائن عند طباعته
        public override string ToString()
        {
            return $"Human Name: {Name}, Age: {Age}";
        }

        // ب) Overriding لدالة Equals لمقارنة البيانات (Data vs Data) بدلاً من العناوين
        public override bool Equals(object obj)
        {
            // استخدام الفحص الحديث (is) لتجنب أخطاء الكاستنج
            if (obj is Human otherHuman)
            {
                return this.Age == otherHuman.Age && this.Name == otherHuman.Name;
            }
            return false;
        }

        // ج) دالة مخصصة للنسخ (تستخدم MemberwiseClone المحمية بداخل الكلاس)
        public Human MakeShallowCopy()
        {
            return (Human)this.MemberwiseClone();
        }

        // يجب دائماً مصاحبة Equals بـ GetHashCode (قاعدة برمجية)
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age);
        }
    }

    // ==========================================
    // 3. البرنامج الرئيسي (Main Program)
    // ==========================================
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("         1. تجربة الـ Partial Class           ");
            Console.WriteLine("==============================================");

            Car myCar = new Car { Model = "BMW", Year = 2025 };
            myCar.Move(); // دالة من الملف الأول
            myCar.Stop(); // دالة من الملف الثاني

            Console.WriteLine("\n==============================================");
            Console.WriteLine("       2. تجربة الـ Override لدالة ToString     ");
            Console.WriteLine("==============================================");

            Human h1 = new Human("أحمد", 22);
            // عند طباعة h1 مباشرة، ستستدعي دالة ToString المعدلة تلقائياً
            Console.WriteLine(h1);

            Console.WriteLine("\n==============================================");
            Console.WriteLine("       3. تجربة الـ Override لدالة Equals       ");
            Console.WriteLine("==============================================");

            Human h2 = new Human("أحمد", 22);
            Human h3 = new Human("محمد", 30);

            // يقارن البيانات ببعضها لأننا عملنا Override لـ Equals
            Console.WriteLine($"هل h1 يساوى h2 (نفس البيانات)؟ {h1.Equals(h2)}"); // True
            Console.WriteLine($"هل h1 يساوى h3 (بيانات مختلفة)؟ {h1.Equals(h3)}"); // False

            Console.WriteLine("\n==============================================");
            Console.WriteLine("       4. تجربة الـ MemberwiseClone (نسخ)     ");
            Console.WriteLine("==============================================");

            Human original = new Human("علي", 25);
            Human cloned = original.MakeShallowCopy();

            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Cloned: {cloned}");
            Console.WriteLine($"هل هما نفس الكائن في الذاكرة (Reference)؟ {ReferenceEquals(original, cloned)}"); // False (نسخة جديدة)

            Console.WriteLine("\nاضغط أي مفتاح للإنهاء...");
            Console.ReadKey();
        }
    }
}