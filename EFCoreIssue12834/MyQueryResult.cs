using System;
using System.Linq.Expressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EFCoreIssue12834
{
    public class MyQueryResult
    {
        public long PrimaryId { get; set; }

        public long SecondaryId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PrimaryEntityType PrimaryType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SecondaryEntityType SecondaryType { get; set; }

        public static readonly Expression<Func<MyPrimaryEntity, MyQueryResult>> FromPrimaryEntityProjection
            = entity => new MyQueryResult()
            {
                PrimaryId = entity.Id,
                SecondaryId = entity.SecondaryEntity.Id,
                PrimaryType = entity.Type,
                //SecondaryType = entity.SecondaryEntity.Type
                SecondaryType = Enum.Parse<SecondaryEntityType>(entity.SecondaryEntity.Type.ToString())
            };

        public static readonly Expression<Func<MySecondaryEntity, MyQueryResult>> FromSecondaryEntityProjection
            = entity => new MyQueryResult()
            {
                PrimaryId = entity.PrimaryEntity.Id,
                SecondaryId = entity.Id,
                //PrimaryType = entity.PrimaryEntity.Type,
                PrimaryType = Enum.Parse<PrimaryEntityType>(entity.PrimaryEntity.Type.ToString()),
                SecondaryType = entity.Type
            };
    }
}
