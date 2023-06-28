using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a pet.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("pet")]
    public class PetType : MQ2DataType
    {
        internal PetType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _buff = new IndexedMember<PetBuffType, int, IntType, string>(this, "Buff");
            _buffDuration = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "BuffDuration");
            _findBuff = new IndexedMember<BuffType>(this, "FindBuff");
        }

        /// <summary>
        /// A buff on your pet index (1 based), or the index of a buff on your pet by name.
        /// Cast the to <see cref="uint"/> when searching by buff name since the returned index of the buff is stored in the <see cref="MQ2VarPtr.Dword"/> property.
        /// </summary>
        [JsonIgnore]
        private IndexedMember<PetBuffType, int, IntType, string> _buff { get; }

        /// <summary>
        /// Find the 1 based index of a pet buff by name.
        /// </summary>
        /// <param name="name">The name of the buff.</param>
        /// <returns>The 1 based index of the pet buff.</returns>
        public uint? GetPetBuff(string name) => _buff[name];

        /// <summary>
        /// Get a buff on your pet by index.
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns>The pet buff.</returns>
        public PetBuffType GetPetBuff(int index) => _buff[index];

        /// <summary>
        /// Remaining duration on a pet's buff, by spell name or index (1 based).
        /// Cast the result to <see cref="TimeSpan"/>.
        /// </summary>
        [JsonIgnore]
        private IndexedMember<TimeStampType, int, TimeStampType, string> _buffDuration { get; }

        /// <summary>
        /// Get the remaining duration of a pet buff.
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns>The remaining duration of the buff.</returns>
        public TimeSpan? GetPetBuffDuration(int index) => _buffDuration[index];

        /// <summary>
        /// Get the remaining duration of a pet buff.
        /// </summary>
        /// <param name="name">The name of the pet buff</param>
        /// <returns>The remaining duration of the buff.</returns>
        public TimeSpan? GetPetBuffDuration(string name) => _buffDuration[name];

        /// <summary>
        /// Is pet in combat?
        /// </summary>
        public bool Combat => GetMember<BoolType>("Combat");
        
        /// <summary>
        /// Is GHold enabled?
        /// </summary>
        public bool GHold => GetMember<BoolType>("GHold");
        
        /// <summary>
        /// Is Hold enabled?
        /// </summary>
        public bool Hold => GetMember<BoolType>("Hold");
        
        /// <summary>
        /// Is ReGroup enabled?
        /// </summary>
        public bool ReGroup => GetMember<BoolType>("ReGroup");
        
        /// <summary>
        /// Current stance, either "FOLLOW" or "GUARD"
        /// </summary>
        public PetStance? Stance => GetMember<StringType>("Stance");
        
        /// <summary>
        /// Is Stop enabled?
        /// </summary>
        public bool Stop => GetMember<BoolType>("Stop");
        
        /// <summary>
        /// Pet's target
        /// </summary>
        public SpawnType Target => GetMember<SpawnType>("Target");
        
        /// <summary>
        /// Is Taunt enabled?
        /// </summary>
        public bool Taunt => GetMember<BoolType>("Taunt");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool Focus => GetMember<BoolType>("Focus");

        /// <summary>
        /// TODO: new member
        /// </summary>
        [JsonIgnore]
        private IndexedMember<BuffType> _findBuff { get; }

        /// <summary>
        /// Get a buff by name if on the pet.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BuffType GetBuff(string name) => _findBuff[name];

        /// <summary>
        /// All the current pet buffs.
        /// </summary>
        public IEnumerable<PetBuffType> PetBuffs
        {
            get
            {
                var items = new List<PetBuffType>();
                var index = 1;

                while (true)
                {
                    var item = GetPetBuff(index);

                    if (item != null && index <= 100)
                    {
                        items.Add(item);
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                return items;
            }
        }
    }
}