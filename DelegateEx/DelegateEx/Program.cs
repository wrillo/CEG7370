using System;
namespace DelegateEx
{
    delegate string ReportClassData();
    class TestDelegate
    {
        int a, b;
        TestDelegate() // constructor
        { a = 5; b = 7; }
        static void Main(string[] args)
        {
            TestDelegate tg1 = new TestDelegate();
            ReportClassData rpcd = new ReportClassData(tg1.ToString);
            // now rpcd is a delegate object pointing to ToString
            Console.WriteLine(rpcd()); // call rpcd which points to ToString
        }
        override public string ToString()
        {
            return ("a: " + a + " b: " + b);
        }
    }
}