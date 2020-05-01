using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MittelalterKi.Data.StateMachine.Handlungen
{
    public class BestelleFeld : IndividuumsBasisSubHandlung
    {
        private static readonly System.Random rnd = new System.Random();

        public BestelleFeld(Individuum individuum, IHandlung vorherigeAktion, ILogger logger) : base(individuum, vorherigeAktion, logger) {
            logger.LogDebug($"[{individuum.Name}] {vorherigeAktion?.GetType()?.Name} => BestelleFeld.Begin");
        }

#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt. Ist einen enfache implementierung ohne asyncrone Vorgänge
        public override async Task<IHandlung> BerechneNächste(decimal zeitEinheiten = 1)
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {
            var schlecht = ReduziereGrundBedürfnise(zeitEinheiten);

            if (schlecht.Any(b => b.Name == "Wasser"))
            {
                return new Trinken(individuum, this, logger);
            }
            if (schlecht.Any(b => b.Name == "Narung"))
            {
                return new Essen(individuum, this, logger);
            }

            var energi = individuum.Bedürfnise.FirstOrDefault(b => b.Name == "Energi");
            var narungsLager = individuum.Bedürfnise.FirstOrDefault(b => b.Name == "NarungsLager");

            if (energi==null
             || narungsLager == null
             || energi.Wert <= 0 
             || (energi.Wert < 30 && narungsLager.Wert < narungsLager.SollMin)
             || narungsLager.Wert >= narungsLager.SollMax)
            {
                logger.LogDebug($"[{individuum.Name}].Beende BestelleFeld => {vorherigeAktion?.GetType()?.Name}");
                return vorherigeAktion;
            }

            narungsLager.Wert += rnd.Next(0,10);
            energi.Wert -= rnd.Next(1, 3);
            if (narungsLager.Wert >= narungsLager.SollMax)
            {
                narungsLager.Wert = narungsLager.SollMax;
                logger.LogDebug($"[{individuum.Name}] BestelleFeld.Ende => {vorherigeAktion?.GetType()?.Name}");
                return vorherigeAktion;
            }

            return this;
        }
    }
}
