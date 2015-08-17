using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace data_supply
{
    class Program
    {
        public class FontSymbol
        {
            public string Name { get; set; }
            public string Hex { get; set; }

            public override string ToString()
            {
                return string.Format("{1} - {0}", Name, Hex);
            }
        }
        public class Data
        {
            public List<FontSymbol> Symbols { get; set; }
            public Data()
            {
                Symbols = new List<FontSymbol>();
            }
            public Data(Data data)
            {
                Symbols = new List<FontSymbol>(data.Symbols);
            }
        }


        static void Main(string[] args)
        {
            { // FONTAWESOME :-)
                Console.WriteLine("MAKE FontAwesome");
                var data = new Data();
                ExCSS.Parser p = new ExCSS.Parser();
                var css = p.Parse(File.ReadAllText("font-awesome.css"));
                foreach (var item in css.StyleRules)
                {
                    ExCSS.StyleRule srule = item as ExCSS.StyleRule;
                    //.fa-glass:before
                    string cleanName = srule.Value.Replace(".fa-", "").Replace(":before", "").Replace("-", "_");

                    foreach (var name in cleanName.Split(','))
                    {
                        FontSymbol d = new FontSymbol();
                        d.Name = MakeCamelText(name);

                        var decl = srule.Declarations.FirstOrDefault(A => A.Name.ToLower() == "content");
                        if (decl != null)
                        {
                            var term = decl.Term as ExCSS.PrimitiveTerm;
                            if (term != null)
                            {
                                d.Hex = string.Format("{0:X}", (int)(term.Value as string)[0]);
                                data.Symbols.Add(d);
                            }
                        }
                    }
                }
                MakeJson(data, "fontawesome.json", false);
                Console.WriteLine("MAKE FontAwesome DONE.");
            }


            Console.Read();
        }

        private static void MakeJson(Data data, string fileName, bool doubleKeys = false)
        {
            List<string> keys = new List<string>();
            Data result = new Data(data);

            if (doubleKeys == false)
            {
                foreach (var item in data.Symbols)
                {
                    if (keys.Contains(item.Name))
                    {
                        Console.WriteLine("WARNING: key doubled {0} {1}", item.Name, fileName);
                        result.Symbols.Remove(item);
                    }
                }
            }

            File.WriteAllText(fileName, Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        static string MakeCamelText(string input, bool trim = true)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string result = "";
                bool camel = true;
                for (int i = 0; i < input.Length; i++)
                {
                    char c = input[i];
                    if (char.IsLetter(c))
                    {
                        if (camel)
                        {
                            result += char.ToUpper(c);
                            camel = false;
                        }
                        else
                        {
                            result += c;
                        }
                    }
                    else
                    {
                        camel = true;
                        if (!trim)
                        {
                            result += c;
                        }
                    }
                }

                return result;
            }
            return input;
        }
    }
}
