using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Consignment_Control.Library.Core.Entity
{
    /// <summary>
    /// simple base entity class, EF Domain base file
    /// </summary>
    public abstract partial class CoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Compare Object Type with corebaseentity
        /// </summary>
        /// <param name="obj">Generic Object</param>
        /// <returns>True / False</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as CoreBaseEntity);
        }

        /// <summary>
        /// Check the value with base entity
        /// </summary>
        /// <param name="obj">Anyobject which inherit CoreBaseEntity class</param>
        /// <returns>True / False</returns>
        private static bool IsTransient(CoreBaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }

        /// <summary>
        /// Get the objet type
        /// </summary>
        /// <returns>Object Type</returns>
        private Type GetUnproxiedType()
        {
            return GetType();
        }

        /// <summary>
        /// Compare the object type
        /// </summary>
        /// <param name="other">Anyobject which inherit CoreBaseEntity class</param>
        /// <returns>True / False</returns>
        public virtual bool Equals(CoreBaseEntity other)
        {
            if (other == null)
                return false;

            // Compare the two instance 
            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        /// <summary>
        /// Get default object hastcode
        /// </summary>
        /// <returns>Hascode in INT Type</returns>
        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        /// <summary>
        /// Determinies whether the specified object instances are considered equal.
        /// </summary>
        /// <param name="x">the first object to compare</param>
        /// <param name="y">the object compare with the first object</param>
        /// <returns>True / False</returns>
        public static bool operator ==(CoreBaseEntity x, CoreBaseEntity y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// Determinies whether the specified object instances are considered NOT equal.
        /// </summary>
        /// <param name="x">the first object to compare</param>
        /// <param name="y">the object compare with the first object</param>
        /// <returns>True / False</returns>
        public static bool operator !=(CoreBaseEntity x, CoreBaseEntity y)
        {
            return !(x == y);
        }
    }
}