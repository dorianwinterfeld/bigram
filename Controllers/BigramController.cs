using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleChart.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleChart.Controllers
{
    public class BigramController : Controller
    {
        /// <summary>
        /// Index method
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public IActionResult Index(string inputText = "")
        {
            ViewBag.inputText = inputText;
            return View();
        }

        /// <summary>
        /// ProcessBigram called by Ajax callback
        /// </summary>
        /// <param name="inputText">Input string</param>
        /// <returns>tuple, frequency pairs as JSON data</returns>
        public JsonResult ProcessBigram(string inputText = "")
        {
            var results = ProcessString(inputText);
            var list = new List<KeyValuePair<string, int>>();
            foreach (var item in results)
            {
                list.Add(new KeyValuePair<string, int>(item.Key, item.Value));
            }
            return Json(list);
        }

        /// <summary>
        /// untility to remove punctuation from string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string RemovePunctuation(string input)
        {
            return input.Replace(".", "").Replace("!", "").Replace("?", "").Replace("\"", "").Replace(",", "").Replace("#", "").Replace(":", "").Replace(";", "");
        }

        /// <summary>
        /// ProcessString calculates frequency of tuples
        /// </summary>
        /// <param name="strIn">Input string</param>
        /// <returns>hash of tuple, requency pairs</returns>
        public Dictionary<string, int> ProcessString(string strIn = "")
        {
            var dic = new Dictionary<string, int>();

            var source = RemovePunctuation(strIn);
            var words = source.ToLower().Split(' ');

            for (var i = 0; i < words.Length - 1; i++)
            {
                var phrase = $"{words[i]} {words[i + 1]}";

                var ext = dic.FirstOrDefault(r => r.Key == phrase);

                if (ext.Value > 0)
                {
                    dic[phrase] = ext.Value + 1;
                }
                else
                {
                    dic.Add(phrase, 1);
                }
            }
            return dic;
        }
    }
}