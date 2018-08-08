using System.Collections.Generic;

namespace Adventure.Net
{
    public class GrammarBuilder
    {
        private readonly IList<string> grammarTokens;

        public GrammarBuilder(IList<string> grammarTokens)
        {
            this.grammarTokens = grammarTokens;
        }

        public IList<string> Build()
        {
            string[,] temp = new string[8,grammarTokens.Count];
            int xpos = 0;
            int ypos = 0;
            int n = 0; 
            var grammars = new List<string>();

            if (grammarTokens.Contains(K.ALL))
            {
                grammars.Add(K.MULTI_TOKEN);
                grammars.Add(K.MULTIHELD_TOKEN);
                return grammars;
            }

            foreach (string grammarToken in grammarTokens)
            {
                if (grammarToken == K.OBJECT_TOKEN)
                {
                    xpos = n;
                    temp[0, n] = K.NOUN_TOKEN;
                    temp[1, n] = K.HELD_TOKEN;
                    temp[2, n] = K.MULTI_TOKEN;
                    temp[3, n] = K.MULTIHELD_TOKEN;
                }
                else if (grammarToken == K.INDIRECT_OBJECT_TOKEN)
                {
                    ypos = n;
                    temp[4, xpos] = K.NOUN_TOKEN;
                    temp[5, xpos] = K.HELD_TOKEN;
                    temp[6, xpos] = K.MULTI_TOKEN;
                    temp[7, xpos] = K.MULTIHELD_TOKEN;

                    temp[0, n] = K.NOUN_TOKEN;
                    temp[1, n] = K.NOUN_TOKEN;
                    temp[2, n] = K.NOUN_TOKEN;
                    temp[3, n] = K.NOUN_TOKEN;
                    temp[4, n] = K.HELD_TOKEN;
                    temp[5, n] = K.HELD_TOKEN;
                    temp[6, n] = K.HELD_TOKEN;
                    temp[7, n] = K.HELD_TOKEN;
                }
                else
                {
                    temp[0, n] = grammarToken;
                    temp[1, n] = grammarToken;
                    temp[2, n] = grammarToken;
                    temp[3, n] = grammarToken;
                    temp[4, n] = grammarToken;
                    temp[5, n] = grammarToken;
                    temp[6, n] = grammarToken;
                    temp[7, n] = grammarToken;
                }

                n++;
            }

            int count = (ypos > 0) ? 8 : 4;

            for (int i = 0; i < count; i++)
            {
                var elements = new List<string>();
                
                for (int j = 0; j < grammarTokens.Count; j++)
                {
                    elements.Add(temp[i,j]);    
                }

                string grammar = string.Join(" ", elements.ToArray());
                if (!grammars.Contains(grammar))
                    grammars.Add(grammar);
            }
            
            return grammars;
        }
    }
}
