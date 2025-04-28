#if UNITY_6000_0_OR_NEWER
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using Task = System.Threading.Tasks.Task;

namespace BeardPhantom.UnityExtended
{
    public static partial class AwaitableUtility
    {
        public static async Awaitable WhenAll(IEnumerable<Awaitable> awaitables, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using PooledObject<List<Awaitable>> _ = ListPool<Awaitable>.Get(out List<Awaitable> list);
            list.AddRange(awaitables);
            bool complete = AreAllTasksComplete(list);
            while (!complete)
            {
                cancellationToken.ThrowIfCancellationRequested();
                complete = AreAllTasksComplete(list);
                if (!complete)
                {
                    await Task.Yield();
                }
            }

            cancellationToken.ThrowIfCancellationRequested();
            foreach (Awaitable awaitable in list)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await awaitable;
            }
        }

        public static async Awaitable WhenAll(Awaitable awaitable1, Awaitable awaitable2, CancellationToken cancellationToken = default)
        {
            while (!awaitable1.IsCompleted || !awaitable2.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            cancellationToken.ThrowIfCancellationRequested();
            await awaitable1;
            await awaitable2;
        }

        public static async Awaitable WhenAll(
            Awaitable awaitable1,
            Awaitable awaitable2,
            Awaitable awaitable3,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.IsCompleted || !awaitable2.IsCompleted || !awaitable3.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            cancellationToken.ThrowIfCancellationRequested();
            await awaitable1;
            await awaitable2;
            await awaitable3;
        }

        public static async Awaitable WhenAll(
            Awaitable awaitable1,
            Awaitable awaitable2,
            Awaitable awaitable3,
            Awaitable awaitable4,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.IsCompleted || !awaitable2.IsCompleted || !awaitable3.IsCompleted || !awaitable4.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            cancellationToken.ThrowIfCancellationRequested();
            await awaitable1;
            await awaitable2;
            await awaitable3;
            await awaitable4;
        }

        public static async Awaitable WhenAll(
            Awaitable awaitable1,
            Awaitable awaitable2,
            Awaitable awaitable3,
            Awaitable awaitable4,
            Awaitable awaitable5,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.IsCompleted
                   || !awaitable2.IsCompleted
                   || !awaitable3.IsCompleted
                   || !awaitable4.IsCompleted
                   || !awaitable5.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            cancellationToken.ThrowIfCancellationRequested();
            await awaitable1;
            await awaitable2;
            await awaitable3;
            await awaitable4;
            await awaitable5;
        }

        public static async Awaitable WhenAll(
            Awaitable awaitable1,
            Awaitable awaitable2,
            Awaitable awaitable3,
            Awaitable awaitable4,
            Awaitable awaitable5,
            Awaitable awaitable6,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.IsCompleted
                   || !awaitable2.IsCompleted
                   || !awaitable3.IsCompleted
                   || !awaitable4.IsCompleted
                   || !awaitable5.IsCompleted
                   || !awaitable6.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            cancellationToken.ThrowIfCancellationRequested();
            await awaitable1;
            await awaitable2;
            await awaitable3;
            await awaitable4;
            await awaitable5;
            await awaitable6;
        }

        public static async Awaitable WhenAll(
            Awaitable awaitable1,
            Awaitable awaitable2,
            Awaitable awaitable3,
            Awaitable awaitable4,
            Awaitable awaitable5,
            Awaitable awaitable6,
            Awaitable awaitable7,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.IsCompleted
                   || !awaitable2.IsCompleted
                   || !awaitable3.IsCompleted
                   || !awaitable4.IsCompleted
                   || !awaitable5.IsCompleted
                   || !awaitable6.IsCompleted
                   || !awaitable7.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            cancellationToken.ThrowIfCancellationRequested();
            await awaitable1;
            await awaitable2;
            await awaitable3;
            await awaitable4;
            await awaitable5;
            await awaitable6;
            await awaitable7;
        }

        public static async Awaitable WhenAll(
            Awaitable awaitable1,
            Awaitable awaitable2,
            Awaitable awaitable3,
            Awaitable awaitable4,
            Awaitable awaitable5,
            Awaitable awaitable6,
            Awaitable awaitable7,
            Awaitable awaitable8,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.IsCompleted
                   || !awaitable2.IsCompleted
                   || !awaitable3.IsCompleted
                   || !awaitable4.IsCompleted
                   || !awaitable5.IsCompleted
                   || !awaitable6.IsCompleted
                   || !awaitable7.IsCompleted
                   || !awaitable8.IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            cancellationToken.ThrowIfCancellationRequested();
            await awaitable1;
            await awaitable2;
            await awaitable3;
            await awaitable4;
            await awaitable5;
            await awaitable6;
            await awaitable7;
            await awaitable8;
        }

        public static async Awaitable<(T1 Result1, T2 Result2)> WhenAll<T1, T2>(
            Awaitable<T1> awaitable1,
            Awaitable<T2> awaitable2,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.GetAwaiter().IsCompleted || !awaitable2.GetAwaiter().IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            T1 result1 = await awaitable1;
            T2 result2 = await awaitable2;
            return (result1, result2);
        }

        public static async Awaitable<(T1 Result1, T2 Result2, T3 Result3)> WhenAll<T1, T2, T3>(
            Awaitable<T1> awaitable1,
            Awaitable<T2> awaitable2,
            Awaitable<T3> awaitable3,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.GetAwaiter().IsCompleted || !awaitable2.GetAwaiter().IsCompleted || !awaitable3.GetAwaiter().IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            T1 result1 = await awaitable1;
            T2 result2 = await awaitable2;
            T3 result3 = await awaitable3;
            return (result1, result2, result3);
        }

        public static async Awaitable<(T1 Result1, T2 Result2, T3 Result3, T4 Result4)> WhenAll<T1, T2, T3, T4>(
            Awaitable<T1> awaitable1,
            Awaitable<T2> awaitable2,
            Awaitable<T3> awaitable3,
            Awaitable<T4> awaitable4,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.GetAwaiter().IsCompleted
                   || !awaitable2.GetAwaiter().IsCompleted
                   || !awaitable3.GetAwaiter().IsCompleted
                   || !awaitable4.GetAwaiter().IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            T1 result1 = await awaitable1;
            T2 result2 = await awaitable2;
            T3 result3 = await awaitable3;
            T4 result4 = await awaitable4;
            return (result1, result2, result3, result4);
        }

        public static async Awaitable<(T1 Result1, T2 Result2, T3 Result3, T4 Result4, T5 Result5)> WhenAll<T1, T2, T3, T4, T5>(
            Awaitable<T1> awaitable1,
            Awaitable<T2> awaitable2,
            Awaitable<T3> awaitable3,
            Awaitable<T4> awaitable4,
            Awaitable<T5> awaitable5,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.GetAwaiter().IsCompleted
                   || !awaitable2.GetAwaiter().IsCompleted
                   || !awaitable3.GetAwaiter().IsCompleted
                   || !awaitable4.GetAwaiter().IsCompleted
                   || !awaitable5.GetAwaiter().IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            T1 result1 = await awaitable1;
            T2 result2 = await awaitable2;
            T3 result3 = await awaitable3;
            T4 result4 = await awaitable4;
            T5 result5 = await awaitable5;
            return (result1, result2, result3, result4, result5);
        }

        public static async Awaitable<(T1 Result1, T2 Result2, T3 Result3, T4 Result4, T5 Result5, T6 Result6)> WhenAll<T1, T2, T3, T4, T5,
            T6>(
            Awaitable<T1> awaitable1,
            Awaitable<T2> awaitable2,
            Awaitable<T3> awaitable3,
            Awaitable<T4> awaitable4,
            Awaitable<T5> awaitable5,
            Awaitable<T6> awaitable6,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.GetAwaiter().IsCompleted
                   || !awaitable2.GetAwaiter().IsCompleted
                   || !awaitable3.GetAwaiter().IsCompleted
                   || !awaitable4.GetAwaiter().IsCompleted
                   || !awaitable5.GetAwaiter().IsCompleted
                   || !awaitable6.GetAwaiter().IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            T1 result1 = await awaitable1;
            T2 result2 = await awaitable2;
            T3 result3 = await awaitable3;
            T4 result4 = await awaitable4;
            T5 result5 = await awaitable5;
            T6 result6 = await awaitable6;
            return (result1, result2, result3, result4, result5, result6);
        }

        public static async Awaitable<(T1 Result1, T2 Result2, T3 Result3, T4 Result4, T5 Result5, T6 Result6, T7 Result7)> WhenAll<T1, T2,
            T3, T4, T5, T6, T7>(
            Awaitable<T1> awaitable1,
            Awaitable<T2> awaitable2,
            Awaitable<T3> awaitable3,
            Awaitable<T4> awaitable4,
            Awaitable<T5> awaitable5,
            Awaitable<T6> awaitable6,
            Awaitable<T7> awaitable7,
            CancellationToken cancellationToken = default)
        {
            while (!awaitable1.GetAwaiter().IsCompleted
                   || !awaitable2.GetAwaiter().IsCompleted
                   || !awaitable3.GetAwaiter().IsCompleted
                   || !awaitable4.GetAwaiter().IsCompleted
                   || !awaitable5.GetAwaiter().IsCompleted
                   || !awaitable6.GetAwaiter().IsCompleted
                   || !awaitable7.GetAwaiter().IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            T1 result1 = await awaitable1;
            T2 result2 = await awaitable2;
            T3 result3 = await awaitable3;
            T4 result4 = await awaitable4;
            T5 result5 = await awaitable5;
            T6 result6 = await awaitable6;
            T7 result7 = await awaitable7;
            return (result1, result2, result3, result4, result5, result6, result7);
        }

        public static async Awaitable<(T1 Result1, T2 Result2, T3 Result3, T4 Result4, T5 Result5, T6 Result6, T7 Result7, T8 Result8)>
            WhenAll<T1, T2, T3, T4, T5, T6, T7, T8>(
                Awaitable<T1> awaitable1,
                Awaitable<T2> awaitable2,
                Awaitable<T3> awaitable3,
                Awaitable<T4> awaitable4,
                Awaitable<T5> awaitable5,
                Awaitable<T6> awaitable6,
                Awaitable<T7> awaitable7,
                Awaitable<T8> awaitable8,
                CancellationToken cancellationToken = default)
        {
            while (!awaitable1.GetAwaiter().IsCompleted
                   || !awaitable2.GetAwaiter().IsCompleted
                   || !awaitable3.GetAwaiter().IsCompleted
                   || !awaitable4.GetAwaiter().IsCompleted
                   || !awaitable5.GetAwaiter().IsCompleted
                   || !awaitable6.GetAwaiter().IsCompleted
                   || !awaitable7.GetAwaiter().IsCompleted
                   || !awaitable8.GetAwaiter().IsCompleted)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            T1 result1 = await awaitable1;
            T2 result2 = await awaitable2;
            T3 result3 = await awaitable3;
            T4 result4 = await awaitable4;
            T5 result5 = await awaitable5;
            T6 result6 = await awaitable6;
            T7 result7 = await awaitable7;
            T8 result8 = await awaitable8;
            return (result1, result2, result3, result4, result5, result6, result7, result8);
        }

        private static bool AreAllTasksComplete(List<Awaitable> awaitables)
        {
            foreach (Awaitable awaitable in awaitables)
            {
                if (!awaitable.IsCompleted)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
#endif