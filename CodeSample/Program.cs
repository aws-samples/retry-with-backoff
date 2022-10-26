using System;
using System.Threading.Tasks;

namespace CodeSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var retryObj = new RetryWithBackoff();
            await retryObj.DoRetriesWithBackOff();
        }

    }
}