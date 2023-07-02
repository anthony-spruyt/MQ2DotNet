using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Pet object.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-pet/
    /// </summary>
    [PublicAPI]
    [MQ2Type("pet")]
    public class PetType : SpawnType
    {
        public const int MAX_PET_BUFFS = 100;

        internal PetType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _buff = new IndexedMember<PetBuffType, int, IntType, string>(this, "Buff");
            _buffDuration = new IndexedMember<TimeStampType, int, TimeStampType, string>(this, "BuffDuration");
            _findBuff = new IndexedMember<BuffType>(this, "FindBuff");
        }

        /// <summary>
        /// Returns the slot number (base 1) for buffname
        /// Buff[buffname]
        /// 
        /// The buff at the given slot (base 1)
        /// Buff[slot]
        /// 
        /// Cast the to <see cref="uint"/> when searching by buff name since the returned index of the buff is stored in the <see cref="MQ2VarPtr.Dword"/> property.
        /// </summary>
        private readonly IndexedMember<PetBuffType, int, IntType, string> _buff;

        /// <summary>
        /// Returns the slot number (base 1) for buffname.
        /// </summary>
        /// <param name="buffName">The name of the buff.</param>
        /// <returns>The base 1 index of the pet buff.</returns>
        public uint? GetPetBuff(string buffName) => _buff[buffName];

        /// <summary>
        /// The buff at the given slot (base 1).
        /// </summary>
        /// <param name="slot">The base 1 slot.</param>
        /// <returns>The pet buff.</returns>
        public PetBuffType GetPetBuff(int slot) => _buff[slot];

        /// <summary>
        /// Buff time remaining for pet buff buffname in miliseconds
        /// BuffDuration[buffname]
        /// 
        /// Buff time remaining for pet buff in slot slot (base 1) in miliseconds
        /// BuffDuration[slot]
        /// 
        /// Cast the result to <see cref="TimeSpan"/>.
        /// </summary>
        private readonly IndexedMember<TimeStampType, int, TimeStampType, string> _buffDuration;

        /// <summary>
        /// Get the remaining duration of a pet buff.
        /// </summary>
        /// <param name="slot">The base 1 slot.</param>
        /// <returns>The remaining duration of the buff.</returns>
        public TimeSpan? GetPetBuffDuration(int slot) => _buffDuration[slot];

        /// <summary>
        /// Get the remaining duration of a pet buff.
        /// </summary>
        /// <param name="buffName">The name of the pet buff</param>
        /// <returns>The remaining duration of the buff.</returns>
        public TimeSpan? GetPetBuffDuration(string buffName) => _buffDuration[buffName];

        /// <summary>
        /// Combat state
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
        /// Returns the pet's current stance, (e.g. FOLLOW, GUARD)
        /// </summary>
        public PetStance? Stance => GetMember<StringType>("Stance");
        
        /// <summary>
        /// Is Stop enabled?
        /// </summary>
        public bool Stop => GetMember<BoolType>("Stop");

        /// <summary>
        /// Returns the pet's current target.
        /// </summary>

        [JsonIgnore]
        public SpawnType Target => GetMember<SpawnType>("Target");
        
        /// <summary>
        /// Is Taunt enabled?
        /// </summary>
        public bool Taunt => GetMember<BoolType>("Taunt");

        /// <summary>
        /// Focus state
        /// </summary>
        public bool Focus => GetMember<BoolType>("Focus");

        /// <summary>
        /// Online doco has no info on it, just that it exists.
        /// </summary>
        private readonly IndexedMember<BuffType> _findBuff;

        /// <summary>
        /// Get a buff by name if on the pet.
        /// </summary>
        /// <param name="buffName"></param>
        /// <returns></returns>
        public BuffType FindBuff2(string buffName) => _findBuff[buffName];

        /// <summary>
        /// All the current pet buffs.
        /// </summary>
        public IEnumerable<PetBuffType> PetBuffs
        {
            get
            {
                var index = 1;

                while (index <= MAX_PET_BUFFS)
                {
                    var item = GetPetBuff(index);

                    if (item != null)
                    {
                        index++;

                        yield return item;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}