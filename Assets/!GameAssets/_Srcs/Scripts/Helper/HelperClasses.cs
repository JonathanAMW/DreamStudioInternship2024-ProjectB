//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/07
//----------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;

namespace UtilityCollections
{
    /// <summary>
    /// Class that contain static methods to compare between 2 lists
    /// </summary>
    public static class ListComparer
    {
        public static bool IsEqualWithSameOrder<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            return list1.SequenceEqual(list2);
        }

        public static bool IsEqualWithoutSameOrder<T>(List<T> list1, List<T> list2)
        {
            // Create dictionaries to store element counts
            Dictionary<T, int> count1 = new Dictionary<T, int>();
            Dictionary<T, int> count2 = new Dictionary<T, int>();

            // Count occurrences of elements in list1
            CountElements(list1, count1);

            // Count occurrences of elements in list2
            CountElements(list2, count2);

            // Compare counts of elements in both dictionaries
            return AreDictionariesEqual(count1, count2);
        }

        private static void CountElements<T>(List<T> list, Dictionary<T, int> count)
        {
            foreach (T item in list)
            {
                if (!count.ContainsKey(item))
                    count[item] = 0;
                count[item]++;
            }
        }

        private static bool AreDictionariesEqual<T>(Dictionary<T, int> dict1, Dictionary<T, int> dict2)
        {
            if (dict1.Count != dict2.Count)
                return false;

            foreach (KeyValuePair<T, int> kvp in dict1)
            {
                if (!dict2.ContainsKey(kvp.Key) || dict2[kvp.Key] != kvp.Value)
                    return false;
            }

            return true;
        }
    }
}