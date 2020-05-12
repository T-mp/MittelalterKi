using MittelalterKi.Data.StateMachine.Bedürfnise;
using System.Collections.Generic;

namespace MittelalterKi.Data.StateMachine.Materialien
{
    public class Wassermelone : IMaterial
    {
        public string Name => GetType().Name;

        public decimal Menge { get; set; } = 100;
        public IReadOnlyList<IBedürfnis> Befridigt { get; } = new List<IBedürfnis> {
            new Bedürfnis("Narung", 5),
            new Bedürfnis("Energi", 3),
            new Bedürfnis("Wasser", 3),
        };

        public string Bild => $"🍉:{Menge:0.0}";

        public IReadOnlyList<IMaterial> Braucht { get; } = new List<IMaterial>();
    }
}