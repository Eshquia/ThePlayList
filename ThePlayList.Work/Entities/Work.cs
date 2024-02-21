using EventBusRabbitMQ.Events.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using ThePlayList.Work.Enums;

namespace ThePlayList.Work.Entities
{
    public class Work:IEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }
        public Dictionary<string, Operator> Operators { get; set; }
        public Dictionary<string, Link> Links { get; set; }
        public Dictionary<string, object> OperatorTypes { get; set; }
        public Status Status { get; set; }
        public DateTime LastWorkDate { get; set; }

    }
}
