using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogPessoal.src.utilities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        NORMAL,
        ADMIN
    }
}
