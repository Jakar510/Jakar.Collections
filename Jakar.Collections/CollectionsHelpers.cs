// Jakar.Extensions :: Jakar.Collections
// 11/16/2026

namespace Jakar.Extensions;


/// <summary> The Jakar.Extensions helpers these collections use, kept internal so they don't clash with Jakar.Extensions' public ones. </summary>
internal static class CollectionsHelpers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool ShouldContinue( this CancellationToken token ) => !token.IsCancellationRequested;

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Span<TValue> AsSpan<TValue>( this List<TValue> list ) => CollectionsMarshal.AsSpan(list);


    /// <summary> <see langword="true"/> when every one of <paramref name="values"/> is in <paramref name="span"/>. </summary>
    public static bool ContainsAll<TValue>( this ReadOnlySpan<TValue> span, EqualityComparer<TValue> comparer, params ReadOnlySpan<TValue> values )
    {
        foreach ( TValue value in values )
        {
            bool found = false;

            foreach ( TValue item in span )
            {
                if ( !comparer.Equals(item, value) ) { continue; }

                found = true;
                break;
            }

            if ( !found ) { return false; }
        }

        return true;
    }
    public static bool ContainsAll<TValue>( this Span<TValue> span, EqualityComparer<TValue> comparer, params ReadOnlySpan<TValue> values ) => ( (ReadOnlySpan<TValue>)span ).ContainsAll(comparer, values);


    public static async IAsyncEnumerable<(int Index, TValue Value)> Enumerate<TValue>( this IAsyncEnumerable<TValue> source, int start, [EnumeratorCancellation] CancellationToken token = default )
    {
        int index = start;
        await foreach ( TValue value in source.WithCancellation(token).ConfigureAwait(false) ) { yield return ( index++, value ); }
    }
}



/// <summary> Jakar.Extensions' <c> Json.Serialize/Deserialize&lt;T, TConverter&gt; </c>, for the collections' JSON helpers. </summary>
internal static class CollectionsJson
{
    private static readonly JsonReaderOptions __readerOptions = new() { AllowComments = true, AllowTrailingCommas = true };


    public static string Serialize<T, TConverter>( in T value, bool indented = false )
        where TConverter : IJsonConverter<T>
    {
        JsonWriter writer = new(stackalloc char[256], indented ? JsonWriterOptions.Indent : JsonWriterOptions.Default);

        try
        {
            TConverter.Write(ref writer, in value);
            return writer.ToString();
        }
        finally { writer.Dispose(); }
    }


    public static T Deserialize<T, TConverter>( scoped ReadOnlySpan<char> json )
        where TConverter : IJsonConverter<T>
    {
        JsonReader<char> reader = new(json, __readerOptions);

        try
        {
            return TConverter.TryRead(ref reader, out T? value) && reader.TryReadEnd()
                       ? value
                       : throw reader.CreateException();
        }
        finally { reader.Dispose(); }
    }


    public static bool TryDeserialize<T, TConverter>( scoped ReadOnlySpan<char> json, [MaybeNullWhen(false)] out T value )
        where TConverter : IJsonConverter<T>
    {
        JsonReader<char> reader = new(json, __readerOptions);

        try
        {
            if ( TConverter.TryRead(ref reader, out value) && reader.TryReadEnd() ) { return true; }

            value = default;
            return false;
        }
        finally { reader.Dispose(); }
    }
}