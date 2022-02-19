﻿using Ipme.WikiBeer.Entities.AssociationTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities
{
    public class UserBeer : IAssociationTable
    {
        public Guid UserId { get; private set; }
        public UserEntity User { get; private set; }

        public Guid BeerId { get; private set; }
        public BeerEntity Beer { get; private set; }

        public UserBeer(Guid userId, Guid beerId)
        {
            UserId = userId;
            BeerId = beerId;        }

        public UserBeer(Guid userId, Guid beerId, UserEntity user, BeerEntity beer)
            : this(userId, beerId)
        {
            User = user;
            Beer = beer;
        }

        public (Guid, Guid) GetCompositeKey()
        {
            return (UserId,BeerId);
        }

        public bool IsInCompositeKey(Guid id)
        {
            return id == UserId || id == BeerId;
        }

        public bool Equals(UserBeer? other)
        {
            if (other is null)
                return false;
            else return this.GetCompositeKey() == other.GetCompositeKey();
        }

        public bool Equals(IAssociationTable? other)
        {
            if (other is null || other is not UserBeer) return false;
            else return this.GetCompositeKey() == other.GetCompositeKey();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return GetCompositeKey().GetHashCode();
        }
    }
}
