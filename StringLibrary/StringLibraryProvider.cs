using System.Collections.Generic;
using System.Linq;

namespace StringLibrary
{
    public static class StringLibraryProvider
    {
        //example
        //| str = "(({[][]{}(())}))"  ==> true  |
        //| str = "()"                ==> true  |
        //| str = "({({()})})"        ==> true  |
        //|=====================================|           
        //| str = ""                  ==> false |
        //| str = "(()}"              ==> false |
        //| str = "){}("              ==> false |

        private static readonly Dictionary<int, char> _startBrackets = new Dictionary<int, char>
        {
            [1] = '(',
            [2] = '[',
            [3] = '{'
        };
        private static readonly Dictionary<int, char> _endBrackets = new Dictionary<int, char>
        {
            [1] = ')',
            [2] = ']',
            [3] = '}'
        };

        public static bool GetInfoAboutCorrectBrackets(string str)
        {
            if (str.Length < 2 || str.Length % 2 != 0 || _endBrackets.Any(x => x.Value == str[0]))
            {
                return false;
            }

            int indexForRebuildStr;
            string tempStr = str;

            for (int i = 0; i < str.Length / 2; i++)
            {
                indexForRebuildStr = GetIndexForRebuildStr(tempStr);

                if (indexForRebuildStr == -1)
                {
                    return false;
                }

                tempStr = GetRebuildStr(tempStr, indexForRebuildStr);
            }

            return true;
        }

        private static int GetIndexForRebuildStr(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (i > 0 && _startBrackets.FirstOrDefault(x => x.Value == str[i - 1]).Key == _endBrackets.FirstOrDefault(x => x.Value == str[i]).Key)
                {
                    return (i - 1);
                }
            }

            return -1;
        }

        private static string GetRebuildStr(string str, int indexEndBracket)
        {
            return str.Remove(indexEndBracket, 2);
        }
    }
}