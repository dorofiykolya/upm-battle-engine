﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Battle.Engine.Utils
{
    public class PriorityQueueComparable<T> : IEnumerable<T> where T : IComparable
    {
        private readonly List<T> _data;

        public PriorityQueueComparable(int capacity = 0)
        {
            _data = new List<T>(capacity);
        }

        public int Count => _data.Count;

        public List<T>.Enumerator GetEnumerator() => _data.GetEnumerator();

        // ReSharper disable once HeapView.BoxingAllocation
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _data.GetEnumerator();

        // ReSharper disable once HeapView.BoxingAllocation
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Enqueue(T item)
        {
            _data.Add(item);
            var ci = _data.Count - 1;
            while (ci > 0)
            {
                var pi = (ci - 1) / 2;
                if (_data[ci].CompareTo(_data[pi]) >= 0) break;

                var tmp = _data[ci];
                _data[ci] = _data[pi];
                _data[pi] = tmp;
                ci = pi;
            }
        }

        public T Dequeue()
        {
            var li = _data.Count - 1;
            var frontItem = _data[0];
            _data[0] = _data[li];
            _data.RemoveAt(li);

            --li;
            var pi = 0;
            while (true)
            {
                var ci = pi * 2 + 1;
                if (ci > li) break;

                var rc = ci + 1;
                if (rc <= li && _data[rc].CompareTo(_data[ci]) < 0)
                    ci = rc;
                if (_data[pi].CompareTo(_data[ci]) <= 0) break;

                var tmp = _data[pi];
                _data[pi] = _data[ci];
                _data[ci] = tmp;
                pi = ci;
            }

            return frontItem;
        }

        public T Peek()
        {
            var frontItem = _data[0];
            return frontItem;
        }

        public void Clear()
        {
            _data.Clear();
        }
    }
}