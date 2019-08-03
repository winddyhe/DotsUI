﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using DotsUI.Core;
using UnityEngine.UI;

namespace DotsUI.Hybrid
{
    internal class ScrollBarConverter : TypedConverter<UnityEngine.UI.Scrollbar>
    {
        protected override void ConvertComponent(UnityEngine.UI.Scrollbar unityComponent, Entity entity, RectTransformToEntity rectTransformToEntity, Dictionary<UnityEngine.Object, Entity> assetToEntity, EntityManager mgr)
        {
            var scrollHandle = rectTransformToEntity[unityComponent.handleRect];
            mgr.AddComponentData(entity, new DotsUI.Controls.ScrollBar(){
                ScrollHandle = scrollHandle,
                ParentScrollRect = rectTransformToEntity[unityComponent.GetComponentInParent<ScrollRect>().transform as UnityEngine.RectTransform]
            });
            mgr.AddComponentData(entity, new Controls.ScrollBarHandle()
            {

            });
            var pointerInputReceiver = GetOrAddComponent<Input.PointerInputReceiver>(mgr, entity);
            pointerInputReceiver.ListenerTypes |= Input.PointerEventType.BeginDrag | Input.PointerEventType.Drag |
                                                  Input.PointerEventType.EndDrag;
            mgr.SetComponentData(entity, pointerInputReceiver);
        }
    }
}