using Newtonsoft.Json;
using System;

namespace BlazorServerApp20220519
{
    public static class DevCode
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
