using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Question
{
    [Serializable]
    public class Option
    {
        [Serializable]
        public class Effects
        {
            public int esteem;
            public int satisfaction;
            public int risk;
        }

        public string optionName;
        public Effects effects = new Effects();

        public int GetScore()
        {
            // TODO: Change this to a better score evaluation (or) event don't use score at all.

            return effects.esteem - effects.risk + effects.satisfaction;
        }
    }

    public string question;
    public List<Option> options = new List<Option>()
    {
        new Option() {optionName = "Yes"}, 
        new Option() {optionName = "No"}, 
    };

    public Option GetBestOption()
    {
        // TODO: Change this is needed to better find out the best option (or) even don't use Best Option at all.

        if (options.Count == 0)
        {
            return null;
        }

        Option currentBest = options[0];
        int currentBestScore = options[0].GetScore();
        for (int i = 1; i < options.Count; i++)
        {
            int score = options[i].GetScore();
            if (score > currentBestScore)
            {
                currentBestScore = score;
                currentBest = options[i];
            }
        }

        return currentBest;
    }
}