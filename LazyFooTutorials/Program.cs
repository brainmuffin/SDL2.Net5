using System;
using System.Collections.Generic;

namespace LazyFooTutorials
{
    internal static class Program
    {
        private static readonly Dictionary<string, Action> LessonsMap =
            new() {{"L01", RunLessonOne}};
        
        private static void Main(string[] args)
        {
            var actionToRun = WhatToRun(args[0]);

            actionToRun.Invoke();
        }

        private static Action WhatToRun(string key)
        {
            if (LessonsMap.ContainsKey(key))
                return LessonsMap[key];

            return NoOp;
        }

        private static void NoOp()
        {
        }

        private static void RunLessonOne()
        {
            L01_Hello_SDL.Show();
        }
    }
}