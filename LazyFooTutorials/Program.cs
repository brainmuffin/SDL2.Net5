using System;
using System.Collections.Generic;

namespace LazyFooTutorials
{
    public interface ILesson
    {
        void Show();
    }

    public class EmptyLesson : ILesson
    {
        public void Show() { }
    }

    internal static class Program
    {
        private static readonly Dictionary<string, Func<ILesson>> SLessonsMap =
            new()
            {
                {"L01", () => new L01_Hello_SDL()},
                {"L02", () => new L02_Hello_Image()},
                {"L03", () => new L03_X_Out()}
            };

        private static void Main(string[] args)
        {
            WhatToRun(args[0]).Show();
        }

        private static ILesson WhatToRun(string key)
        {
            return SLessonsMap.ContainsKey(key)
                ? SLessonsMap[key].Invoke()
                : new EmptyLesson();
        }
    }
}