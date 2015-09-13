using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using NUnit.Framework;

namespace TribalWarsTests.I18nTools
{
	[TestFixture]
	public class I18nHelper
	{
		[Test]
		public void CreateNewTranslation()
		{
			const string NewLanguage = ".es.resx";

			var nlFiles = Directory.GetFiles(@"C:\code\TribalWars\TWTactics\TribalWars", "*.nl.resx", SearchOption.AllDirectories);
			foreach (var nlFile in nlFiles)
			{
				var nlRes = new ResXResourceReader(nlFile);
				var engRes = new ResXResourceReader(nlFile.Replace(".nl.resx", ".resx"));

				//var newFileName = nlFile.Replace(".nl.resx", NewLanguage);
				//var newFile = File.Create(newFileName);
				//var newRes = new ResXResourceWriter(newFile);

				if (IsFormResx(nlFile))
				{
					
				}

				Debug.WriteLine("");
				Debug.WriteLine("");
				Debug.WriteLine("");
				Debug.WriteLine(nlFile);
				Debug.WriteLine("-------------------------------------------");
				foreach (DictionaryEntry entry in nlRes.OfType<DictionaryEntry>().Where(IsRelevant))
				{
					Debug.WriteLine(entry.Key + ": " + entry.Value);
				} 
			}
		}

		private bool IsFormResx(string resx)
		{
			var name = new FileInfo(resx).Name;
			var manualResx = new[] { "ControlsRes.nl.resx", "VillageGridExRes.nl.resx", "FormRes.nl.resx" };
			return !manualResx.Contains(name);
		}

		private bool IsRelevant(DictionaryEntry data)
		{
			var notRelevant = new[] {"Image", "Size", "ImeMode"};
			string propName = data.Key.ToString().Substring(data.Key.ToString().LastIndexOf(".") + 1);
			if (notRelevant.Contains(propName))
				return false;

			return true;
		}
	}
}
