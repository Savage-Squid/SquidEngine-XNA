using System;
using System.Collections.Generic;
using System.Text;

namespace SquidEditor
{
    internal enum SceneItemGroup
    {
        GlobalTemplates,
        LocalTemplates,
        SceneInstances
    }

    internal enum SceneItemType
    {
        Sprite,
        AnimatedSprite,
        TileGrid,
        ParticleEffect,
        PostProcessingAnimation,
        TextItem,
        CompositeEntity,
        Default,
    }
}
