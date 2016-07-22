﻿using System;

namespace _Project_Geass.Logging
{
    public class Logger
    {
        private readonly string _baseName;

        public Logger(string _base)
        {
            _baseName = _base;
        }

        public void WriteLog(string text)
        {
            Console.WriteLine($"{_baseName}:{text}");
        }

        public void WriteLog(string text, ConsoleColor c)
        {
            var holder = Console.ForegroundColor;
            Console.ForegroundColor = c;
            Console.WriteLine($"{_baseName}:{text}");
            Console.ForegroundColor = holder;
        }
    }
}