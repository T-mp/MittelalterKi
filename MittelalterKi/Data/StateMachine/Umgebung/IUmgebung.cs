using MittelalterKi.Data.StateMachine.Materialien;
using System.Collections.Generic;

namespace MittelalterKi.Data.StateMachine.Umgebung
{
    public interface IUmgebung
    {
        decimal Y { get; set; }
        decimal X { get; set; }
        decimal Z { get; set; }

        IList<IMaterial> Bietet { get; set; }

        string Bild { get; }
    }
}