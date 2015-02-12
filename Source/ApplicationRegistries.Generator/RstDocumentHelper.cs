using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationRegistries.Generator
{
    static class RstDocumentHelper
    {
        public static string GetSingleLine(string baseText)
        {
            int count = 0;
            var arr = baseText.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (System.Text.Encoding.UTF8.GetByteCount(arr, i, 1) != 1)
                {
                    count++;
                }
                count++;
            }
            
            return "".PadLeft(count, '-');
        }
    }
}
