// Jakar.Extensions :: Jakar.Collections
// 11/16/2026

namespace Jakar.Extensions;


public static class ArrayBuffers
{
    /// <summary> Copies <paramref name="source"/> into a pooled <see cref="ArrayBuffer{TValue}"/> (was <c> AsyncLinq.ToArrayBuffer </c> in Jakar.Extensions). </summary>
    [Pure] [MustDisposeResource] public static ArrayBuffer<TValue> ToArrayBuffer<TEnumerator, TValue>( this in ValueEnumerable<TEnumerator, TValue> source )
        where TEnumerator : struct, IValueEnumerator<TValue>, allows ref struct => ArrayBuffer<TValue>.Create(source);
}
