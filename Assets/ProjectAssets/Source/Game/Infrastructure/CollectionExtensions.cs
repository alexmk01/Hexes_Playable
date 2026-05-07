
namespace Game.Infrastructure 
{
    public static class CollectionExtensions
    {
        //Tuple array allocation may be threated as a function by playworks js compiler. So use this instead of new (..)[length]
        public static T[] CreateArray<T>(int length) where T : struct
        {
            return new T[length];
        }
    }
}