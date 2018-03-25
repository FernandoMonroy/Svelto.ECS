﻿using System;
using Svelto.ECS.Internal;

namespace Svelto.ECS
{
    public class EntityViewBuilder<EntityViewType> : IEntityViewBuilder where EntityViewType : EntityView, new()
    {
        public void BuildEntityViewAndAddToList(ref ITypeSafeList list, int entityID, out IEntityView entityView)
        {
            if (list == null)
                list = new TypeSafeFasterListForECSForClasses<EntityViewType>();

            var castedList = list as TypeSafeFasterListForECSForClasses<EntityViewType>;

            var lentityView = EntityView<EntityViewType>.BuildEntityView(entityID);

            castedList.Add(lentityView);

            entityView = lentityView;
        }

        public ITypeSafeList Preallocate(ref ITypeSafeList list, int size)
        {
            if (list == null)
                list = new TypeSafeFasterListForECSForClasses<EntityViewType>(size);
            else
                list.ReserveCapacity(size);

            return list;
        }

        public Type[] GetEntityViewType()
        {
            return _entityViewType;
        }

        public void MoveEntityView(int entityID, ITypeSafeList fromSafeList, ITypeSafeList toSafeList)
        {
            var fromCastedList = fromSafeList as TypeSafeFasterListForECSForClasses<EntityViewType>;
            var toCastedList = toSafeList as TypeSafeFasterListForECSForClasses<EntityViewType>;

            toCastedList.Add(fromCastedList[fromCastedList.GetIndexFromID(entityID)]);
        }

        public bool mustBeFilled
        {
            get { return true; }
        }

        readonly Type[] _entityViewType = {typeof(EntityViewType)};
    }
}