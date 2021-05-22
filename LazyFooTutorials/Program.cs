using System;
using System.Collections.Generic;

namespace LazyFooTutorials
{
    internal static class Program
    {
        private static readonly Dictionary<string, Action> LessonsMap =
            new()
            {
                {"L01", RunLessonOne},
                {"L02", RunLessonTwo}
            };
        
        private static void Main(string[] args)
        {
            WhatToRun(args[0]).Invoke();
        }

        private static Action WhatToRun(string key)
        {
            return LessonsMap.ContainsKey(key) ? LessonsMap[key] : delegate {  };
        }

        private static void RunLessonOne()
        {
            L01_Hello_SDL.Show();
        }

        private static void RunLessonTwo()
        {
            var lesson02 = new L02_Hello_Image();
            
            lesson02.Show();
        }
    }
}