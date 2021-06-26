using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public interface ITest
    {
        public string Name
        {
            get;
        }
        public Task Execute();
        public bool WasSuccessful { get; set; }
        public IList<Exception> Errors { get; set; }
    }
}