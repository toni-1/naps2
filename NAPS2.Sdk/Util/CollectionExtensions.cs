﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NAPS2.Util
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Appends the given item to the end of the enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T item)
        {
            foreach (var obj in enumerable)
            {
                yield return obj;
            }
            yield return item;
        }

        /// <summary>
        /// Prepends the given item to the start of the enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, T item)
        {
            yield return item;
            foreach (var obj in enumerable)
            {
                yield return obj;
            }
        }

        /// <summary>
        /// Removes multiple elements from the list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="elements"></param>
        public static void RemoveAll<T>(this IList<T> list, IEnumerable<T> elements)
        {
            list.RemoveAllAt(list.IndiciesOf(elements));
        }

        /// <summary>
        /// Removes multiple elements from the list at the specified indices.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="indices"></param>
        public static void RemoveAllAt<T>(this IList<T> list, IEnumerable<int> indices)
        {
            int offset = 0;
            foreach (int i in indices.OrderBy(x => x))
            {
                list.RemoveAt(i - offset++);
            }
        }

        /// <summary>
        /// Gets an enumerable of elements at the specified indices.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="indices"></param>
        /// <returns></returns>
        public static IEnumerable<T> ElementsAt<T>(this IList<T> list, IEnumerable<int> indices)
        {
            return indices.Select(i => list[i]);
        }

        /// <summary>
        /// Gets an enumerable of indices of the specified elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static IEnumerable<int> IndiciesOf<T>(this IList<T> list, IEnumerable<T> elements)
        {
            int i = 0;
            var elementDict = list.ToDictionary(element => element, element => i++);
            try
            {
                return elements.Select(x => elementDict[x]).ToList();
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Not all elements present in list", nameof(elements));
            }
        }

        /// <summary>
        /// Adds a key-value pair to the multi-dictionary.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddMulti<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = new HashSet<TValue>();
            }
            dict[key].Add(value);
        }

        /// <summary>
        /// Adds an enumeration of key-value pairs to the multi-dictionary.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public static void AddMulti<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dict, TKey key, IEnumerable<TValue> values)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = new HashSet<TValue>();
            }
            foreach (var value in values)
            {
                dict[key].Add(value);
            }
        }

        /// <summary>
        /// Gets the element for the given key, or default(TKey) if none is present.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return default;
        }

        /// <summary>
        /// Gets the element for the given key, or the provided value if none is present.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets the element for the given key, or the provided value if none is present.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> defaultValue)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return defaultValue();
        }

        /// <summary>
        /// Gets the element for the given key, or sets and returns the provided value if none is present.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = defaultValue;
            }
            return dict[key];
        }

        /// <summary>
        /// Gets the element for the given key, or sets and returns the provided value if none is present.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> defaultValue)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = defaultValue();
            }
            return dict[key];
        }
    }
}
