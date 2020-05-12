using MittelalterKi.Data.StateMachine.Bedürfnise;
using System.Collections.Generic;

namespace MittelalterKi.Data.StateMachine.Materialien
{
    public interface IMaterial
    {
        string Name { get; }
        decimal Menge { get; set; }

        IReadOnlyList<IBedürfnis> Befridigt { get; }
        IReadOnlyList<IMaterial> Braucht { get; }

        string Bild { get; }
    }
}