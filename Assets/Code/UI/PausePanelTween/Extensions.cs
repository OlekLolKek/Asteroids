using System;
using System.Collections.Generic;


namespace UI.PausePanelTween
{
    public static class Extensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T thingy in source)
            {
                action(thingy);
            }

            return source;
        }
    }
}