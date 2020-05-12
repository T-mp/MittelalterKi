using MittelalterKi.Data.StateMachine.Bedürfnise;
using System.Collections.Generic;

namespace MittelalterKi.Data.StateMachine.Materialien
{
    public class WassermelonenKerne : ISamen
    {
        public string Name => GetType().Name;

        public decimal Menge { get; set; } = 100;
        public IReadOnlyList<IBedürfnis> Befridigt { get; } = new List<IBedürfnis>();

        public string Bild => $"🍉k:{Menge:0.0}";

        public IReadOnlyList<IMaterial> Braucht { get; } = new List<IMaterial> { new Wasser() /*, new Feld()*/ };
        public IMaterial WirdZu { get; } = new Wassermelone();
    }
}