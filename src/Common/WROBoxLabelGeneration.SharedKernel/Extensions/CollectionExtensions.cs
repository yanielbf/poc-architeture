namespace WROBoxLabelGeneration.SharedKernel.Extensions
{
    public static class CollectionExtensions
    {
        public static List<List<T>> ChunkAsLists<T>(this List<T> source, int chunkSize)
        {
            var result = new List<List<T>>();
            for (int i = 0; i < source.Count; i += chunkSize)
            {
                result.Add(source.GetRange(i, Math.Min(chunkSize, source.Count - i)));
            }
            return result;
        }
    }
}
