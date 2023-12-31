﻿using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Denobrium.Json.Benchmark.DataObjects
{
    [DataContract(Name = "Knife")]
    [JsonObject(MemberSerialization.OptOut)]
    [Serializable]
    public class Knife : Product
    {
        public Knife() { }

        public Knife(string name, string code, short levelOfSharpness)
        {
            Name = name;
            Code = code;
            LevelOfSharpness = levelOfSharpness;
        }

        public short LevelOfSharpness{ get; set; }
    }
}
