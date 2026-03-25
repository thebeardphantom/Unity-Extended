using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public struct AwaitableTaskList : IDisposable
    {
        private List<Awaitable> _tasks;

        private bool _used;

        private AwaitableTaskList(List<Awaitable> tasks)
        {
            _tasks = tasks;
            _used = false;
        }

        public static AwaitableTaskList Get(out AwaitableTaskList list)
        {
            List<Awaitable> tasks = ListPool<Awaitable>.Get();
            list = new AwaitableTaskList(tasks);
            return list;
        }

        public void Dispose()
        {
            ListPool<Awaitable>.Release(_tasks);
            _tasks = null;
        }

        public void Add(Awaitable awaitable)
        {
            ThrowIfUnusable();
            _tasks.Add(awaitable);
        }

        public async Awaitable WhenAll()
        {
            ThrowIfUnusable();
            await AwaitableUtility.WhenAll(_tasks);
            MarkUsed();
        }

        private void ThrowIfUnusable()
        {
            if (_used)
            {
                throw new InvalidOperationException("This list is already used.");
            }
        }

        private void MarkUsed()
        {
            _used = true;
            Clear();
        }

        private void Clear()
        {
            _tasks.Clear();
        }
    }
}