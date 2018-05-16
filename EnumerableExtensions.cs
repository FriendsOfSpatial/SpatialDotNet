﻿using System;
using System.Collections.Generic;

namespace SpatialDotNet
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> func)
        {
            foreach (var item in items)
                func(item);
        }
    }
}