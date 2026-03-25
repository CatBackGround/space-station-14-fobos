using Content.Shared.DeadSpace.Implants.Radiance.Components;
using Content.Shared.Implants;

namespace Content.Server.DeadSpace.Implants.Radiance.Systems;

/// <summary>
/// System used for adding or removing components with a radiance implant
/// </summary>
public sealed class RadianceSystem : EntitySystem
{

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RadianceImplantComponent, ImplantImplantedEvent>(OnImplantImplanted);
        SubscribeLocalEvent<RadianceImplantComponent, ImplantRemovedEvent>(OnImplantRemoved);
    }

    private void OnImplantImplanted(Entity<RadianceImplantComponent> ent, ref ImplantImplantedEvent args)
    {
        if (args.Implanted == null)
            return;

        EnsureComp<RadianceComponent>(args.Implanted);
    }

    private void OnImplantRemoved(Entity<RadianceImplantComponent> ent, ref ImplantRemovedEvent args)
    {
        RemComp<RadianceComponent>(args.Implanted);
    }
}
