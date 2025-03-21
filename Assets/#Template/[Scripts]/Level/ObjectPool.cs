using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace DancingLineFanmade.Level
{
    public class ObjectPool<T> where T : Object
    {
        private List<T> pool = new List<T>();
        private int size = 0;

        public T this[int index]
        {
            get => pool[index];
            set => pool[index] = value;
        }

        public bool Full
        {
            get => pool.Count >= size;
        }

        public void Add(T t)
        {
            if (pool.Count < size) pool.Add(t);
            else Debug.LogError("Out of bounds. The size of the pool is " + size + " and there are " + pool.Count + " objects in the pool now.");
        }

        public int Size
        {
            get => size;
            set => size = value;
        }

        public int Count
        {
            get => pool.Count;
        }

        public void DestoryAll()
        {
            foreach(T t in pool) Object.Destroy(t.GameObject());
            pool.Clear();
        }

        public T First()
        {
            return pool.First();
        }

        public void MoveToLast(T t)
        {
            pool.Remove(t);
            pool.Add(t);
        }
    }
}