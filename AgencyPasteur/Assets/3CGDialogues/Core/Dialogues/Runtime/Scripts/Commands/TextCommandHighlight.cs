using System.Globalization;
using TCG.Dialogues.Core.Camera;

namespace TCG.Core.Dialogues
{ 
    internal class TextCommandHighlight : TextCommand
    {
        private float _duration = 3f;
        private string _spotlighted = "paillasses";
        public override void SetupData(string strCommandData)
        {
            string[] args = strCommandData.Split("|");
            if (args.Length >= 1)
            {
                _duration = float.Parse(args[0], CultureInfo.InvariantCulture);
            }
            if (args.Length >= 2)
            {
                _spotlighted = args[1] ;
            }
        }
        public override void OnEnter()
        {
           GameManager.Instance.SpotLight(_spotlighted,_duration);
        }
    }
}