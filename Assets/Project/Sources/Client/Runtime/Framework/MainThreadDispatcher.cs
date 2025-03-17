using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Framework
{
    public sealed class MainThreadDispatcher : MonoBehaviour
    {
        private static readonly Queue<Action> _executionQueue = new();

        public static void Enqueue(Action action)
        {
            lock (_executionQueue)
            {
                _executionQueue.Enqueue(action);
            }
        }

        void Update()
        {
            while (_executionQueue.Count > 0)
            {
                Action action;
                lock (_executionQueue)
                {
                    action = _executionQueue.Dequeue();
                }
                action?.Invoke();
            }
        }
    }
}