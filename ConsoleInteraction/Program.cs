﻿using System;
using System.Diagnostics;
using System.Net.Http;
using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Collections;
using AngleSharp.Css;

namespace ConsoleInteraction
{
    class Program
    {
        static void Main(string[] args)
        {
            //IMPORTANT:
            //http://www.w3.org/TR/domcore/#interface-htmlcollection

            TestCSS(Stylesheets.rsi);

            //CssSelectorTest.Slickspeed();

            //TestHtml(Snippets.Invalid);

            //TestHtml(Webpages.Simon);

            //TestHtml(Webpages.Test);

            //TestHtml(Snippets.SelfClosedP);

            //TestHtml(Snippets.NormalClosedP);

            //TestHtmlFragment(@"<a href='?hi&amp=5'>hi</a>");

            //TestHtml("<div id=mydiv class=hi></div>", true);

            //TestHtml(Webpages.CodeProject);

            TestWebRequest("http://www.yahoo.com/", false);

            TestWebRequest("http://www.codeproject.com/", false);

            TestWebRequest("http://www.florian-rappl.de/", true);
        }

        private static void TestWebRequest(string url, bool openConsole)
        {
            var sw = Stopwatch.StartNew();
            HttpClient client = new HttpClient();
            var result = client.GetAsync(url).Result;
            //var source = result.Content.ReadAsStringAsync().Result;
            var source = result.Content.ReadAsStreamAsync().Result;

            sw.Stop();
            Console.WriteLine("Loading " + url + " took ... " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();

            var html = DocumentBuilder.Html(source);
            sw.Stop();

            Console.WriteLine("Parsing " + url + " took ... " + sw.ElapsedMilliseconds + "ms");

            if (openConsole)
            {
                var console = new HtmlSharpConsole(html);
                console.Capture();
            }
        }

        private static void TestHtml(string source, bool openConsole)
        {
            var doc = TestHtml(source);

            if (openConsole)
            {
                var console = new HtmlSharpConsole(doc);
                console.Capture();
            }
        }

        private static HTMLDocument TestHtml(string source)
        {
            var sw = Stopwatch.StartNew();
            var html = DocumentBuilder.Html(source);
            sw.Stop();
            Console.WriteLine(">>> START");
            Console.WriteLine(html.ToHtml());
            Console.WriteLine(">>> END");
            Console.WriteLine();
            Console.WriteLine("... " + sw.ElapsedMilliseconds + "ms");
            Console.WriteLine("=====================================");
            return html;
        }

        private static NodeList TestHtmlFragment(string source)
        {
            var sw = Stopwatch.StartNew();
            var nodes = DocumentBuilder.HtmlFragment(source, new HTMLParagraphElement());
            sw.Stop();
            Console.WriteLine(">>> START");
            Console.WriteLine(nodes.ToHtml());
            Console.WriteLine(">>> END");
            Console.WriteLine();
            Console.WriteLine("... " + sw.ElapsedMilliseconds + "ms");
            Console.WriteLine("=====================================");
            return nodes;
        }

        private static void TestCSS(string source)
        {
            var parser = new CssParser(source);
            var sw = Stopwatch.StartNew();
            var doc = parser.Parse();
            sw.Stop();
            Console.WriteLine("... " + sw.ElapsedMilliseconds + "ms");
        }
    }
}