using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhoenix
{
    public class TextHelper
    {



        public static string RemoveFirst(string str, char rem)
        {
            List<char> sex = str.ToList();
            for (int i = 0; i < sex.Count; i++)
            {
                if (sex[i] == rem)
                {
                    sex.RemoveAt(i);
                    i = sex.Count;
                }
            }
            string Mystring = "";
            for (int i = 0; i < sex.Count; i++) Mystring = Mystring + sex[i];

            return Mystring;
        }

        public static string Remove(string str, char rem)
        {
            List<char> sex = str.ToList();
            for(int i = 0; i < sex.Count; i++)
            {
                if (sex[i] == rem)
                {
                    sex.RemoveAt(i);
                }
            }
            string Mystring = "";
            for (int i = 0; i < sex.Count; i++) Mystring = Mystring + sex[i];

            return Mystring;
        }


        public static string TextBoxHelper(string phrase, int boxLength)
        {
            int working = 0;
            string[] words = phrase.Split(' ');

            string end = "";
            List<string> balls = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                balls.Add(words[i]);
            }

            //balls.Insert(0, "                                "); //make this bl - 2
            //balls.Insert(0, "  "); // make this 2

            for (int i = 0; i < balls.Count; i++)
            {
                
                if (working + balls.ElementAt(i).Length > boxLength-1)
                {
                    working = 0;
                    balls.Insert(i, "\n");
                }
                working += balls.ElementAt(i).Length;
            }
            for (int i = 0; i < balls.Count; i++)
            {

                end += (balls.ElementAt(i));
                if (!balls.ElementAt(i).Equals("\\"))
                {
                    end += " ";
                }
            }
            return end;
        }
       

    }

}
