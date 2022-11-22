using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using ThePlayList.Work.Enums;

namespace ThePlayList.Work.Entities
{
    public class Work
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Connect> Connects { get; set; }
        public Status Status { get; set; }
        public DateTime LastWorkDate { get; set; }

    }
}
