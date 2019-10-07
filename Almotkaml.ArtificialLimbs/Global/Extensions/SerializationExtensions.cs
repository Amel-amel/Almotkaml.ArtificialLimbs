using Newtonsoft.Json;

namespace Almotkaml.ArtificialLimbs.Global.Extensions
{
    public static class SerializationExtensions
    {
        public static string ToSerializedObject(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static TObject ToDeserializedObject<TObject>(this string serializedObject)
            where TObject : class
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<TObject>(serializedObject);
                return obj;
            }
            catch
            {
                // ignored
            }
            return null;
        }
    }
}