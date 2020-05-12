using Microsoft.Extensions.Logging;
using MittelalterKi.Data.StateMachine.Bedürfnise;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MittelalterKi.Data.StateMachine.Handlungen
{
    public abstract class IndividuumsBasisHandlung : IHandlung
    {
        protected readonly Individuum individuum;
        protected readonly ILogger logger;

        public IndividuumsBasisHandlung(Individuum individuum, ILogger logger)
        {
            this.individuum = individuum;
            this.logger = logger;
        }
        protected IEnumerable<IBedürfnis> ReduziereGrundBedürfnise(decimal zeitEinheiten = 1)
        {
            var grundBedürfnise = individuum.Bedürfnise
                .Where(b => b.Name == "Narung" || b.Name == "Wasser");
            foreach (var gb in grundBedürfnise)
            {
                gb.Wert -= zeitEinheiten;
            }

            var schlechte = grundBedürfnise.Where(b => b.Wert < b.SollMin);
            if (schlechte.Any())
            {
                logger.LogDebug($"{individuum}.ReduziereGrundBedürfnise({zeitEinheiten}) => schlechte=[{string.Join(",", schlechte.Select(b => $"'{b.Name}':{b.Wert}<{b.SollMin}"))}]");
            }
            return schlechte;
        }

        public abstract Task<IHandlung> BerechneNächste(decimal zeitEinheiten = 1);
    }
}
