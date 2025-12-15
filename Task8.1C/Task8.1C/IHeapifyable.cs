namespace Task8._1C
{
    public interface IHeapifyable<K, D>
    {
        D Data { get; set; }
        K Key { get; }
        int Position { get; }
    }
}
