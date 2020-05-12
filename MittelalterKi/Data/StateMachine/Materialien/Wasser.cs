using MittelalterKi.Data.StateMachine.Bedürfnise;
using System.Collections.Generic;

namespace MittelalterKi.Data.StateMachine.Materialien
{
    public class Wasser : IMaterial
    {
        public string Name => GetType().Name;

        public decimal Menge { get; set; } = 100;
        public IReadOnlyList<IBedürfnis> Befridigt { get; } = new List<IBedürfnis> {
            new Bedürfnis("Wasser", 10),
        };

        public string Bild => $"🚰:{Menge:0.0}";

        public IReadOnlyList<IMaterial> Braucht { get; } = new List<IMaterial>();
    }
}