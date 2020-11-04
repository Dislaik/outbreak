using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak
{
    public class Translation
    {
        public string Lang;
        public Dictionary<string, Dictionary<string, string>> Locale { get; set; } = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, string> DictionaryLang = new Dictionary<string, string>();

        public Translation(string Locales)
        {
            Lang = Locales;
            Locale.Add(Locales, DictionaryLang);
        }
        public string _(string Translation)
        {
            string LocaleError = $"Locale [{Lang}] does not exist!";
            string TranslationError = $"Translation [{Lang}][{Translation}] does not exist!";

            if (Locale.ContainsKey(Lang))
            {
                try
                {
                    return Locale[Lang][Translation];
                } 
                catch { return TranslationError; }
            }
            else { return LocaleError; }
        }

        public void Global()
        {
            Locale["EN"] = new Dictionary<string, string>(){
                {"Admin:Menu_Title", "DEVELOPER MENU"},
                {"Admin:Menu_Subtitle", "Just for tests"},
                {"Inventory:Menu_Title", "INVENTORY"},
                {"Inventory:Menu_Subtitle", "Keep your items save"}
            };

            Locale["ES"] = new Dictionary<string, string>(){
                {"Admin:Menu_Title", "MENU DE DESARROLLADOR"},
                {"Admin:Menu_Subtitle", "Solo para pruebas"},
                {"Inventory:Menu_Title", "INVENTARIO"},
                {"Inventory:Menu_Subtitle", "mantén tus objetos guardados"}
            };
        }
    }
}