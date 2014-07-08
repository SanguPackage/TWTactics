using System;
using System.Text.RegularExpressions;

namespace TribalWars.Browsers.Parsers
{
    /// <summary>
    /// Interface for handling the parsing of web documents
    /// </summary>
    public interface IBrowserParser
    {
        /// <summary>
        /// Gets the pattern for analysing the url
        /// </summary>
        Regex UrlRegex
        {
            get;
        }

        /// <summary>
        /// Checks if the current page is handled by the parser
        /// </summary>
        /// <param name="url">The browser Uri</param>
        bool Handles(string url);

        /// <summary>
        /// Handles the parsing of the document
        /// </summary>
        /// <param name="document">The document Html</param>
        /// <param name="serverTime">The time the page was generated</param>
        bool Handle(string document, DateTime serverTime);
    }
}
