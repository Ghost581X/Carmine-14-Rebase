using System.Numerics;
using Robust.Shared.Prototypes;

namespace Content.Shared._Crescent.SpaceBiomes;

[Prototype("ambientSpaceBiome")]
public sealed class SpaceBiomePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public string Name = "";

    [DataField(required: false)]
    public string Description = "";

    [DataField(required: false)]
    public Color BiomeColor = Color.Violet;
    [DataField(required: false)]
    public float BiomeAlpha = 0.1f;
    [DataField(required: false)]
    public List<Vector2> VertexCoordinates = new List<Vector2>();
    [DataField(required: false)]
    public Vector2? TitleCoordinates;
    [DataField(required: false)]
    public string Parallax = "Default";
    [DataField(required: false)]
    public float SwapDuration = 2f; //in seconds
}
