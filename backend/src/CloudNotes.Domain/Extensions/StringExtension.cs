using Newtonsoft.Json;

namespace CloudNotes.Domain.Extensions;

public static class StringExtension
{
    public static string SerializeObject(this object obj)
    {
        return (obj == null) ? default : JsonConvert.SerializeObject(obj);
    }
}
