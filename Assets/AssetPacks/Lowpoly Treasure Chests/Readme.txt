Notes on prefabs:
- The treasure chests come in three varieties: animated, closed and a version with a loose lid
	- The "Animated" prefab contains an animator controller set up with the provided animations.
	- The "Closed" prefab is a single static mesh. This prefab could be used in cases where the treasure chests do not need to animate or be opened
	- The "Loose_Lid" prefab consists of multiple separate meshes (base, lid, loot). These meshes can be shown/hidden or animated (through code).

Notes on meshes, animations and materials:
- All three treasure chests share the same animations. These animations have been exported as a separate asset.
- All assets share a single material with atlassed textures. There is a albedo/gloss texture and a metallic texture.