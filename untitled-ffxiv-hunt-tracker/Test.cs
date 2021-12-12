using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace untitled_ffxiv_hunt_tracker
{
    class Test
    {
        public Test()
        {
            
        }

        public void ListenerMethod(object sender, string result)
        {
            Console.WriteLine($"Hello! I'm listening to you!. - {result}");
        }
    }
}
