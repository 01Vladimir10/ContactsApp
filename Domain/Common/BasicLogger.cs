using System;
using System.Globalization;
using System.IO;

namespace Domain.Common
{
    public class BasicLogger<T> : ILogger<T>
    {
        private readonly TextWriter _writer;
        private static string ClassName => typeof(T).Name;
        
        public BasicLogger(TextWriter output)
        {
            _writer = output;
        }
        public BasicLogger()
        {
            _writer = Console.Out;
        }

        public void I(string message)
        {
            Log("INFO", message);        
        }

        public void E(string message)
        {
            Log("ERROR", message);
        }

        public void W(string message)
        {
            Log("WARNING", message);
        }

        private void Log(string tag, string message)
        {
            var output = $"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} | {tag} | {ClassName} | {message}";
            _writer.WriteLine(output);
        }
    }
}