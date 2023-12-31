﻿using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Denobrium.Json.Benchmark.DataObjects
{
    [DataContract(Name = "Spoon")]
    [JsonObject(MemberSerialization.OptOut)]
    [Serializable]
    public class Spoon : Product
    {
        public Spoon() { }

        public Spoon(string name, string code, int capacityInMillis)
        {
            Name = name;
            Code = code;
            CapacityInMillis = capacityInMillis;
        }

        public int CapacityInMillis { get; set; }
    }
}
