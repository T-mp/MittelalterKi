using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MittelalterKi.Data.StateMachine.Handlungen
{
    internal class Trinken : IndividuumsBasisSubHandlung
    {
        public Trinken(Individuum individuum, IHandlung vorherigeAktion, ILogger logger) : base(individuum, vorherigeAktion, logger)
        {
            logger.LogDebug($"[{individuum.Name}].Beginne Trinken");
        }

#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt. Ist einen enfache implementierung ohne asyncrone Vorgänge
        public override async Task<IHandlung> BerechneNächste(decimal zeitEinheiten = 1)
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {
            var wasser = individuum.Bedürfnise.FirstOrDefault(b => b.Name == "Wasser");
            if (wasser != null)
            {
                //Beim Trinken wird Wasser mehr aufgefüllt als "normalerweise" gesenkt
                wasser.Wert += zeitEinheiten * 6;
            }

            var schlecht = ReduziereGrundBedürfnise(zeitEinheiten);

            if (schlecht.Any(b => b.Name == "Wasser"))
            {
                return this;
            }
            if (schlecht.Any(b => b.Name == "Narung"))
            {
                return new Essen(individuum, this, logger);
            }
            if (wasser != null
                && wasser.Wert > (wasser.SollMax / 100) * 80)
            {
                logger.LogDebug($"[{individuum.Name}].Beende Trinken");
                return vorherigeAktion;
            }

            return this;
        }
    }
}
