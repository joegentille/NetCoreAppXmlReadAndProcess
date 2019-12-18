using System;

namespace ReadXML
{
    class Program
    {
        static void Main(string[] args)
        {
            ReaderFile reader = new ReaderFile();
            reader.GetXmlFileDataWithXmlLoad();
            //reader.GetXmlFileDataWithLinq();
            //reader.GetXmlFileDataWithSerialization();
            Console.WriteLine("Hello World!");
        }
    }
}
