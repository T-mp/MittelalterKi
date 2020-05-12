using Microsoft.Extensions.Logging;
using MittelalterKi.Data.StateMachine.Bedürfnise;
using MittelalterKi.Data.StateMachine.Fähigkeiten;
using MittelalterKi.Data.StateMachine.Handlungen;

namespace MittelalterKi.Data.StateMachine
{
    public class EinIndividuum: Individuum
    {
        private static readonly System.Random rnd = new System.Random();

        public EinIndividuum(ILogger<EinIndividuum> logger) : base(logger)
        {
            Bedürfnise.Add(new GrundBedürfnis("Narung", rnd.Next(10, 100)) { Min = 10, SollMin = 20, SollMax = 100 });
            Bedürfnise.Add(new GrundBedürfnis("Wasser", rnd.Next(20, 80)){ Min = 10, SollMin = 20, SollMax = 100 });
            Bedürfnise.Add(new Bedürfnis("NarungsLager", rnd.Next(0, 500)) { SollMin = 50, SollMax = 1000 });
            Bedürfnise.Add(new GrundBedürfnis("Energi", rnd.Next(0, 100)) { SollMin = 0, SollMax = 100 });

            atuelleHandlung = new Warte(this, logger);
        }
    }
}
